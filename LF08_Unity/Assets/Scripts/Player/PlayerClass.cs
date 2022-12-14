using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClass : MonoBehaviour
{
    private int _gold;
    private string _playerName;
    private readonly int _difficulty;
    private int _killCount;
    private int _deathCount;


    public PlayerClass(string playerName, int difficulty)
    {
        this._playerName = playerName;
        _gold = 0;
        _killCount = 0;
        _deathCount = 0;
        this._difficulty = difficulty;
    }

    public PlayerClass(string playerName)
    {
        this._playerName = playerName;
        _gold = 0;
        _killCount = 0;
        _deathCount = 0;
        _difficulty = 1;
    }


    //Methoden
    public void AddGold(int value)
    {
        _gold += value * _difficulty;
    }

    public void RemoveGold(int value)
    {
        _gold -= value * _difficulty;
    }

    public void AddDeath()
    {
        _deathCount++;
    }

    public void AddKill()
    {
        _killCount++;
    }

    // Update is called once per frame
    private void Update()
    {
        if (_gold > 0) _gold = 0;
    }
}