using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
public class PlayerStats
{
    public int Id { get; set; }
    public string PlayerName { get; set; }
    public long KillCount { get; set; }
    public long DeathCount { get; set; }
    public long Score { get; set; }

    public long MaxGold { get; set; }
    public bool IsNewPlayer { get; set; }

    

    public void IncrementMaxGold(long gold)
    {
        MaxGold++;
    }

    public void IncrementKillCount()
    {
        KillCount++;
    }

    public void IncrementDeathCount()
    {
        DeathCount++;
    }

}
