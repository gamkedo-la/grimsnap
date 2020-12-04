using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum AudioOutputGroup { Master, Music, PlayerSound, EnemySound, Ambience }

[CreateAssetMenu(menuName = "GrimSnap/AudioData", fileName = "New AudioData.asset")]
public class AudioData : ScriptableObject
{
    [SerializeField] List<AudioClip> Sounds = new List<AudioClip>();

    [SerializeField] AudioMixer mixer;
    [SerializeField] AudioOutputGroup output;

    [SerializeField] bool Loop = false;

    [Range(-80, 0.0001f)]
    [SerializeField] float Volume;

    [Range(-24, 24)]
    [SerializeField] float Pitch = 0f;

    [Range(0f, 1f)]
    [SerializeField] float SpatialBlend = 1f;

    [Range(0, 5)]
    [SerializeField] float RandomVolume = 0f;
    [SerializeField] bool RandomizeVolume = false;

    [Range(0, 12)]
    [SerializeField] float RandomPitch = 0f;
    [SerializeField] bool RandomizePitch = false;

    #region AudioSource Parameters
    public float GetVol()
    {
        if (RandomizeVolume == true)
        {
            return AudioFunctions.DbToLinear(Volume + GetRandomValueOffset(RandomVolume));
        }
        else
        {
            return AudioFunctions.DbToLinear(Volume);
        }
    }

    private float GetRandomValueOffset(float value)
    {
        return Random.Range(-value, value);
    }

    public float GetPitch()
    {
        if (RandomizePitch == true)
        {
            return AudioFunctions.St2pitch(Pitch + GetRandomValueOffset(RandomPitch));
        }
        else
        {
            return AudioFunctions.St2pitch(Pitch);
        }
    }

    public bool IsLooping() { return Loop; }
    public float GetSpatialBlend() { return SpatialBlend; }

    public AudioMixerGroup GetOutputGroup(GameObject calledBy)
    {
        string groupName;

        switch (output)
        {
            case AudioOutputGroup.Master:
                groupName = "Master";
                break;
            case AudioOutputGroup.Music:
                groupName = "Music";
                break;
            case AudioOutputGroup.PlayerSound:
                groupName = "PlayerSound";
                break;
            case AudioOutputGroup.EnemySound:
                groupName = "EnemySound";
                break;
            case AudioOutputGroup.Ambience:
                groupName = "Ambience";
                break;
            default:
                groupName = "Master";
                break;
        }

        if (mixer != null)
        {
            return mixer.FindMatchingGroups(groupName)[0];
        }
        else
        {
            Debug.LogError("Mixer Output Group could not be set by: " + calledBy.name);
            return null;
        }

    }
    #endregion

    #region AudioClip Logic

    public AudioClip GetClip(GameObject calledBy)
    {
        var numberOfClips = Sounds.Count;

        if (numberOfClips == 0)
        {
            Debug.LogError("AudioData called by: " + calledBy.name + " does not contain any AudioClips.");
            return null;
        }

        if (numberOfClips > 1)
        {
            return Sounds[Random.Range(0, numberOfClips)];
        }
        else
        {
            return Sounds[0];
        }
    }

    #endregion
}
