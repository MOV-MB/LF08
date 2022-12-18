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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        if (this.name.Contains("Coin"))
        {
            _amount = 1;
            _player.AddMoney(_amount);
            AudioManager.main.PlaySFX(MoneyPickupSoundName);
        }

        else if (this.name.Contains("Money_10"))
        {
            _amount = 10;
            _player.AddMoney(_amount);
            AudioManager.main.PlaySFX(MoneyPickupSoundName);
        }

        else if (this.name.Contains("Heart"))
        {
            _hp = 10;
            _player.AddHealth(_hp);
            AudioManager.main.PlaySFX(HealthPickupSoundName);
        }

        Destroy(this.gameObject);
    }

}
