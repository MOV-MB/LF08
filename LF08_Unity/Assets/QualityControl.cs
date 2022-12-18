using TMPro;
using UnityEngine;

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
