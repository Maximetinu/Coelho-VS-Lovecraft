using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingMusicController : MonoBehaviour
{

    private AudioSource myAudioSource { get { return GetComponent<AudioSource>(); } }
    public float fadeInTime = 5f;
    public float fadeOutTime = 10f;

    void Start()
    {
        this.FadeIn();
    }

    public void SetNewSong(AudioClip song)
    {
        myAudioSource.clip = song;
    }

    public void FadeIn()
    {
        StartCoroutine(FadeIn(fadeInTime));
    }

    public void FadeOut()
    {
        StartCoroutine(FadeOut(fadeOutTime));
    }

    IEnumerator FadeIn(float duration)
    {
        float start = Time.time;
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            elapsed = Time.time - start;
            float normalisedTime = Mathf.Clamp(elapsed / duration, 0, 1);
            myAudioSource.volume = Mathf.Lerp(0.0f, 1.0f, normalisedTime);
            yield return null;
        }
    }

    IEnumerator FadeOut(float duration)
    {
        float start = Time.time;
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            elapsed = Time.time - start;
            float normalisedTime = Mathf.Clamp(elapsed / duration, 0, 1);
            myAudioSource.volume = Mathf.Lerp(1.0f, 0.0f, normalisedTime);
            yield return null;
        }
    }
}
