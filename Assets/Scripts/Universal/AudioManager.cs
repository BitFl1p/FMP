using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource[] sfx;
    public AudioSource[] musicTracks;
    public int currentTrack;
    public bool musicCanPlay;
    public AudioSource[] musicToPlay;
    public float volume;

    void Update()
    {
        foreach (AudioSource sf in sfx) { sf.volume = volume; };
        foreach (AudioSource song in musicTracks) { song.volume = volume; };
        if (musicCanPlay)
        {
            if (!musicToPlay[currentTrack].isPlaying)
            {
                musicToPlay[currentTrack].Play();
            }
        }
        else
        {
            musicToPlay[currentTrack].Stop();
        }
    }
    public void SwitchTrack(int newTrack)
    {
        musicTracks[currentTrack].Stop();
        currentTrack = newTrack;
        musicTracks[currentTrack].Play();
    }
}
