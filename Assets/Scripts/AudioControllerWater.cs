using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControllerWater : MonoBehaviour
{
    public AudioSource audioSourceWaterSong;
    public AudioClip[] bgSongs;
    // Start is called before the first frame update
    void Start()
    {
        audioSourceWaterSong.clip = bgSongs[0];
        audioSourceWaterSong.loop = true;
        audioSourceWaterSong.Play();
        audioSourceWaterSong.volume = 0.15f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
