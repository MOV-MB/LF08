using Assets.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private Player player;
    private const string moneyPickupSoundName = "moneyPickup";
    private const string healthPickupSoundName = "hpPickup";
    ulong amount = 0;
    float hp = 0;

    private void Start()
    {
        player = GameObject.Find("Player").gameObject.GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if (this.name.Contains("Coin"))
            {
                amount = 1;
                player.AddMoney(amount);
                AudioManager.main.PlaySFX(moneyPickupSoundName);
            }

            else if (this.name.Contains("Money_10"))
            {
                amount = 10;
                player.AddMoney(amount);
                AudioManager.main.PlaySFX(moneyPickupSoundName);
            }

            else if (this.name.Contains("Heart"))
            {
                hp = 20;
                player.AddHealth(hp);
                AudioManager.main.PlaySFX(healthPickupSoundName);
            }

            Destroy(this.gameObject);
        }
    }

}
