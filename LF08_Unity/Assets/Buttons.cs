using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class Buttons : MonoBehaviour
{
    private const string ButtonClickSoundName = "buttonClick";

    public void PlayButtonClickAndSwitchScene(int index)
    {
        AudioManager.main.PlaySFX(ButtonClickSoundName, callback: (sound) => SwitchScene(index));
    }

    public void PlayButtonClick()
    {
        AudioManager.main.PlaySFX(ButtonClickSoundName);
    }

    public void SwitchScene(int index)
    {
        SceneManager.LoadScene(index);

        //needed in case of game ending while being paused
        Time.timeScale = 1f;
        PauseMenu.isGamePaused = false;
    }

    public void CheckForName()
    {

        string enteredPlayerName = GameObject.Find("InputFieldText").GetComponent<TextMeshProUGUI>().text.Trim();
        PlayerStats playerStats = PlayerStatsManager.Instance.GetPlayerStats(enteredPlayerName);

        if (playerStats == null )
        {
            PlayerStatsManager.Instance.PlayerStatsLocal.IsNewPlayer = true;
            PlayerStatsManager.Instance.PlayerStatsLocal.PlayerName = enteredPlayerName;
        }
        else
        {
            playerStats.PlayerName = enteredPlayerName;
            PlayerStatsManager.Instance.PlayerStatsLocal = playerStats;
        }

        
    }

    public void RetrieveScoreBoardInfo()
    {
        GameObject name;
        GameObject score;
        GameObject gamesPlayed;
        GameObject kd;

        name = GameObject.Find("Name");
        score = GameObject.Find("HighestScore");
        gamesPlayed = GameObject.Find("Games Played");
        kd = GameObject.Find("K/D");

        List<PlayerStats> top5PlayerStatsList = PlayerStatsManager.Instance.GetTopPlayers();


        // Loop through the list of top 5 player stats
        for (int i = 0; i < top5PlayerStatsList.Count; i++)
        {
            // Get the current player stat
            PlayerStats playerStat = top5PlayerStatsList[i];

            
            name.transform.GetChild(i).GetComponent<TMP_Text>().text = playerStat.PlayerName;

            // Repeat the process for the other GameObjects
            score.transform.GetChild(i).GetComponent<TMP_Text>().text = playerStat.Score.ToString();
            gamesPlayed.transform.GetChild(i).GetComponent<TMP_Text>().text = playerStat.DeathCount.ToString();
            kd.transform.GetChild(i).GetComponent<TMP_Text>().text =
                (playerStat.KillCount / playerStat.DeathCount).ToString();
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}