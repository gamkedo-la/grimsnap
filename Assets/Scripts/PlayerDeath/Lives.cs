using UnityEngine;

namespace PlayerDeath
{
    public class Lives : MonoBehaviour
    {
        public int startLives = 5;
        public int lives;

        private void Awake()
        {
            lives = startLives;
        }

        public void LooseLive()
        {
            if (lives > 0)
            {
                lives--;
            }
        }

        public bool IsNoLivesLeft()
        {
            return lives == 0;
        }
    }
}