using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// NOT ENOUGH GAME JAM TIME TO IMPLEMENT SERIALIZABLE DICTIONARIES IN UNITY INSPECTOR
public class AudioController : MonoBehaviour
{

    // Singleton
    private static AudioController m_instance;
    public static AudioController Instance { get { return m_instance; } }

    private AudioSource myAudioSource { get { return GetComponent<AudioSource>(); } }

    [SerializeField]
    AudioClip CthulhuDeath, CthulhuRage, CthulhuRageStarts, ClickEffect, HoverEffect, Thunder, ThunderStarts;

    [Space(20)]
    [SerializeField]
    List<AudioClip> CthulhuPain, CthulhuDefenseWhiplash, ThrowWord, WordDamageExplosion, WordDefenseExplosion, WordHit;

    void Awake()
    {
        if (m_instance == null)
        {
            m_instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayWordHit()
    {
        myAudioSource.PlayOneShot(GetRandomClip(WordHit));
    }

    public void PlayWordDefenseExplosion()
    {
        myAudioSource.PlayOneShot(GetRandomClip(WordDefenseExplosion));
    }

    public void PlayWordDamageExplosion()
    {
        myAudioSource.PlayOneShot(GetRandomClip(WordDamageExplosion));
    }

    public void PlayThrowWord()
    {
        myAudioSource.PlayOneShot(GetRandomClip(ThrowWord));
    }

    public void PlayCthulhuPain()
    {
        myAudioSource.PlayOneShot(GetRandomClip(CthulhuPain));
    }

    public void PlayCthulhuDefenseWhiplash()
    {
        myAudioSource.PlayOneShot(GetRandomClip(CthulhuDefenseWhiplash));
    }

    public IEnumerator PlayCthulhuDeath(float delay = 0.0f)
    {
        yield return new WaitForSeconds(delay);
        myAudioSource.PlayOneShot(CthulhuDeath);
    }

    public IEnumerator PlayCthulhuRage(float delay = 0.0f)
    {
        yield return new WaitForSeconds(delay);
        myAudioSource.PlayOneShot(CthulhuRage);
    }

    public IEnumerator PlayCthulhuRageStarts(float delay = 0.0f)
    {
        yield return new WaitForSeconds(delay);
        myAudioSource.PlayOneShot(CthulhuRageStarts);
    }

    public IEnumerator PlayThunder(float delay = 0.0f)
    {
        yield return new WaitForSeconds(delay);
        myAudioSource.PlayOneShot(Thunder);
    }

    public IEnumerator PlayThunderStarts(float delay = 0.0f)
    {
        yield return new WaitForSeconds(delay);
        myAudioSource.PlayOneShot(ThunderStarts);
    }

    public void PlayClickEffect()
    {
        myAudioSource.PlayOneShot(ClickEffect);
    }

    public void PlayHoverEffect()
    {
        myAudioSource.PlayOneShot(HoverEffect);
    }

    private AudioClip GetRandomClip(List<AudioClip> list)
    {
        return list[Random.Range(0, list.Count - 1)];
    }
}
