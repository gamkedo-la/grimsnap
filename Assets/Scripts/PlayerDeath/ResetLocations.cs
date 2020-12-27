using UnityEngine;
using UnityEngine.AI;

namespace PlayerDeath
{
    public class ResetLocations : MonoBehaviour
    {
        public Vector3 resetLocation;
        public GameObject player;

        private void Awake()
        {
            resetLocation = GameObject.FindGameObjectWithTag("Player").transform.position;
            Debug.Log($"default player location after death: {resetLocation.ToString()}");

            player = GameObject.FindGameObjectWithTag("Player");
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
            
            player.GetComponent<PlayerControl>().ClearTarget();
        }
    }
}