using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;
using UnityEditor.Experimental.GraphView;
using UnityEngine.Events;

public class PressRecognizer : MonoBehaviour
{
    [Header("# Input Info")]
    [SerializeField]
    private XRNode inputSource;
    [SerializeField]
    private InputHelpers.Button inputButton;    
    [SerializeField]
    private Transform movementSource;
    [SerializeField]
    public float pressRate = 0.2f;

    private float inputThreshold = 0.1f;

    private bool canPress = true;

    [Header("# Events")]
    [SerializeField]
    private UnityEvent onPressed;

    // Update is called once per frame
    void Update()
    {
        InputHelpers.IsPressed(InputDevices.GetDeviceAtXRNode(inputSource), inputButton, out bool isPressed, inputThreshold);

        if (isPressed && canPress)
        {
            StartCoroutine(Press());
        }
    }

    private IEnumerator Press()
    {
        canPress = false;
        onPressed?.Invoke();
        yield return new WaitForSeconds(pressRate);
        canPress = true;
    }
}