using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashingAndDisappearing : MonoBehaviour
{

    public bool inStart = true;
    public float startingFlashTime = 2.0f;
    public float decreaseMultiplier = 0.75f;
    public float disappearLimit = 0.001f;

    void Start()
    {
        if (inStart)
            Invoke("Flash", startingFlashTime);
    }

    public void Flash()
    {
        gameObject.SetActive(!gameObject.activeInHierarchy);
        startingFlashTime *= decreaseMultiplier;
        if (startingFlashTime >= disappearLimit)
            Invoke("Flash", startingFlashTime);
        else
        {
            gameObject.SetActive(false);
        }
    }

}