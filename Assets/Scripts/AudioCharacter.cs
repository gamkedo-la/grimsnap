using System.Collections.Generic;
using UnityEngine;

namespace GrimSnapAudio
{
    [System.Serializable]
    public class AudioEvent
    {
        [SerializeField] string EventName;
        [SerializeField] AudioData audioData;

        public string GetEventName() { return EventName; }
        public AudioData GetAudioData() { return audioData; }
    }

    [System.Serializable]
    public class AudioCharacter : MonoBehaviour, AudioActions
    {
        [SerializeField] internal AudioSourceController controller;
        public List<AudioEvent> audioEvents = new List<AudioEvent>();

        public AudioData GetEvent(int index)
        {
            return audioEvents[index].GetAudioData();
        }

        public void AttackAudio() { }
        public void TakeDamageAudio() { }
    }

    public interface AudioActions
    {
        void AttackAudio();

        void TakeDamageAudio();
    }
}