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

        public void GruntAudio()
        {
            if (GetEvent(1) != null)
                controller.PlayAudio(GetEvent(1), gameObject);
        }

    }
}
