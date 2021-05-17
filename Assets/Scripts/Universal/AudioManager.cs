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
    public float maxVol;
    bool done;
    List<int> tracksToPlay;
    int currentIndex = -1;

    void Update()
    {
        foreach (AudioSource sf in sfx) { sf.volume = volume * maxVol; }
        foreach (AudioSource song in musicTracks) { song.volume = volume * maxVol; };
        if (musicCanPlay)
        {
            if (!musicToPlay[currentTrack].isPlaying)
            {
                if(tracksToPlay != null)
                {
                    if (tracksToPlay.Count >= 1)
                    {
                        currentIndex += Random.Range(0, tracksToPlay.Count);
                        currentTrack = tracksToPlay[currentIndex];
                    }
                }
                done = true;
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
        foreach (AudioSource track in musicTracks) track.Stop();
        currentTrack = newTrack;
        musicTracks[currentTrack].Play();
    }
    public void SwitchTrack(List<int> newTracks)
    {
        foreach(AudioSource track in musicTracks) track.Stop();
        currentIndex = -1;
        tracksToPlay = newTracks;
    }
}
