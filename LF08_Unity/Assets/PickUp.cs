using Assets.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private Player player;
    private const string moneyPickupSoundName = "moneyPickup";

    private void Start()
    {
        player = GameObject.Find("Player").gameObject.GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            player.IncrementMoney();
            AudioManager.main.PlaySFX(moneyPickupSoundName);
            Destroy(this.gameObject);
        }
    }
}
