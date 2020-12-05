using UnityEngine;

public class AudioOnAnim : MonoBehaviour
{
    [SerializeField] string EventName;
    [SerializeField] AudioData audioData;
    internal AudioSourceController controller;

    private void Start()
    {
        controller = GetComponent<AudioSourceController>();
    }

    public void PlayAnimationAudio(string eventName)
    {
        if (controller != null)
        {
            if (audioData != null)
            {
                if (eventName == EventName)
                    controller.PlayAudio(audioData, this.gameObject);
                else
                    Debug.LogWarning("Event Name does not match. Tried to play: " + eventName + " from: " + EventName);
            }
            else
                Debug.LogError("No audio data found on: " + this.name + " for Audio Event: " + EventName);
        }
        else
        {
            Debug.LogError("No audio controller found on: " + this.name + " for Audio Event: " + EventName);
        }
    }
}
