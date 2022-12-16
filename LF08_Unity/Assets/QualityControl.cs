using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class QualityControl : MonoBehaviour
{
    private void Start()
    {
        transform.GetComponent<TMP_Dropdown>().SetValueWithoutNotify(QualitySettings.GetQualityLevel());
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
}
