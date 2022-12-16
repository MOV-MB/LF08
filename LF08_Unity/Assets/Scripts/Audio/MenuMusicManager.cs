using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Scripts.Player;

public class MenuMusicManager : MonoBehaviour
{
    public string selectedMusic;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.main.PlayMusic(selectedMusic, true);
    }
}
