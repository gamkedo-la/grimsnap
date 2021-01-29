using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
public enum AudioState { Normal, Battle, GameOver }
public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    [Header("Music Manager Config")]
    public bool playStingers;
    [SerializeField] AudioState currentAudioState;
    internal AudioState heldAudioState;
    [SerializeField] List<MusicStatePlayer> musicList = new List<MusicStatePlayer>();

    [Header("Mixer Config")]
    [SerializeField] AudioMixer mixer; //Todo, add Snapshot changes to Music Change Transitions
    public AudioMixerSnapshot gameStartSnapshot;
    public AudioMixerSnapshot normalMusicSnapshot;
    public AudioMixerSnapshot battleStateSnapshot;
    public AudioMixerSnapshot fadeOutMusicSnapshot;
    public float musicFadeTime = 1f;

    //public UnityAction<GameObject, AudioState> musicStateChange;
    [Header("Music Events")]
    public UnityEvent playNormalMusic;
    public UnityEvent playBattleMusic;
    public UnityEvent playGameOverMusic;
    public UnityEvent playStinger;
    public static Action<AudioState> notifyMusicManager;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else if (instance != null)
        {
            Destroy(this);
        }
    }

    #region Music Initialization
    void Start()
    {
        notifyMusicManager += CompareAudioState;

        UpdateAudioState(AudioState.Normal);
        MusicChange(currentAudioState);

        InitializeMusicList();

        if (normalMusicSnapshot != null)
            normalMusicSnapshot.TransitionTo(2f);
        else
            Debug.LogError("Did you setup mixer in the music manager?");
    }

    private void InitializeMusicList()
    {
        var music = GetComponentsInChildren<MusicStatePlayer>();
        foreach (MusicStatePlayer track in music)
        {
            musicList.Add(track);
        }
    }

    #endregion

    void Update()
    {

    }

    #region Music State Change Logic
    public void MusicChange(AudioState audioState)
    {
        switch (audioState)
        {
            case AudioState.Normal:
                //CompareAudioState(audioState);
                playNormalMusic.Invoke();
                playStingers = true;
                break;

            case AudioState.Battle:
                //CompareAudioState(audioState);
                playStingers = false;
                battleStateSnapshot.TransitionTo(musicFadeTime);
                playBattleMusic.Invoke();
                break;

            case AudioState.GameOver:
                //CompareAudioState(audioState);
                playGameOverMusic.Invoke();
                break;
        }
    }

    private void CompareAudioState(AudioState audioState)
    {
        if (audioState == currentAudioState)
        {
            return;
        }
        else if (audioState != currentAudioState)
        {
            StopCoroutine("RestartLevelMusic");
            normalMusicSnapshot.TransitionTo(0.5f);

            if (playStingers)
            {
                playStinger.Invoke();
            }

            StopLevelMusic();
            MusicChange(audioState);
            UpdateAudioState(audioState);
        }
    }

    private void UpdateAudioState(AudioState audioState)
    {
        currentAudioState = audioState;
        heldAudioState = currentAudioState;
    }
    #endregion

    #region Music Controls
    private void StopLevelMusic()
    {
        foreach (MusicStatePlayer music in musicList)
        {
            music.StopMusic();
        }
    }
    public void FadeOutMusic()
    {
        fadeOutMusicSnapshot.TransitionTo(musicFadeTime);
    }

    public void ExitBattleMusic()
    {
        StartCoroutine(RestartLevelMusic(musicFadeTime));
    }

    public IEnumerator RestartLevelMusic(float waitTime)
    {
        float offset = 0.1f;

        yield return new WaitForSecondsRealtime(waitTime + offset);
        FadeOutMusic();

        yield return new WaitForSecondsRealtime(musicFadeTime);

        StopLevelMusic();
        UpdateAudioState(AudioState.Normal);
        MusicChange(currentAudioState);
        normalMusicSnapshot.TransitionTo(musicFadeTime);
    }

    public void SetMusicFadeTime(float time)
    {
        musicFadeTime = time;
    }

    #endregion
    private void OnDestroy()
    {
        notifyMusicManager -= CompareAudioState;
    }
}
