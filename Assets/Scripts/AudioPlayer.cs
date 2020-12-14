using UnityEngine;

namespace GrimSnapAudio
{
    public class AudioPlayer : AudioCharacter, IAudioActions
    {
        [SerializeField] MusicManager musicManager;
        [SerializeField] AudioState playerAudioState;

        public void SetPlayerAudioState(AudioState audioState)
        {
            playerAudioState = audioState;

            if (musicManager != null)
                musicManager.MusicChange(playerAudioState);
        }
        public void AttackAudio()
        {
            if (GetEvent(0) != null)
                controller.PlayAudio(GetEvent(0), gameObject);
            //   Debug.LogWarning("Player Attack Audio");

        }

        public void TakeDamageAudio()
        {
            if (GetEvent(1) != null)
                controller.PlayAudio(GetEvent(1), gameObject);

            // Debug.LogWarning("Player Damage Audio");
        }
    }
}