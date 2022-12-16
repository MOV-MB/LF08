using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public Slider healthSlider;
    public GameObject bar;
    public float timeToTween = 0.25f;
    public LeanTweenType easeType;

    private void Start()
    {
       Debug.Log("healthSlider value:" + healthSlider.value);
    }
    public void SetHealth(float health)
    {
        LeanTween.value(bar, healthSlider.value, health / 100, timeToTween)
                 .setOnUpdate((float val)=> { healthSlider.value = val; })
                 .setEase(easeType);
    }
}
