using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource audioSourceBeachSong;
    public AudioClip[] bgSongs;
    public float volumeMusic;

    // Start is called before the first frame update
    void Start()
    {
        audioSourceBeachSong.volume = volumeMusic;
    }

    // Update is called once per frame
  public void PlayBgMusic()
    {
        audioSourceBeachSong.clip = bgSongs[0];
        audioSourceBeachSong.loop = true;
        audioSourceBeachSong.Play();
    }
    public void PlayBeachSound()
    {
        audioSourceBeachSong.clip = bgSongs[1];
        audioSourceBeachSong.loop = true;
        audioSourceBeachSong.Play();
    }
}
