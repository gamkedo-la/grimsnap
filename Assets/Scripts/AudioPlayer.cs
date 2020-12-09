﻿namespace GrimSnapAudio
{
    public class AudioPlayer : AudioCharacter, IAudioActions
    {
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