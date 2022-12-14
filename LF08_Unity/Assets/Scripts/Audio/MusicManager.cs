using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public string selectedMusic;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.main.PlayMusic(selectedMusic, true);
    }

    void Update()
    {
        if (PauseMenu.isGamePaused)
        {
            AudioManager.main.music.audioMixer.SetFloat("cutoffFreq", 1500f);
            AudioManager.main.currentMusic.source.pitch = 0.85f;
        }
        else if(!PauseMenu.isGamePaused)
        {
            AudioManager.main.music.audioMixer.SetFloat("cutoffFreq", 22000f);
            AudioManager.main.currentMusic.source.pitch = 1f;
        }
    }
}
