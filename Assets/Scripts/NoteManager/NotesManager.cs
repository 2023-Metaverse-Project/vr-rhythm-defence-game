using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesManager : MonoBehaviour
{
    [SerializeField]
    private int bpm = 0;

    double currentTime = 0d;

    [SerializeField]
    Transform tfLeftNoteSpawn = null;
    [SerializeField]
    Transform tfRightNoteSpawn = null;

    //[SerializeField]
    //GameObject leftNote = null;
    //[SerializeField]
    //GameObject rightNote = null;

    TimingManager theTimingManager;
    ComboManager theComboManager;

    private bool isPlaying = false;
    private List<GameObject> activeNoteList = new List<GameObject>();

    private void Start()
    {
        theTimingManager = GetComponent<TimingManager>();
        theComboManager = FindObjectOfType<ComboManager>();
    }

    public void Play()
    {
        isPlaying = true;
    }

    public void Stop()
    {
        isPlaying=false;
        Debug.Log("NotesManager Stop called");
        for (int i = 0; i < activeNoteList.Count; i++)
        {
            Debug.Log(activeNoteList[i]);
            if (activeNoteList[i].CompareTag("LeftNote"))
                ObjectPool.instance.leftNoteQueue.Enqueue(activeNoteList[i]);
            else if (activeNoteList[i].CompareTag("RightNote"))
                ObjectPool.instance.rightNoteQueue.Enqueue(activeNoteList[i]);

            activeNoteList[i].SetActive(false);
        }

        activeNoteList.Clear();
        theTimingManager.leftNoteList.Clear();
        theTimingManager.rightNoteList.Clear();
    }

    public void Restart()
    {
        isPlaying = true;
    }

    private void Update()
    {
        if (isPlaying)
        {
            currentTime += Time.deltaTime;

            if (currentTime >= 60d / bpm)
            {
                GameObject t_leftNote = ObjectPool.instance.leftNoteQueue.Dequeue();
                t_leftNote.transform.position = tfLeftNoteSpawn.transform.position;
                t_leftNote.SetActive(true);
                activeNoteList.Add(t_leftNote);

                GameObject t_rightNote = ObjectPool.instance.rightNoteQueue.Dequeue();
                t_rightNote.transform.position = tfRightNoteSpawn.transform.position;
                t_rightNote.SetActive(true);
                activeNoteList.Add(t_rightNote);



                //GameObject t_leftNote = Instantiate(leftNote, tfLeftNoteSpawn.position, Quaternion.identity, this.transform);
                //GameObject t_rightNote = Instantiate(rightNote, tfRightNoteSpawn.position, Quaternion.Euler(0,0,180), this.transform);

                theTimingManager.leftNoteList.Add(t_leftNote);
                theTimingManager.rightNoteList.Add(t_rightNote);

                currentTime -= 60d / bpm;
            }
        }
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        //Debug.Log("충돌 발생: " + collision.ToString());
        if (collision.CompareTag("LeftNote"))
        {
            theTimingManager.leftNoteList.Remove(collision.gameObject);
            ObjectPool.instance.leftNoteQueue.Enqueue(collision.gameObject);
            // Destroy(collision.gameObject);

            collision.gameObject.SetActive(false);

            // 유저가 버튼을 누르지 않고 그냥 지나친 Note의 경우 이 값이 True이다.
            if (collision.gameObject.GetComponent<LeftNote>().getNoteImageEnabled() == true)
            {
                theComboManager?.ResetCombo();
            }
        }
        else if (collision.CompareTag("RightNote"))
        {
            theTimingManager.rightNoteList.Remove(collision.gameObject);
            ObjectPool.instance.rightNoteQueue.Enqueue(collision.gameObject);

            collision.gameObject.SetActive(false);
        }

        activeNoteList.Remove(collision.gameObject);
    }

}
