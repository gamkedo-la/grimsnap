using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            this.gameObject.SetActive(false);
            other.GetComponent<InventoryManager>().CollectQuestItem(this);

            var audio = other.GetComponent<GrimSnapAudio.AudioPlayer>();

            if (audio != null)
                audio.ItemPickUpAudio();
        }
    }
}