using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioAtPoint : MonoBehaviour
{
    [SerializeField] AudioData audioData;
    [SerializeField] AudioSourceController controller;

    public void PlayAudioAtPoint(GameObject instantiatedBy)
    {
        controller.PlayAudio(audioData, instantiatedBy);

        Destroy(gameObject, 3);
    }
}
