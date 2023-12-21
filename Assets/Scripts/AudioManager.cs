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
    // ���� ��𼭵��� ȣ���� �� �ֵ��� �ڱ� �ڽ��� instance�� �����.
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
        Debug.Log("PLAY(): BPM�� �����մϴ�.:" + MusicList[music_index].bpm);
    }
    public void Stop()
    {
        StopMusic();
    }
    public void Restart()
    {
        notesManager.SetBPM(MusicList[music_index].bpm);
        Debug.Log("PLAY(): BPM�� �����մϴ�.:" + MusicList[music_index].bpm);
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
        //    Debug.Log(p_music_name + "(��)��� �̸��� ������ �����ϴ�.");
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
                // ��� ������ ���� SFX�÷��̾ ã�Ƽ� �ش� �÷��̾ �̿��ؼ� ����Ѵ�.
                for(int x=0;x<SFXPlayerList.Length;x++)
                {
                    if (!SFXPlayerList[x].isPlaying)
                    {
                        SFXPlayerList[x].clip = SFXList[i].clip;
                        SFXPlayerList[x].Play();
                        return;
                    }
                }
                Debug.Log("��� ����� �÷��̾ ������Դϴ�.");
                return;
            }
        }

        Debug.Log(p_sfx_name + "(��)��� �̸��� ȿ������ �����ϴ�.");
    }
}
