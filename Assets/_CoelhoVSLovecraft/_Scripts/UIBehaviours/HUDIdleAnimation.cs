using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDIdleAnimation : MonoBehaviour
{
    [Header("Animation")]
    public List<Vector2> offsets;
    public float frameDuration;
    public int startingFrame = 0;

    private RectTransform rt { get { return GetComponent<RectTransform>(); } }
    private int currentFrame = 0;

    private Vector2 originalPosition;

    void Start()
    {
        originalPosition = rt.position;
        currentFrame = startingFrame;
        InvokeRepeating("NextFrame", frameDuration, frameDuration);
    }

    private void NextFrame()
    {
        rt.position = ToVector3(originalPosition + offsets[currentFrame]);
        currentFrame++;
        if (currentFrame == offsets.Count)
            currentFrame = 0;
    }

    private Vector3 ToVector3(Vector2 v)
    {
        return new Vector3(v.x, v.y, 0.0f);
    }

}