using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class JudgementTextManager : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI[] textList;

    [SerializeField]
    Animator[] judgementTextAnimators;

    private void Start()
    {
        for (int i = 0; i < textList.Length; i++)
        {
            textList[i].gameObject.SetActive(false);
        }
    }

    public void Stop()
    {
        for (int i = 0; i < textList.Length; i++)
        {
            textList[i].gameObject.SetActive(false);
        }
    }

    public void TextAnimating(PressTiming pressType)
    {
        textList[(int)pressType].gameObject.SetActive(true);
        judgementTextAnimators[(int)pressType].SetTrigger("Hit");
    }
}
