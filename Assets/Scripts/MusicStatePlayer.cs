using UnityEngine;

public class MusicStatePlayer : MonoBehaviour
{
    // ToDO make this play variable tracks
    [SerializeField] MusicData music;
    [SerializeField] AudioSourceController controller;
    [SerializeField] double waitTime;

    // Start is called before the first frame update
    void Start()
    {
        if (controller == null)
            controller = gameObject.AddComponent<AudioSourceController>();
    }

    public void StartMusic()
    {
        controller.PlayMusic(music, gameObject);
    }

    public void StartMusicScheduled()
    {
        controller.PlayMusicScheduled(music, AudioSettings.dspTime + waitTime, gameObject);
    }

    public void StopMusic()
    {
        controller.StopAll();
    }
}
