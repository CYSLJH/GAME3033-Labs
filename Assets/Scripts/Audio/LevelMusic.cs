using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMusic : MonoBehaviour
{
    //used to start game scene music
    public void Start()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            AudioManager.PlayMusic(AudioManager.Music.MainMenu);
        }
        if (SceneManager.GetActiveScene().name == "Credits")
        {
            AudioManager.PlayMusic(AudioManager.Music.MainMenu);
        }
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            AudioManager.PlayMusic(AudioManager.Music.Game);
        }

        //DontDestroyOnLoad(this.gameObject);
    }
}
