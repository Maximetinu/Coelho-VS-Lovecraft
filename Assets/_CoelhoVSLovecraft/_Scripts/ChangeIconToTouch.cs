using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeIconToTouch : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            transform.parent.GetComponentInChildren<Image>().enabled = true;
            this.GetComponent<TMPro.TextMeshProUGUI>().enabled = false;
        }
    }

}
