using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using Assets.Scripts.Player;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour
{
    // Reference to the UpgradeMenuUI object
    public GameObject UpgradeMenuUI;
    public Player player;
    public int UpgradeMenuTimer;
    private float _elapsedTime;

    public TextMeshProUGUI TextDescription1;
    public TextMeshProUGUI TextDescription2;
    public TextMeshProUGUI TextDescription3;

    public TextMeshProUGUI TextValue1;
    public TextMeshProUGUI TextValue2;
    public TextMeshProUGUI TextValue3;

    public Button UpgradeButton1;
    public Button UpgradeButton2;
    public Button UpgradeButton3;

    private void Start()
    {
        UpgradeButton1.onClick.AddListener(() => GetDisplayedUpgrade(1));
        UpgradeButton2.onClick.AddListener(() => GetDisplayedUpgrade(2));
        UpgradeButton3.onClick.AddListener(() => GetDisplayedUpgrade(3));
    }

    // Dictionary of possible upgrades, with the name as the key and the value as the value
    private Dictionary<string, int> upgrades = new Dictionary<string, int>()
    {
        {"ATTACK UP", 30},
        {"DOUBLE SHOT", 100},
        {"TRIPLE SHOT", 200},
        {"ATTACKSPEED UP", 30},
        {"HEALTH UP", 50},
        {"HEALTH BACK", 10}
    };

    void Update()
    {
        // Check if the UpgradeMenuUI object is closed and the game is not paused
        if (!UpgradeMenuUI.activeInHierarchy && !PauseMenu.isGamePaused)
        {
            _elapsedTime += Time.deltaTime;
            if (_elapsedTime >= UpgradeMenuTimer)
            {
                // Reset the timer
                _elapsedTime = 0;

                // Set the UpgradeMenuUI object to active
                UpgradeMenuUI.SetActive(true);
                Time.timeScale = 0f;
                PauseMenu.isGamePaused = true;

                // Select 3 random upgrades from the dictionary
                List<KeyValuePair<string, int>> selectedUpgrades = SelectRandomUpgrades(3);

                // Display the selected upgrades in the UpgradeMenuUI object
                DisplayUpgrades(selectedUpgrades);
            }
        }
    }

    // Selects a specified number of random upgrades from the dictionary and returns a list of KeyValuePair objects
    List<KeyValuePair<string, int>> SelectRandomUpgrades(int count)
    {
        // Create a list to store the selected upgrades
        List<KeyValuePair<string, int>> selectedUpgrades = new List<KeyValuePair<string, int>>();

        // Keep selecting upgrades until the desired number has been reached
        while (selectedUpgrades.Count < count)
        {
            // Choose a random name from the list of keys
            string name = upgrades.Keys.ElementAt(UnityEngine.Random.Range(0, upgrades.Count));

            // Add the name and value to the list of selected upgrades if they are not already present
            if (!selectedUpgrades.Any(x => x.Key == name))
            {
                selectedUpgrades.Add(new KeyValuePair<string, int>(name, upgrades[name]));
            }
        }

        return selectedUpgrades;
    }

    public void CloseUpgradeMenu()
    {
        UpgradeMenuUI.SetActive(false);
        Time.timeScale = 1f;
        PauseMenu.isGamePaused = false;
    }

    // Displays the specified upgrades in the UpgradeMenuUI object
    public void DisplayUpgrades(List<KeyValuePair<string, int>> selectedUpgrades)
    {
        // Assign the first upgrade to TextDescription1 and TextValue1
        TextDescription1.text = selectedUpgrades[0].Key;
        TextValue1.text = selectedUpgrades[0].Value.ToString();

        // Assign the second upgrade to TextDescription2 and TextValue2
        TextDescription2.text = selectedUpgrades[1].Key;
        TextValue2.text = selectedUpgrades[1].Value.ToString();

        // Assign the third upgrade to TextDescription3 and TextValue3
        TextDescription3.text = selectedUpgrades[2].Key;
        TextValue3.text = selectedUpgrades[2].Value.ToString();
    }

    public void GetDisplayedUpgrade(int buttonNumber)
    {
        string description = "";

        if (buttonNumber == 1) description = TextDescription1.text;
        else if(buttonNumber == 2) description = TextDescription2.text;
        else if(buttonNumber == 3) description = TextDescription3.text;

        ApplyUpgrade(description);
    }

    private void ApplyUpgrade(string input)
    {
        switch (input)
        {
            case "ATTACK UP":
                player.Money = 101;
                break;
            case "DOUBLE SHOT":
                player.Money = 102;
                break;
            case "TRIPLE SHOT":
                player.Money = 103;
                break;
            case "ATTACKSPEED UP":
                player.Money = 104;
                break;
            case "HEALTH UP":
                player.Money = 105;
                break;
            case "HEALTH BACK":
                player.Money = 106;
                break;
        }
    }
}