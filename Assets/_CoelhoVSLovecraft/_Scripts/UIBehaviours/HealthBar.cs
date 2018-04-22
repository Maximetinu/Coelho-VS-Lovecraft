using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour, IObserver
{

    private Slider healthSlider { get { return GetComponent<Slider>(); } }

    void Start()
    {
        GameController.Instance.cthulhuController.AddObserver(this);
        healthSlider.maxValue = GameController.Instance.cthulhuController.maxHealthPoints;
        healthSlider.value = GameController.Instance.cthulhuController.GetCurrentHP();
    }

    public void OnNotify()
    {
        healthSlider.value = GameController.Instance.cthulhuController.GetCurrentHP();
    }
}
