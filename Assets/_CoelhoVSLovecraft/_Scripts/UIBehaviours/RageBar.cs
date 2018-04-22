using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RageBar : MonoBehaviour, IObserver
{

    private Slider rageSlider { get { return GetComponent<Slider>(); } }

    void Start()
    {
        GameController.Instance.cthulhuController.AddObserver(this);
        rageSlider.maxValue = GameController.Instance.cthulhuController.maxHealthPoints;
        rageSlider.value = GameController.Instance.cthulhuController.GetCurrentRage();
    }

    public void OnNotify()
    {
        rageSlider.value = GameController.Instance.cthulhuController.GetCurrentRage();
    }
}
