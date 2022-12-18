using Assets.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private Player _player;
    private const string MoneyPickupSoundName = "moneyPickup";
    private const string HealthPickupSoundName = "hpPickup";
    private long _amount = 0;
    private float _hp = 0;

    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
    }
    /// <summary>
    /// Checks if the player is colliding with the pickup and which pickup.
    /// And if so, adds the amount of money to the player's money.
    /// </summary>
    /// <param name="collision">the collision that entered the trigger</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        if (this.name.Contains("Coin"))
        {
            _amount = 5;
            _player.AddMoney(_amount);
            AudioManager.main.PlaySFX(MoneyPickupSoundName);
        }

        else if (this.name.Contains("Money_10"))
        {
            _amount = 25;
            _player.AddMoney(_amount);
            AudioManager.main.PlaySFX(MoneyPickupSoundName);
        }

        else if (this.name.Contains("Heart"))
        {
            _hp = 20;
            _player.AddHealth(_hp);
            AudioManager.main.PlaySFX(HealthPickupSoundName);
        }

        Destroy(this.gameObject);
    }

}
