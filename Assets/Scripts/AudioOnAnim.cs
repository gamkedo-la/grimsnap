using UnityEngine;

public class AudioOnAnim : MonoBehaviour
{
    [SerializeField] string EventName;
    [SerializeField] AudioData audioData;
    internal AudioSourceController controller;

    public void PlayAnimationAudio()
    {
        if (controller != null)
        {
            if (audioData != null)
                controller.PlayAudio(audioData, this.gameObject);
            else
                Debug.LogError("No audio data found on: " + this.name + " for Audio Event: " + EventName);
        }
        else
        {
            Debug.LogError("No audio controller found on: " + this.name + " for Audio Event: " + EventName);
        }
    }
}
