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

    [Space(50)]
    [Header("Enable AI")]
    public bool AIEnabled = false;
    public float wordCooldownRandom = 0.4f;
    private float currentExtraCooldown = 0.0f;

    private float lastShotTime = -999.9f;
    private Animator myAnimator { get { return GetComponent<Animator>(); } }
    private bool dying = false;
    private bool hasStopTouching = true;

    void Start()
    {
        DecideExtraCooldown();
    }

    public bool GetTouchInput()
    {
        return (Input.touchCount > 0 && Input.GetTouch(0).position.x > Screen.width / 2);
    }

    private void DecideExtraCooldown()
    {
        currentExtraCooldown = wordCooldown + Random.Range(-wordCooldownRandom, +wordCooldownRandom);
    }

    void Update()
    {
        bool input;
        if (Application.platform == RuntimePlatform.Android)
            input = GetTouchInput() && hasStopTouching;
        else
            input = Input.GetKeyDown(GameController.Instance.coelhoAttackKey);
        if (AIEnabled)
            input = true;
        if (!dying && !HaveCooldown() && input && GameController.Instance.IsInputEnabled())
            FireWord();

        if (Input.touchCount == 0)
            hasStopTouching = true;
        else
            hasStopTouching = false;
    }

    private bool HaveCooldown()
    {
        return (Time.time - lastShotTime < (wordCooldown + currentExtraCooldown));
    }

    private void FireWord()
    {
        DecideExtraCooldown();
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
