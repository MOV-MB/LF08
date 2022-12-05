using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public AudioMixer mixer;
    public string mixerVariableName;

    void Start()
    {
        UpdateSliderValue();
    }

    private void UpdateSliderValue()
    {
        float value;
        mixer.GetFloat(mixerVariableName, out value);
        transform.GetComponent<Slider>().SetValueWithoutNotify(Mathf.Pow(10, value / 20));
    }

    public void SetVolume(float volume)
    {
        mixer.SetFloat(mixerVariableName, Mathf.Log10(volume) * 20);
    }
}
