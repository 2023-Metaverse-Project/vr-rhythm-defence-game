using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkillInfoPrinter : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI textRecognizerMode;
    [SerializeField]
    TextMeshProUGUI textSkillName;
    [SerializeField]
    TextMeshProUGUI textGestureAccuracy;

    public void ChangeModeText(string text)
    {
        ChangeText(textRecognizerMode, "Recognizer Mode: " + text);
    }

    public void ChangeSkillNameText(string text)
    {
        ChangeText(textSkillName, "SkillName: " + text);
    }

    public void ChangeGestureAccuracyText(float accuracy)
    {
        ChangeText(textGestureAccuracy, "Gesture Accuracy: " + accuracy);
    }

    private void ChangeText(TextMeshProUGUI textUI, string text)
    {
        textUI.text = text;
    }
}
