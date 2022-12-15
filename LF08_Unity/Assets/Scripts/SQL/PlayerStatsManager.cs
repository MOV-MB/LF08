using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsManager 
{
    public PlayerStats _playerStats;
    //TODO: Find a way to make this work and implement the functionality from DatabaseHelper.cs and manage the PlayerStats variables
    public PlayerStatsManager()
    {
        _playerStats = new PlayerStats();
    }
}
