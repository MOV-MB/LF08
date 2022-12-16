using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Scripts.Player;

public class MusicManager : MonoBehaviour
{
    public string selectedMusic;
    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.main.PlayMusic(selectedMusic, true);
        player = GameObject.Find("Player").gameObject.GetComponent<Player>();
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

        if(player.Health < 0 && player != null)
        {
            AudioManager.main.music.audioMixer.SetFloat("cutoffFreq", 1500f);
            AudioManager.main.effects.audioMixer.SetFloat("cutOffFreq", 1500f);
            AudioManager.main.effects.audioMixer.SetFloat("effectsPitch", 0.65f);
        }
    }
}
