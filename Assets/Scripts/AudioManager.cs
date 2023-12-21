using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Music
{
    public string name;
    public AudioClip clip;
    public int bpm;
    public float sinkdelay;
}

public class AudioManager : MonoBehaviour
{
    // 언제 어디서든지 호출할 수 있도록 자기 자신을 instance로 만든다.
    public static AudioManager instance;

    [SerializeField]
    NotesManager notesManager;

    [SerializeField]
    Music[] MusicList = null;
    [SerializeField]
    Music[] SFXList = null;

    [SerializeField]
    AudioSource MusicPlayer = null;
    [SerializeField]
    AudioSource[] SFXPlayerList = null;

    private int music_index = 2;

    private void Start()
    {
        instance = this;
    }

    public void Play()
    {
        Debug.Log("Here: " + notesManager.ToString());
        notesManager.SetBPM(MusicList[music_index].bpm);
        Debug.Log("PLAY(): BPM을 변경합니다.:" + MusicList[music_index].bpm);
    }
    public void Stop()
    {
        StopMusic();
    }
    public void Restart()
    {
        notesManager.SetBPM(MusicList[music_index].bpm);
        Debug.Log("PLAY(): BPM을 변경합니다.:" + MusicList[music_index].bpm);
    }

    public void SetMusicIndex(int index)
    {
        music_index = index;
    }

    public void PlayMusic()
    {
        //for (int i = 0; i < MusicList.Length; i++)
        //{
        //    if(p_music_name == MusicList[i].name)
        //    {
        //        MusicPlayer.clip = MusicList[i].clip;
        //        MusicPlayer.Play();
        //        return;
        //    }
        //    Debug.Log(p_music_name + "(이)라는 이름의 음악이 없습니다.");
        //}
        StartCoroutine(WaitAndPlay(MusicList[music_index]));
    }

    private IEnumerator WaitAndPlay(Music music)
    {
        yield return new WaitForSeconds(music.sinkdelay);
        MusicPlayer.clip = music.clip;
        MusicPlayer.Play();
    }

    public void StopMusic()
    {
        MusicPlayer.Stop();
        StopAllCoroutines();
    }

    public void PlaySFX(string p_sfx_name)
    {
        for (int i = 0;i < SFXList.Length;i++)
        {
            if (p_sfx_name == SFXList[i].name)
            {
                // 재생 중이지 않은 SFX플레이어를 찾아서 해당 플레이어를 이용해서 재생한다.
                for(int x=0;x<SFXPlayerList.Length;x++)
                {
                    if (!SFXPlayerList[x].isPlaying)
                    {
                        SFXPlayerList[x].clip = SFXList[i].clip;
                        SFXPlayerList[x].Play();
                        return;
                    }
                }
                Debug.Log("모든 오디오 플레이어가 재생중입니다.");
                return;
            }
        }

        Debug.Log(p_sfx_name + "(이)라는 이름의 효과음이 없습니다.");
    }
}
