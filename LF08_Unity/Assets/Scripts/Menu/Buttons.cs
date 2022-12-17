using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Buttons : MonoBehaviour
{
    private const string buttonClickSoundName = "buttonClick";

    public void PlayButtonClickAndSwitchScene(int index)
    {
        AudioManager.main.PlaySFX(buttonClickSoundName, callback: (Sound) => SwitchScene(index));
    }

    public void PlayButtonClick()
    {
        AudioManager.main.PlaySFX(buttonClickSoundName);
    }

    public void SwitchScene(int index)
    {
        SceneManager.LoadScene(index);

        //needed in case of game ending while being paused
        Time.timeScale = 1f;
        PauseMenu.isGamePaused = false;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}