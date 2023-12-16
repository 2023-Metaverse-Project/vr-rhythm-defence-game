using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightNote : MonoBehaviour
{
    [SerializeField]
    private float noteSpeed = 400;

    UnityEngine.UI.Image noteImage;

    public void HideNote()
    {
        noteImage.enabled = false;
    }

    private void Update()
    {
        transform.localPosition += Vector3.left * noteSpeed * Time.deltaTime;

        //transform.localRotation = Quaternion.identity; // 고개 움직였을 때 노트 돌아가지 않게 해줌
        transform.localRotation = Quaternion.Euler(0, 0, 180);
    }
}
