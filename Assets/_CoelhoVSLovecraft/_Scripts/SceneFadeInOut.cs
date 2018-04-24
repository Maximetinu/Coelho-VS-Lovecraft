using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneFadeInOut : MonoBehaviour
{
    public RawImage Image;
    public float FadeTime = 5.0f;

    void Start()
    {
        this.FadeIn();
    }

    public void FadeIn()
    {
        StartCoroutine(Fade(Image, FadeTime, Color.black, Color.clear));
    }

    public void FadeOut()
    {
        StartCoroutine(Fade(Image, FadeTime, Color.clear, Color.white));
    }

    IEnumerator Fade(RawImage mat, float duration, Color startColor, Color endColor)
    {
        float start = Time.time;
        float elapsed = 0;
        while (elapsed < duration)
        {
            // calculate how far through we are
            elapsed = Time.time - start;
            float normalisedTime = Mathf.Clamp(elapsed / duration, 0, 1);
            mat.color = Color.Lerp(startColor, endColor, normalisedTime);
            // wait for the next frame
            yield return null;
        }
    }

    // void OnValidate()
    // {
    //     if (Image == null)
    //         Debug.LogError("GameController: FadeInOut Image is null");
    // }
}
