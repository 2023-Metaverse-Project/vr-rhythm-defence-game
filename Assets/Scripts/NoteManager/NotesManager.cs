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

    private void Start()
    {
        theTimingManager = GetComponent<TimingManager>();
    }

    private void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime >= 60d / bpm)
        {
            Debug.Log("LeftNote Dequeue Start");
            GameObject t_leftNote = ObjectPool.instance.leftNoteQueue.Dequeue();
            Debug.Log("LeftNote Dequeue End");
            t_leftNote.transform.position = tfLeftNoteSpawn.transform.position;
            t_leftNote.SetActive(true);
            Debug.Log("LeftNote Dequeue Start");
            GameObject t_rightNote = ObjectPool.instance.rightNoteQueue.Dequeue();
            Debug.Log("LeftNote Dequeue End");
            t_rightNote.transform.position = tfRightNoteSpawn.transform.position;
            t_rightNote.SetActive(true);

            //GameObject t_leftNote = Instantiate(leftNote, tfLeftNoteSpawn.position, Quaternion.identity, this.transform);
            //GameObject t_rightNote = Instantiate(rightNote, tfRightNoteSpawn.position, Quaternion.Euler(0,0,180), this.transform);

            theTimingManager.leftNoteList.Add(t_leftNote);
            theTimingManager.rightNoteList.Add(t_rightNote);

            currentTime -= 60d / bpm;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("충돌 발생: " + collision.ToString());
        if (collision.CompareTag("LeftNote"))
        {
            Debug.Log("LeftNote Start");
            theTimingManager.leftNoteList.Remove(collision.gameObject);
            Debug.Log("End");

            ObjectPool.instance.leftNoteQueue.Enqueue(collision.gameObject);
            Debug.Log("End2");
            // Destroy(collision.gameObject);

            collision.gameObject.SetActive(false);

        }
        else if (collision.CompareTag("RightNote"))
        {
            Debug.Log("RightNote Start");
            theTimingManager.rightNoteList.Remove(collision.gameObject);
            Debug.Log("End");
            ObjectPool.instance.rightNoteQueue.Enqueue(collision.gameObject);
            Debug.Log("End2");

            collision.gameObject.SetActive(false);

        }
    }

}
