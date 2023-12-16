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
    [SerializeField]
    GameObject leftNote = null;
    [SerializeField]
    GameObject rightNote = null;

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
            GameObject t_leftNote = Instantiate(leftNote, tfLeftNoteSpawn.position, Quaternion.identity, this.transform);
            GameObject t_rightNote = Instantiate(rightNote, tfRightNoteSpawn.position, Quaternion.Euler(0,0,180), this.transform);
            
            theTimingManager.boxNoteList.Add(t_leftNote);
            theTimingManager.rightNoteList.Add(t_rightNote);

            currentTime -= 60d / bpm;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Note"))
        {
            theTimingManager.boxNoteList.Remove(collision.gameObject);
            theTimingManager.rightNoteList.Remove(collision.gameObject);

            Destroy(collision.gameObject);
        }
    }

}
