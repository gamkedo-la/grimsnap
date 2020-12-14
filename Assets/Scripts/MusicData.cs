using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(menuName = "GrimSnap/MusicData", fileName = "New MusicData.asset")]
public class MusicData : ScriptableObject
{
    [SerializeField] List<AudioClip> Music = new List<AudioClip>();

    [SerializeField] AudioMixer mixer;
    [SerializeField] AudioOutputGroup output;

    [SerializeField] bool Loop = false;
    [SerializeField] int bpm;
    [SerializeField] int bars;


    [Range(-80, 0.0001f)]
    [SerializeField] float Volume;

    private float SpatialBlend = 0f;

    #region AudioSource Parameters
    public float GetVol() { return AudioFunctions.DbToLinear(Volume); }

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

    #region Retrieving Music Clip
    public AudioClip GetClip(GameObject calledBy)
    {
        var numberOfClips = Music.Count;

        if (numberOfClips == 0)
        {
            Debug.LogError("MusicData called by: " + calledBy.name + " does not contain any AudioClips.");
            return null;
        }

        if (numberOfClips > 1)
        {
            return Music[Random.Range(0, numberOfClips)];
        }
        else
        {
            return Music[0];
        }
    }

    #endregion
}
