using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using Assets.Scripts.Player;

public class UpgradeMenu : MonoBehaviour
{
    // Reference to the UpgradeMenuUI object
    public GameObject upgradeMenuUI;
    public int upgradeMenuTimer;
    private float elapsedTime;

    public TextMeshProUGUI TextDescription1;
    public TextMeshProUGUI TextDescription2;
    public TextMeshProUGUI TextDescription3;

    public TextMeshProUGUI TextValue1;
    public TextMeshProUGUI TextValue2;
    public TextMeshProUGUI TextValue3;

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
        if (!upgradeMenuUI.activeInHierarchy && !PauseMenu.isGamePaused)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= upgradeMenuTimer)
            {
                // Reset the timer
                elapsedTime = 0;

                // Set the UpgradeMenuUI object to active
                upgradeMenuUI.SetActive(true);
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
            string name = upgrades.Keys.ElementAt(Random.Range(0, upgrades.Count));

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
        if(upgradeMenuUI.activeInHierarchy)
        {
            upgradeMenuUI.SetActive(false);
            Time.timeScale = 1f;
        }
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
}