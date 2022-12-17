using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Scripts.Player;

public class GameMusicManager : MonoBehaviour
{
    public string SelectedMusic;
    private Player _player;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.main.PlayMusic(SelectedMusic, true);
        _player = GameObject.Find("Player").GetComponent<Player>();
        AudioManager.main.effects.audioMixer.SetFloat("cutOffFreq", 22000f);
        AudioManager.main.effects.audioMixer.SetFloat("effectsPitch", 1f);
    }

    void Update()
    {
        switch (PauseMenu.isGamePaused)
        {
            case true:
                AudioManager.main.music.audioMixer.SetFloat("cutoffFreq", 1500f);
                AudioManager.main.currentMusic.source.pitch = 0.85f;
                break;
            case false:
                AudioManager.main.music.audioMixer.SetFloat("cutoffFreq", 22000f);
                AudioManager.main.currentMusic.source.pitch = 1f;
                break;
        }

        if (!(_player.Health < 0) || _player == null) return;
        AudioManager.main.music.audioMixer.SetFloat("cutoffFreq", 1500f);
        AudioManager.main.effects.audioMixer.SetFloat("cutOffFreq", 1500f);
        AudioManager.main.effects.audioMixer.SetFloat("effectsPitch", 0.65f);
    }
}
