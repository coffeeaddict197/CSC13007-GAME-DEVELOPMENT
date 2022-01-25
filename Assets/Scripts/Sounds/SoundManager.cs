using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

public enum AudioType
{
    Soundtrack,
    FX
}

public class SoundManager : MonoSingleton<SoundManager>
{
    public List<AudioTrack> tracks;
    public AudioData AudioData;

    protected override void Awake()
    {
        base.Awake();
        AudioData = new AudioData();
        AudioData = AudioData.Load();
    }

    public void Play(string soundName,AudioType type,float volume = 1)
    {
        foreach (var track in tracks)
        {
            AudioObject audio = track.audioObject.Find(x => x.name == soundName && x.type == type);
            if (audio != null)
            {
                track.source.clip = audio.clip;
                if (type == AudioType.Soundtrack)
                {
                    track.source.loop = true;
                    track.source.volume = AudioData.IsOnSoundTrack ? 1 : 0;
                    track.source.Play();
                }
                else
                {
                    track.source.loop = false;
                    track.source.volume = AudioData.IsOnFx ? 1 : 0;
                    track.source.PlayOneShot(track.source.clip);
                }
            }
        }
    }

    public void TurnOffAllSoundTrack()
    {
        List<AudioTrack> listTrack = new List<AudioTrack>();
        foreach (var track in tracks)
        {
            if (track.audioObject.Any(x => x.type == AudioType.Soundtrack))
                track.source.volume = 0;
        }
    }
    
    public void TurnOnAllSoundTrack()
    {
        List<AudioTrack> listTrack = new List<AudioTrack>();
        foreach (var track in tracks)
        {
            if (track.audioObject.Any(x => x.type == AudioType.Soundtrack))
                track.source.volume = 1;
        }
    }
    
}




[System.Serializable]
public class AudioObject
{
    public AudioType type;
    public string name;
    public AudioClip clip;
}

[System.Serializable]
public class AudioTrack
{
    public AudioSource source;
    public List<AudioObject> audioObject;
}

[System.Serializable]
public class AudioData
{
    public static string dataKey = "DATA_AUDIO";
    private bool _isOnFX = true;
    private bool _isOnSoundTrack = true;

    public bool IsOnFx
    {
        get => _isOnFX;
        set
        {
            _isOnFX = value;
            Save();
        }
    }

    public bool IsOnSoundTrack
    {
        get => _isOnSoundTrack;
        set
        {
            _isOnSoundTrack = value;
            Save();
            
        }
    }
    
    
    public AudioData Load()
    {
        string jsonData = PlayerPrefs.GetString(dataKey, "NULL");
        if (jsonData != "NULL")
        {
            return JsonConvert.DeserializeObject<AudioData>(jsonData);
        }
        else
        {
            return new AudioData();
        }
    }

    public void Save()
    {
        PlayerPrefs.SetString(dataKey,JsonConvert.SerializeObject(this));
    }

}
