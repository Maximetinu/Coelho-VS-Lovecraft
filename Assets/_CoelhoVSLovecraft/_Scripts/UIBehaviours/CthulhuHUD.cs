using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CthulhuHUD : MonoBehaviour, IObserver
{
    [Header("Animation")]
    public List<Vector2> offsets;
    public float frameDuration;
    public int startingFrame = 0;

    [Space(10)]
    public Transform HUDPosition;

    private RectTransform rt { get { return GetComponent<RectTransform>(); } }
    private bool currentlyBeingDamaged = false;
    private int currentFrame = 0;

    void Start()
    {
        GameController.Instance.cthulhuController.AddObserver(this);
        rt.position = Camera.main.WorldToScreenPoint(HUDPosition.position);
        currentFrame = startingFrame;
        InvokeRepeating("NextFrame", frameDuration, frameDuration);
    }

    void Update()
    {
        if (currentlyBeingDamaged && GameController.Instance.cthulhuController.IsBeingDamaged())
            rt.position = Camera.main.WorldToScreenPoint(HUDPosition.position);
        else
            currentlyBeingDamaged = false;
    }

    private void NextFrame()
    {
        rt.position = Camera.main.WorldToScreenPoint(HUDPosition.position) + ToVector3(offsets[currentFrame]);
        currentFrame++;
        if (currentFrame == offsets.Count)
            currentFrame = 0;
    }

    private Vector3 ToVector3(Vector2 v)
    {
        return new Vector3(v.x, v.y, 0.0f);
    }

    public void OnNotify()
    {
        currentlyBeingDamaged = GameController.Instance.cthulhuController.IsBeingDamaged();
        if (GameController.Instance.IsCthulhuDead())
        {
            CancelInvoke("NextFrame");
            GetComponent<FlashingAndDisappearing>().Flash();
        }
    }

    private void OnValidate()
    {
        if (startingFrame < 0 || startingFrame >= offsets.Count)
            Debug.LogError("CthulhuHUD: Error, starting frame fuera de rango");
    }
}