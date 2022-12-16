using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerStats
{
    public int Id { get; set; }
    public string PlayerName { get; set; }
    public int KillCount { get; set; }
    public int DeathCount { get; set; }
    public int Score { get; set; }

    public void IncrementKillCount()
    {
        KillCount++;
    }

    public void IncrementDeathCount()
    {
        DeathCount++;
    }

}
