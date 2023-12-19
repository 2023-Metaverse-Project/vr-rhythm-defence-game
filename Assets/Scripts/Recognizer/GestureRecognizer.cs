using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using PDollarGestureRecognizer;
using System.IO;
using UnityEngine.Events;
using TMPro;

public class GestureRecognizer : MonoBehaviour
{
    [Header("# Input Info")]
    public XRNode inputSource;
    public InputHelpers.Button inputButton;
    private float inputThreshold = 0.1f;
    public Transform movementSource;
    public float newPositionThresholdDistance = 0.1f;

    [Header("Change Skill")]
    public UnityEvent<Skill> OnChangeSkill;

    [Header("# Debug Info")]
    public SparkMemoryPool sparkMemoryPool;
    public GameObject projectilePrefab;
    public float scoreThreshold;

    [Header("# Gesture Info")]
    public bool creationMode = false;
    public string newGestureName;


    public UnityEvent<string> OnModeChanged;
    public UnityEvent<string> OnSkillChanged;
    public UnityEvent<float> OnAccuracyChanged;

    private List<Gesture> trainingSet = new List<Gesture>();
    private bool isMoving = false;
    private List<Vector3> positionList = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        string[] gestureFiles = Directory.GetFiles(Application.persistentDataPath, "*.xml");
        foreach (var item in gestureFiles)
        {
            trainingSet.Add(GestureIO.ReadGestureFromFile(item));
        }
    }

    // Update is called once per frame
    void Update()
    {
        OnModeChanged.Invoke(creationMode ? "creating" : "recognizing");

        InputHelpers.IsPressed(InputDevices.GetDeviceAtXRNode(inputSource), inputButton, out bool isPressed, inputThreshold);

        if (!isMoving && isPressed)  // Start the movement
        {
            StartMovement();
        }
        else if (isMoving && !isPressed) // Ending the movement
        {
            EndMovement();
        }
        else if (isMoving && isPressed) //updating the movement
        {
            UpdateMovement();
        }


    }

    void StartMovement()
    {
        Debug.Log("Start Movement");
        isMoving = true;
        positionList.Clear();
        positionList.Add(movementSource.position);

        if (projectilePrefab)
            sparkMemoryPool.spawnSpark(movementSource.position);
    }

    void EndMovement()
    {
        Debug.Log("End Movement");
        isMoving = false;

        // Create The Gesture From the Position List
        Point[] pointArray = new Point[positionList.Count];

        for (int i = 0; i < positionList.Count; i++)
        {
            Vector2 screenPoint = Camera.main.WorldToScreenPoint(positionList[i]);
            pointArray[i] = new Point(screenPoint.x, screenPoint.y, 0);
        }

        Gesture newGesture = new Gesture(pointArray);


        // Add a new Gesture to training set
        if (creationMode)
        {
            newGesture.Name = newGestureName;
            trainingSet.Add(newGesture);

            string fileName = Application.persistentDataPath + "/" + newGestureName + ".xml";
            GestureIO.WriteGesture(pointArray, newGestureName, fileName);
        }
        //recognize
        else
        {
            Result result = PointCloudRecognizer.Classify(newGesture, trainingSet.ToArray());
            Debug.Log("Class: " + result.GestureClass + " / Score: " + result.Score + " / Threshold: " + scoreThreshold);
            if (result.Score > scoreThreshold)
            {
                OnSkillChanged?.Invoke(result.GestureClass);
                OnAccuracyChanged?.Invoke(result.Score);

                // 스킬 결정 후 정보 전달
                if (result.GestureClass == "Fireball")
                    OnChangeSkill?.Invoke(Skill.Fireball);
                else if (result.GestureClass == "Thunder")
                    OnChangeSkill?.Invoke(Skill.Thunder);
            }
            else
            {
                OnSkillChanged?.Invoke("Miss!");
                OnAccuracyChanged?.Invoke(result.Score);
            }
        }
    }

    void UpdateMovement()
    {
        Debug.Log("Update Movement");
        Vector3 lastPosition = positionList[positionList.Count - 1];

        if (Vector3.Distance(movementSource.position, lastPosition) > newPositionThresholdDistance)
        {
            positionList.Add(movementSource.position);
            if (projectilePrefab)
                sparkMemoryPool.spawnSpark(movementSource.position);
        }

    }
}
