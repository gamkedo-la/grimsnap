using System.Collections.Generic;
using UnityEngine;

public class AudioSourceController : MonoBehaviour
{
    public AudioData audioData;
    [SerializeField] List<AudioSource> sources = new List<AudioSource>();
    [SerializeField] int currentIndex = 0;
    [SerializeField] int maxSources = 5;
    [SerializeField] Vector3 position;

    #region Controller Functions
    void Start()
    {
        for (int i = 0; i < maxSources; ++i)
        {
            CreateAndRegisterNewSource();
        }

        position = this.transform.position;
    }

    private void CreateAndRegisterNewSource()
    {
        var source = gameObject.AddComponent<AudioSource>();
        sources.Add(source);
    }

    private void IncrementIndex()
    {
        currentIndex = (currentIndex + 1) % maxSources;
    }

    private AudioSource GetNextSource()
    {
        if (currentIndex < sources.Count)
        {
            if (sources[currentIndex].isPlaying == false)
            {
                return sources[currentIndex];
            }
            else
            {
                IncrementIndex();
                return sources[currentIndex];
            }
        }
        else if (currentIndex >= maxSources)
        {
            CreateAndRegisterNewSource();
            return sources[currentIndex];
        }

        Debug.LogError("Problem getting source");
        return null;
    }

    #endregion

    #region AudioSource Setup

    public void SetSourceProperties(AudioData dataToRead, AudioSource sourceToSet, GameObject calledBy)
    {
        if (dataToRead != null)
        {
            sourceToSet.clip = dataToRead.GetClip(calledBy);
            sourceToSet.volume = dataToRead.GetVol();
            sourceToSet.pitch = dataToRead.GetPitch();
            sourceToSet.loop = dataToRead.IsLooping();
            sourceToSet.spatialBlend = dataToRead.GetSpatialBlend();
            sourceToSet.outputAudioMixerGroup = dataToRead.GetOutputGroup(calledBy);
        }
        else
        {
            Debug.LogError(calledBy.name + " Couldn't read audio data");
        }
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }


    #endregion


    #region AudioPlayback

    public void PlayAudio(AudioData audioToPlay, GameObject calledBy)
    {
        var source = GetNextSource();
        SetSourceProperties(audioToPlay, source, calledBy);
        source.Play();
    }

    public void Stop()
    {
        sources[currentIndex].Stop();
    }

    public void StopAll()
    {
        foreach (AudioSource source in sources)
        {
            source.Stop();
        }
    }

    #endregion
}
