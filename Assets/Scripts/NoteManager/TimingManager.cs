using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingManager : MonoBehaviour
{
    // ���� ��Ʈ�� �������� ������ ����

    public List<GameObject> boxNoteList = new List<GameObject>(); // ���� ������ �ִ��� ��� ��Ʈ�� ���ؾ� ��
    public List<GameObject> rightNoteList = new List<GameObject>(); // ���� �� ��Ʈ ����(���� �� ����)�� ����.

    [SerializeField]
    Transform tfLeftNoteFrame = null; // ���� ���� �߽��� ��. == ���� �������� ��ġ
    [SerializeField]
    RectTransform[] timingRect = null; // �پ��� ���� ���� Perfect, Good, Bad / Miss

    Vector2[] timingBoxs = null; // 

    void Start()
    {
        // Ÿ�̹� �ڽ� ����
        timingBoxs = new Vector2[timingRect.Length];
        for (int i = 0; i < timingRect.Length; i++)
        {
            timingBoxs[i].Set(tfLeftNoteFrame.localPosition.x - timingRect[i].rect.width / 2,
                              tfLeftNoteFrame.localPosition.x + timingRect[i].rect.width / 2);

        }
    }

    public void CheckTiming()
    {
        for (int i = 0; i < boxNoteList.Count; i++)
        {
            float t_notePosX = boxNoteList[i].transform.localPosition.x;

            for(int x = 0; x < timingBoxs.Length; x++)
            {
                if (timingBoxs[x].x <= t_notePosX && t_notePosX <= timingBoxs[x].y)
                {
                    boxNoteList[i].GetComponent<LeftNote>().HideNote();
                    boxNoteList.RemoveAt(i);

                    rightNoteList[i].GetComponent<RightNote>().HideNote();
                    rightNoteList.RemoveAt(i);

                    Debug.Log("Hit" + x);
                    return;
                }
            }
        }

        Debug.Log("Miss");
    }    
}
