using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimingManager : MonoBehaviour
{
    // ���� ��Ʈ�� �������� ������ ����

    public List<GameObject> leftNoteList = new List<GameObject>(); // ���� ������ �ִ��� ��� ��Ʈ�� ���ؾ� ��
    public List<GameObject> rightNoteList = new List<GameObject>(); // ���� �� ��Ʈ ����(���� �� ����)�� ����.

    [SerializeField]
    Transform tfLeftNoteFrame = null; // ���� ���� �߽��� ��. == ���� �������� ��ġ
    [SerializeField]
    RectTransform[] timingRect = null; // �پ��� ���� ���� Perfect, Good, Bad / Miss

    Vector2[] timingBoxs = null; // �� ���������� ��ü���� ���۰� ��

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
        // Combo Manager ������ �� �ְ�!
        comboManager = FindObjectOfType<ComboManager>();
        audioManager = FindObjectOfType<AudioManager>();

        // Ÿ�̹� �ڽ� ����
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
                            comboManager?.IncreaseCombo(); // �޺� ����
                            break;

                        case 1: // Good
                            onTimingChecked?.Invoke(PressTiming.Good);
                            comboManager?.IncreaseCombo(); // �޺� ����
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
