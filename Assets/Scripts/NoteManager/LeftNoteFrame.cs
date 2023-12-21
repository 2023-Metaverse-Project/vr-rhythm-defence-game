using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftNoteFrame : MonoBehaviour
{
    bool musicStart = false;

    private void OnTriggerEnter(Collider collision)
    {
        if (!musicStart)
        {
            if (collision.CompareTag("LeftNote"))
            {
                AudioManager.instance.PlayMusic();
                musicStart = true;
            }
        }
    }
}
