using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource audioSourceBeachSong;
    public AudioClip[] bgSongs;
    // Start is called before the first frame update
    void Start()
    {
        audioSourceBeachSong.clip = bgSongs[0];
        audioSourceBeachSong.loop = true;
        audioSourceBeachSong.Play();
        audioSourceBeachSong.volume = 0.15f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
