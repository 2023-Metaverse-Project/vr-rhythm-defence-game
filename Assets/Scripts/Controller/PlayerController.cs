using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.PointerEventData;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;

public class PlayerController : MonoBehaviour
{
    [Header("# Input Info")]
    public XRNode inputSource;
    public InputHelpers.Button inputButton;
    private float inputThreshold = 0.1f;

    TimingManager theTimingManager;

    private void Start()
    {
        theTimingManager = FindObjectOfType<TimingManager>();
    }

    private void Update()
    {
        InputHelpers.IsPressed(InputDevices.GetDeviceAtXRNode(inputSource), inputButton, out bool isPressed, inputThreshold);

        if (Input.GetKeyDown(KeyCode.Space) || isPressed)
        {
            theTimingManager.CheckTiming();
        }
    }
}
