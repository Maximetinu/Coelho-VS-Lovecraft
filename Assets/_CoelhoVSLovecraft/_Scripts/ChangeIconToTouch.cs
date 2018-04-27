using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeIconToTouch : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        if (Application.isMobilePlatform || Application.platform == RuntimePlatform.Android)
        {
            transform.parent.GetChild(0).gameObject.SetActive(true);
            this.GetComponent<Text>().enabled = false;
        }
    }

}
