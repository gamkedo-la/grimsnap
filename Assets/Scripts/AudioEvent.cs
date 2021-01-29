using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEvent : MonoBehaviour
{
    public AudioData audioData;
    [SerializeField] AudioSourceController controller;

    void Start()
    {
        if (controller == null)
        {
            controller = GetComponent<AudioSourceController>();

            if (controller == null)
            {
                controller = gameObject.AddComponent<AudioSourceController>();
            }
        }
    }

    public void PlayAudioEvent()
    {
        controller.PlayAudio(audioData, gameObject);
    }

    public void PlayAudioEvent(AudioData data)
    {
        controller.PlayAudio(data, gameObject);

    }
}
