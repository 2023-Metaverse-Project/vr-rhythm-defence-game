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
            GameObject t_leftNote = ObjectPool.instance.leftNoteQueue.Dequeue();
            t_leftNote.transform.position = tfLeftNoteSpawn.transform.position;
            t_leftNote.SetActive(true);
            GameObject t_rightNote = ObjectPool.instance.rightNoteQueue.Dequeue();
            t_rightNote.transform.position = tfRightNoteSpawn.transform.position;
            t_rightNote.SetActive(true);

            //GameObject t_leftNote = Instantiate(leftNote, tfLeftNoteSpawn.position, Quaternion.identity, this.transform);
            //GameObject t_rightNote = Instantiate(rightNote, tfRightNoteSpawn.position, Quaternion.Euler(0,0,180), this.transform);

            theTimingManager.leftNoteList.Add(t_leftNote);
            theTimingManager.rightNoteList.Add(t_rightNote);

            currentTime -= 60d / bpm;
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

        }
        else if (collision.CompareTag("RightNote"))
        {
            theTimingManager.rightNoteList.Remove(collision.gameObject);
            ObjectPool.instance.rightNoteQueue.Enqueue(collision.gameObject);

            collision.gameObject.SetActive(false);

        }
    }

}
