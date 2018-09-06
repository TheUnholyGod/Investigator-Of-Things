using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager> {

    Dictionary<string, AudioSource> sources;

	// Use this for initialization
	void Start () {

        sources = new Dictionary<string, AudioSource>();
        this.m_destroyOnLoad = true;

        AudioSource[] source = FindObjectsOfType<AudioSource>() as AudioSource[];

        foreach (AudioSource audio in source)
            sources.Add(audio.gameObject.name, audio);
    }
	
    public void SetVolume(float newVolume)
    {
        foreach(AudioSource audio in sources.Values)
            audio.volume = newVolume;
    }

    public void PlayAudio(string name)
    {
        if (sources.ContainsKey(name) && !sources[name].isPlaying)
            sources[name].Play();
        else
            Debug.Log(name + " doesn't exist/is playing");
    }

    public void PauseAudio(string name)
    {
        if (sources.ContainsKey(name))
            sources[name].Pause();
        else
            Debug.Log(name + " doesn't exist");
    }

    public void StopAudio(string name)
    {
        if (sources.ContainsKey(name))
            sources[name].Stop();
        else
            Debug.Log(name + " doesn't exist/is playing");
    }

    public void Reset()
    {
        foreach(string audio in sources.Keys)
            sources[audio].Stop();

        sources = new Dictionary<string, AudioSource>();

        AudioSource[] source = FindObjectsOfType<AudioSource>() as AudioSource[];

        foreach (AudioSource audio in source)
            sources.Add(audio.gameObject.name, audio);
    }

}
