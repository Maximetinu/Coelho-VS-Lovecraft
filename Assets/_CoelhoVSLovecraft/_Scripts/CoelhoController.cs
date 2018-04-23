using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoelhoController : MonoBehaviour
{

    public float wordCooldown = 0.5f;
    public List<string> coelhoWords;

    [Space(20)]
    public float deathAnimationDelay = 0.5f;

    [Space(20)]
    [Header("References")]
    public Transform wordSpawn;
    public Transform headPosition;
    public GameObject wordProjectilePrefab;

    private float lastShotTime = -999.9f;
    private Animator myAnimator { get { return GetComponent<Animator>(); } }
    private bool dying = false;

    void Update()
    {
        if (!dying && !HaveCooldown() && Input.GetKeyDown(GameController.Instance.coelhoAttackKey) && GameController.Instance.IsInputEnabled())
            FireWord();
    }

    private bool HaveCooldown()
    {
        return (Time.time - lastShotTime < wordCooldown);
    }

    private void FireWord()
    {
        GameObject firingWord = Instantiate(wordProjectilePrefab, GameController.Instance.DynamicTransform);
        firingWord.GetComponent<WordProjectile>().wordText = GetRandomWord();
        firingWord.transform.position = wordSpawn.position;
        firingWord.GetComponent<WordProjectile>().target = GameController.Instance.GetCthulhuTarget();
        lastShotTime = Time.time;

        AudioController.Instance.PlayThrowWord();
        myAnimator.SetTrigger("Attack");
    }

    private string GetRandomWord()
    {
        return coelhoWords[Random.Range(0, coelhoWords.Count - 1)];
    }

    private void OnValidate()
    {
        if (coelhoWords.Count == 0)
            Debug.LogError("Error: Coelho no tiene palabras.");
    }

    private void RunDeathAnimation()
    {
        myAnimator.SetTrigger("Death");
    }

    public void Death()
    {
        Invoke("RunDeathAnimation", deathAnimationDelay);
        this.dying = true;
    }
}
