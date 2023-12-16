using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftNote : MonoBehaviour
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
        transform.localPosition += Vector3.right * noteSpeed * Time.deltaTime;
        transform.localRotation = Quaternion.identity; // �� �������� �� ��Ʈ ���ư��� �ʰ� ����
    }
}
