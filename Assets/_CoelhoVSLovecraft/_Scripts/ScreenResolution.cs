using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenResolution : MonoBehaviour
{
    public int width = 480;
    public int height = 270;
    public bool fullScreen = false;

    // Use this for initialization
    void Start()
    {
        Screen.SetResolution(width, height, fullScreen);
    }
}
