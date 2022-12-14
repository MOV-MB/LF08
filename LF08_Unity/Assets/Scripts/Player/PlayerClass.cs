using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClass : MonoBehaviour
{
    private int gold;
    private string player_name;
    private int difficulty;
    private int kill_count;
    private int death_count;


    public PlayerClass(string player_name, int difficulty)
    {
        this.player_name = player_name;
        this.gold = 0;
        this.kill_count = 0;
        this.death_count = 0;
        this.difficulty = difficulty;

    }
    public PlayerClass(string player_name)
    {
        this.player_name = player_name;
        this.gold = 0;
        this.kill_count = 0;
        this.death_count = 0;
        this.difficulty = 1;
    }


    //Methoden
    public void addGold(int value)
    {
        this.gold += value * this.difficulty;
    }

    public void removeGold(int value)
    {
        this.gold -= value * this.difficulty;
    }

    public void addDeath()
    {
        this.death_count++;
    }

    public void addKill()
    {
        this.kill_count++;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gold > 0)
        {
            this.gold = 0;
        }

    }
}
