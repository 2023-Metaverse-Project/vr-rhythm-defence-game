using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimingManager : MonoBehaviour
{
    // 왼쪽 노트를 기준으로 판정을 하자

    public List<GameObject> leftNoteList = new List<GameObject>(); // 판정 범위에 있는지 모든 노트를 비교해야 함
    public List<GameObject> rightNoteList = new List<GameObject>(); // 오른 쪽 노트 관리(생성 및 제거)를 위함.

    [SerializeField]
    Transform tfLeftNoteFrame = null; // 판정 범위 중심이 됨. == 왼쪽 프레임의 위치
    [SerializeField]
    RectTransform[] timingRect = null; // 다양한 판정 범위 Perfect, Good, Bad / Miss

    Vector2[] timingBoxs = null; // 각 판정범위의 구체적인 시작과 끝

    [SerializeField]
    ParticleSystem[] leftEffectList = null;
    [SerializeField]
    ParticleSystem[] rightEffectList = null;

    [SerializeField]
    private UnityEvent<PressTiming> onTimingChecked;

    [SerializeField]
    private UnityEvent onMissPenalty;

    ComboManager comboManager;
    AudioManager audioManager;

    void Start()
    {
        // Combo Manager 참조할 수 있게!
        comboManager = FindObjectOfType<ComboManager>();
        audioManager = FindObjectOfType<AudioManager>();

        // 타이밍 박스 설정
        timingBoxs = new Vector2[timingRect.Length];
        for (int i = 0; i < timingRect.Length; i++)
        {
            timingBoxs[i].Set(tfLeftNoteFrame.localPosition.x - timingRect[i].rect.width / 2,
                              tfLeftNoteFrame.localPosition.x + timingRect[i].rect.width / 2);
            //Debug.Log(timingBoxs[i]);
        }
    }

    public void CheckTiming()
    {
        for (int i = 0; i < leftNoteList.Count; i++)
        {
            float t_notePosX = leftNoteList[i].transform.localPosition.x;

            for(int x = 0; x < timingBoxs.Length; x++)
            {
                if (timingBoxs[x].x <= t_notePosX && t_notePosX <= timingBoxs[x].y)
                {
                    //Destroy(boxNoteList[i]);
                    leftNoteList[i].GetComponent<LeftNote>().HideNote();
                    leftNoteList.RemoveAt(i);

                    //Destroy(rightNoteList[i]);
                    rightNoteList[i].GetComponent<RightNote>().HideNote();
                    rightNoteList.RemoveAt(i);

                    leftEffectList[x].Play();
                    rightEffectList[x].Play();

                    //Debug.Log("Hit" + x);


                    switch (x)
                    {
                        case 0: // Perfect
                            onTimingChecked?.Invoke(PressTiming.Perfect);
                            comboManager?.IncreaseCombo(); // 콤보 증가
                            break;

                        case 1: // Good
                            onTimingChecked?.Invoke(PressTiming.Good);
                            comboManager?.IncreaseCombo(); // 콤보 증가
                            break;

                        case 2: // Bad
                            onTimingChecked?.Invoke(PressTiming.Bad);
                            comboManager?.ResetCombo();
                            break;
                    }

                    //audioManager.PlaySFX("Clap");
                    //AudioManager.instance.PlaySFX("Clap");

                    return;
                }
            }
        }

        leftEffectList[3].Play();
        rightEffectList[3].Play();

        onTimingChecked?.Invoke(PressTiming.Miss);
        comboManager?.ResetCombo();
        onMissPenalty?.Invoke();
        //Debug.Log("Miss");
    }    
}
