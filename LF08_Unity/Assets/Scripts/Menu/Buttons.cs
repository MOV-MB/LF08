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
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    IEnumerator timer(int index)
    {
        AudioManager.main.PlaySFX(buttonClickSoundName);
        AudioManager.main.PlaySFX("Piano");
        float t = 0;
        while(t < 0.8)
        {
            t += Time.deltaTime;



            yield return null;
        }




        SwitchScene(index);
    }
}