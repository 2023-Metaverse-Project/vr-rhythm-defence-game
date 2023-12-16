using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingManager : MonoBehaviour
{
    // 왼쪽 노트를 기준으로 판정을 하자

    public List<GameObject> boxNoteList = new List<GameObject>(); // 판정 범위에 있는지 모든 노트를 비교해야 함
    public List<GameObject> rightNoteList = new List<GameObject>(); // 오른 쪽 노트 관리(생성 및 제거)를 위함.

    [SerializeField]
    Transform tfLeftNoteFrame = null; // 판정 범위 중심이 됨. == 왼쪽 프레임의 위치
    [SerializeField]
    RectTransform[] timingRect = null; // 다양한 판정 범위 Perfect, Good, Bad / Miss

    Vector2[] timingBoxs = null; // 

    void Start()
    {
        // 타이밍 박스 설정
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
