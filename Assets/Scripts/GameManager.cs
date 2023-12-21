using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [Header("# Input Info")]
    [SerializeField]
    private XRNode inputSource;
    [SerializeField]
    private InputHelpers.Button StartButton;
    [SerializeField]
    private InputHelpers.Button QuitButton;
    [SerializeField]
    private InputHelpers.Button RestartButton;

    private float inputThreshold = 0.1f;

    private bool canPress = true;

    [Header("UI")]
    public GameObject menuUI;
    public GameObject gamingUI;
    public GameObject gameoverUI;

    [Header("Managing Objects")]
    public GameObject note;
    public GameObject pressRecognizer;
    public GameObject core;
    public GameObject audioManager;
    public GameObject LeftNoteFrame; // Note 충돌하면 노래 재생 시작되는 애임.
    public GameObject comboManager;
    public List<GameObject> spawnPoints;

    // Update is called once per frame
    void Update()
    {
        if (canPress)
        {
            InputHelpers.IsPressed(InputDevices.GetDeviceAtXRNode(inputSource), StartButton, out bool isStartPressed, inputThreshold);

            if (isStartPressed)  // 게임 시작
            {
                StartGame();
            }

            InputHelpers.IsPressed(InputDevices.GetDeviceAtXRNode(inputSource), QuitButton, out bool isQuitPressed, inputThreshold);

            if (isQuitPressed)  // 게임 종료
            {
                Application.Quit();
            }

            InputHelpers.IsPressed(InputDevices.GetDeviceAtXRNode(inputSource), RestartButton, out bool isRestartPressed, inputThreshold);

            if (isRestartPressed)  // 재시작
            {
                RestartGame();
            }
        }
    }

    public void StartGame()
    {
        canPress = false;

        gamingUI.SetActive(true);
        menuUI.SetActive(false);
        gameoverUI.SetActive(false);

        note.GetComponent<NotesManager>().Play();
        pressRecognizer.GetComponent<PressRecognizer>().Play();
        core.GetComponent<Core>().Play();
        audioManager?.GetComponent<AudioManager>().Play();
        foreach (GameObject spawnPoint in spawnPoints)
        {
            spawnPoint.GetComponent<EnemySpawner>().Play();
        }
    }

    public void StopGame()
    {
        canPress = true;

        gamingUI.SetActive(false);
        menuUI.SetActive(false);
        gameoverUI.SetActive(true);

        note.GetComponent<NotesManager>().Stop();
        pressRecognizer.GetComponent<PressRecognizer>().Stop();
        core.GetComponent<Core>().Stop();
        audioManager?.GetComponent<AudioManager>().Stop();
        LeftNoteFrame.GetComponent<LeftNoteFrame>().Stop();
        comboManager.GetComponent<ComboManager>().Stop();
        foreach (GameObject spawnPoint in spawnPoints)
        {
            spawnPoint.GetComponent<EnemySpawner>().Stop();
        }
    }

    public void RestartGame()
    {
        canPress = false;

        gamingUI.SetActive(true);
        menuUI.SetActive(false);
        gameoverUI.SetActive(false);

        note.GetComponent<NotesManager>().Restart();
        pressRecognizer.GetComponent<PressRecognizer>().Restart();
        core.GetComponent<Core>().Restart();
        audioManager?.GetComponent <AudioManager>().Restart();
        foreach (GameObject spawnPoint in spawnPoints)
        {
            spawnPoint.GetComponent<EnemySpawner>().Restart();
        }
    }
}
