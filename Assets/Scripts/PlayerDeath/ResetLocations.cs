using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace PlayerDeath
{
    public class ResetLocations : MonoBehaviour
    {
        public Vector3 resetLocation;
        private PlayerControl player;

        private void Awake()
        {
            GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
            player = playerGO.GetComponent<PlayerControl>();
            if (playerGO != null)
            {
                resetLocation = player.transform.position;
                // Debug.Log($"default player location after death: {resetLocation.ToString()}");
            }
            else
            {
                Debug.LogError("no object with player or PlayerControl script on it found");
            }
        }

        public void ChangeResetLocation(Vector3 next)
        {
            resetLocation = next;
            Debug.Log($"Reset location set to {resetLocation.ToString()}");
        }

        public void ResetPlayer()
        {
            player.transform.position = resetLocation;
            Debug.Log($"Player location reset to {player.transform.position.ToString()}");
            
            player.ClearTarget();
        }
    }
}