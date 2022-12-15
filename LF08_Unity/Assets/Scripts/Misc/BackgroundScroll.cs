using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundScroll : MonoBehaviour
{
    public RawImage backgroundImage;
    public float x, y;

    // Update is called once per frame
    void Update()
    {
        backgroundImage.uvRect = new Rect(backgroundImage.uvRect.position + new Vector2(x, y) 
                                 * Time.deltaTime, backgroundImage.uvRect.size);
    }
}
