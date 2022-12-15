using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public Slider healthSlider;

    private void Start()
    {
       Debug.Log("healthSlider value:" + healthSlider.value);
    }
    public void SetHealth(float health)
    {
        if (health > 0) healthSlider.value = health / 100;
        else healthSlider.value = 0;

       Debug.Log("healthSlider value:" + healthSlider.value);
    }
}
