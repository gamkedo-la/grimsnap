namespace GrimSnapAudio
{
    public class AudioEnemy : AudioCharacter, IAudioActions
    {
        public void AttackAudio()
        {

        }

        public void TakeDamageAudio()
        {
            if (GetEvent(0) != null)
                controller.PlayAudio(GetEvent(0), gameObject);
        }
    }
}
