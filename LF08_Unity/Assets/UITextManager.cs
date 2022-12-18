using Assets.Scripts.Player;
using TMPro;
using UnityEngine;

public class UITextManager : MonoBehaviour
{
    private Player player;
    public TextMeshProUGUI moneyText;

    private void Start()
    {
        player = GameObject.Find("Player").gameObject.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        moneyText.text = player.Money.ToString();
    }
}
