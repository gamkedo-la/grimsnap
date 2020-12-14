using UnityEngine;
using UnityEngine.Events;
public enum AudioState { Normal, Battle, GameOver }
public class MusicManager : MonoBehaviour
{
    public bool playStingers;

    //public UnityAction<GameObject, AudioState> musicStateChange;
    public UnityEvent playNormalMusic;
    public UnityEvent playBattleMusic;
    public UnityEvent playGameOverMusic;
    public UnityEvent playStinger;

    [SerializeField] AudioState currentAudioState;
    internal AudioState heldAudioState;

    void Start()
    {
        UpdateAudioState(AudioState.Normal);
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
