using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameClearChecker : MonoBehaviour
{

    AudioManager audioManager;
    Music[] musics = null;
    Music music = null;

    double currentTime = 0;
    double gameTotalTime;

    public UnityEvent<GameStopType> onGameclear;

    private bool isPlaying= false;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        musics = audioManager.GetMusicList();
        isPlaying = false;
    }

    public void Play()
    {
        ResetTimer();
        isPlaying = true;
    }

    public void Stop()
    {
        isPlaying = false;
    }

    public void Restart()
    {
        ResetTimer();
        isPlaying = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlaying)
        {
            currentTime += Time.deltaTime;
            if (currentTime > gameTotalTime)
            {
                //Debug.Log("GAMECLEAR~" + currentTime + " / " + gameTotalTime);
                AudioManager.instance.PlaySFX("Finish");
                onGameclear?.Invoke(GameStopType.GAMECLEAR);
            }
        }
        
    }

    private void ResetTimer()
    {
        currentTime = 0;
        double playtime = (double) musics[audioManager.GetMusicIndex()].playtimeinsec;
        gameTotalTime = playtime + 3.0d;
    }
}
