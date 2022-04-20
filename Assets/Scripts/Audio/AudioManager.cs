using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public enum Sound //when adding enums make sure to update array order in Assets/Resources -> AudioAsset Prefab
    {
        PlayerMove
    }

    public enum Music //when adding enums make sure to update array order in Assets/Resources -> AudioAsset Prefab
    {
        MainMenu,
        Game,
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }


        //need to initialize on a startup gameobject. currently just adding audiomanager script to an empty object in scene to test.
        Initialize();

    }

    private static GameObject oneshotObj;
    private static AudioSource oneshotAudioSource;

    private static GameObject musicObj;
    private static AudioSource musicAudioSource;

    private static Dictionary<Sound, float> soundTimerDictionary;

    public static float SFXVolume = 1;
    public static float MusicVolume = 1;

    public static void Initialize()
    {
        soundTimerDictionary = new Dictionary<Sound, float>();
        soundTimerDictionary[Sound.PlayerMove] = 0f;
    }

    public static void ChangeSFXVolume(float value)
    {
        if (oneshotAudioSource != null)
        {
            oneshotAudioSource.volume = value;
        }
        SFXVolume = value;
    }

    public static void ChangeMusicVolume(float value)
    {
        if (musicAudioSource != null)
        {
            musicAudioSource.volume = value;
        }
        MusicVolume = value;
    }

    public static void PlaySound(Sound sound)
    {
        if (CanPlaySound(sound))
        {
            if (oneshotObj == null)
            {
                oneshotObj = new GameObject("Oneshot Sound");
                oneshotAudioSource = oneshotObj.AddComponent<AudioSource>();
            }
            oneshotAudioSource.volume = SFXVolume;
            oneshotAudioSource.PlayOneShot(GetAudioClip(sound));
        }
    }

    public static void PlaySoundAtPosition(Sound sound, Vector3 position) //AudioManager.PlaySoundAtPosition(AudioManager.Sound.PlayerAttack, transform.position);
    {
        if (CanPlaySound(sound))
        {
            GameObject soundObj = new GameObject("Sound at Position");
            soundObj.transform.position = position;
            AudioSource audioSource = soundObj.AddComponent<AudioSource>();
            audioSource.clip = GetAudioClip(sound);

            //audio source settings
            audioSource.maxDistance = 20f;
            audioSource.spatialBlend = 1f;
            audioSource.rolloffMode = AudioRolloffMode.Logarithmic;
            audioSource.dopplerLevel = 0f;
            audioSource.volume = SFXVolume;
            //audioSource.mute = oneshotAudioSource.mute;

            audioSource.Play();


            Object.Destroy(soundObj, audioSource.clip.length);
        }
    }

    private static bool CanPlaySound(Sound sound)
    {
        switch (sound)
        {
            default:
                return true;
            case Sound.PlayerMove:
                if (soundTimerDictionary.ContainsKey(sound)) //if you're getting an error here make sure there are sounds in the array. Assets/Resources -> AudioAsset Prefab. 
                {
                    float lastTimePlayed = soundTimerDictionary[sound];
                    float playerMoveTimerMax = 0.7f; //change to determine how often move sound can be played.
                    if (lastTimePlayed + playerMoveTimerMax < Time.time)
                    {
                        soundTimerDictionary[sound] = Time.time;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                else
                {
                    return true;
                }
                //break;
        }
    }

    private static AudioClip GetAudioClip(Sound sound)
    {
        foreach (AudioAssets.SoundAudioClip sceneAudioClip in AudioAssets.Instance.soundAudioClipArray)
        {
            if (sceneAudioClip.sound == sound)
            {
                return sceneAudioClip.audioClip;
            }
        }
        return null;
    }

    public static void PlayMusic(Music music)
    {
        if (musicObj == null)
        {
            musicObj = new GameObject("Music Sound");
            musicAudioSource = musicObj.AddComponent<AudioSource>();
        }
        musicAudioSource.clip = GetMusicClip(music);
        musicAudioSource.loop = true;
        musicAudioSource.volume = MusicVolume;
        musicAudioSource.Play();
    }

    private static AudioClip GetMusicClip(Music music)
    {
        foreach (AudioAssets.MusicAudioClip musicAudioClip in AudioAssets.Instance.MusicAudioClipArray)
        {
            if (musicAudioClip.music == music)
            {
                return musicAudioClip.musicClip;
            }
        }
        return null;
    }
}
