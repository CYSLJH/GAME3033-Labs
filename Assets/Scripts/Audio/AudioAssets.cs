using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioAssets : MonoBehaviour
{

    private static AudioAssets instance;
    public static AudioAssets Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Instantiate(Resources.Load<AudioAssets>("AudioAssets"));
            }
            return instance;
        }
    }

    public SoundAudioClip[] soundAudioClipArray;
    public MusicAudioClip[] MusicAudioClipArray;

    [System.Serializable]
    public class SoundAudioClip
    {
        public AudioManager.Sound sound;
        public AudioClip audioClip;
    }

    [System.Serializable]
    public class MusicAudioClip
    {
        public AudioManager.Music music;
        public AudioClip musicClip;
    }
}
