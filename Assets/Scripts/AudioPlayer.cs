using UnityEngine;

namespace GrimSnapAudio
{
    public class AudioPlayer : AudioCharacter, IAudioActions
    {
        [SerializeField] MusicManager musicManager;
        [SerializeField] AudioState playerAudioState;
        internal bool checkForEnemies = false;
        [SerializeField] int detectionRadius;
        [SerializeField] bool drawSphere;
        bool battleStateToggle;

        private void Update()
        {
            //checkForEnemies = CheckForEnemies();

            if (checkForEnemies)
            {
                SetPlayerAudioState(AudioState.Battle);
                battleStateToggle = true;
                checkForEnemies = CheckForEnemies();

            }

            if (!checkForEnemies)
            {
                //SetPlayerAudioState(AudioState.Normal);

                if (battleStateToggle)
                {
                    musicManager.ExitBattleMusic();
                    battleStateToggle = false;
                }
            }
        }

        public void SetPlayerAudioState(AudioState audioState)
        {
            playerAudioState = audioState;

            if (musicManager != null)
                MusicManager.notifyMusicManager.Invoke(playerAudioState);
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

        public void GruntAudio()
        {

        }

        public void ItemPickUpAudio()
        {
            if (GetEvent(2) != null)
                controller.PlayAudio(GetEvent(2), gameObject);
        }
        //public void PlayerBattleStateLoop()
        //{
        //    do
        //    {
        //        checkForEnemies = CheckForEnemies();
        //    } while (checkForEnemies);

        //    if (musicManager != null)
        //        musicManager.ExitBattleMusic();
        //}

        public bool CheckForEnemies()
        {
            Collider[] results = new Collider[5];

            var enemies = Physics.OverlapSphereNonAlloc(gameObject.transform.position, detectionRadius, results, 1 << 9);
            //drawSphere = true;

            if (enemies > 0)
            {
                //Debug.Log("Enemies detected");
                return true;
            }
            else
            {
                return false;
            }
        }

        private void OnDrawGizmos()
        {
            if (drawSphere)
                Gizmos.DrawSphere(gameObject.transform.position, detectionRadius);
        }
    }
}