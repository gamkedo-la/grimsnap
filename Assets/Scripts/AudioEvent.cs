using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEvent : MonoBehaviour
{
    public AudioData audioData;
    [SerializeField] AudioSourceController controller;
    public bool playOnStart;

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

        if (playOnStart)
        {
            PlayAudioEvent();
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

    public AudioSourceController GetAudioController()
    {
        return controller;
    }
}
