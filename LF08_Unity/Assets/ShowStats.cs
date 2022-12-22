using System;
using TMPro;
using UnityEngine;

public class ShowStats : MonoBehaviour
{
    private TMP_Text _currentScore;
    private TMP_Text _personalBest;
    private PlayerStats _playerStatsRetrieved;
    private long _localScore;

    private void Start()
    {
        _currentScore = GameObject.Find("CurrentScore").GetComponent<TMP_Text>();
        _personalBest = GameObject.Find("PersonalBest").GetComponent<TMP_Text>();

        SetAchievedScore();
        DeducePersonalBestAndSave();
    }

    private void SetAchievedScore()
    {
        _localScore = CalculateScore(PlayerStatsManager.Instance.PlayerStatsLocal.KillCount,
                PlayerStatsManager.Instance.PlayerStatsLocal.MaxGold);

        _currentScore.text = "SCORE:\n" + _localScore;
    }

    private void DeducePersonalBestAndSave()
    {
        


        if (!PlayerStatsManager.Instance.PlayerStatsLocal.IsNewPlayer)
        {
            _playerStatsRetrieved =
                PlayerStatsManager.Instance.GetPlayerStats(PlayerStatsManager.Instance.PlayerStatsLocal.PlayerName);

            long personalBest = _localScore > _playerStatsRetrieved.Score ? _localScore : _playerStatsRetrieved.Score;

            PlayerStatsManager.Instance.PlayerStatsLocal.Score = personalBest;

            _personalBest.text =
                "PB:\n" +  PlayerStatsManager.Instance.PlayerStatsLocal;
        }
        else
        {
            _personalBest.text = "PB:\n" + _localScore;
            PlayerStatsManager.Instance.PlayerStatsLocal.Score = _localScore;
        }


        PlayerStatsManager.Instance.SavePlayerStats();
    }

    private static long CalculateScore(long killedEnemies, long overallGold)
    {
        long score = Convert.ToInt64(overallGold + killedEnemies * 10);


        Debug.Log(score);

        return score;
    }
}