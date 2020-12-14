using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
public enum AudioState { Normal, Battle, GameOver }
public class MusicManager : MonoBehaviour
{
    [Header("Music Manager Config")]
    public bool playStingers;
    [SerializeField] AudioState currentAudioState;
    internal AudioState heldAudioState;

    [Header("Mixer Config")]
    [SerializeField] AudioMixer mixer; //Todo, add Snapshot changes to Music Change Transitions
    public AudioMixerSnapshot gameStartSnapshot;
    public AudioMixerSnapshot normalMusicSnapshot;

    //public UnityAction<GameObject, AudioState> musicStateChange;
    [Header("Music Events")]
    public UnityEvent playNormalMusic;
    public UnityEvent playBattleMusic;
    public UnityEvent playGameOverMusic;
    public UnityEvent playStinger;


    void Start()
    {
        UpdateAudioState(AudioState.Normal);
        MusicChange(currentAudioState);

        if (normalMusicSnapshot != null)
            normalMusicSnapshot.TransitionTo(2f);
        else
            Debug.LogError("Did you setup mixer in the music manager?");
    }

    void Update()
    {

    }

    public void MusicChange(AudioState audioState)
    {
        switch (audioState)
        {
            case AudioState.Normal:
                CompareAudioState(audioState);
                playNormalMusic.Invoke();
                break;

            case AudioState.Battle:
                playBattleMusic.Invoke();
                break;

            case AudioState.GameOver:
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
            if (playStingers)
            {
                playStinger.Invoke();
            }
            UpdateAudioState(audioState);
        }
    }

    private void UpdateAudioState(AudioState audioState)
    {
        currentAudioState = audioState;
        heldAudioState = currentAudioState;
    }
}
