using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CthulhuController : SubjectMonoBehaviour
{
    [Header("Main Properties")]
    public float maxHealthPoints = 100.0f;
    public float hitHPDamage = 10.0f;
    public float destroyDelay = 0.1f;

    [Space(5)]
    public float neededRagePoints = 100.0f;
    public float rageIncrement = 10.0f;
    public float lightningDelay = 2.0f;

    [Space(5)]
    public float defenseCooldown = .5f;
    public float cooldownRandom = 0.5f;

    [Space(10)]
    [Header("Damaging Effect Properties")]
    public float damageEffectTime = 0.5f;
    public float damageShakes = 4;
    public float shakeAmount = 1.0f;
    public float decreaseFactor = 1.0f;

    [Space(20)]
    [Header("Referencies")]
    public Transform coelhoTarget;
    public Transform defenseVertical;

    [Space(50)]
    [Header("Enable AI?")]
    public bool AIEnabled = false;
    public Transform AimingAI;

    private SpriteRenderer mySpriteRenderer { get { return GetComponent<SpriteRenderer>(); } }
    private Animator myAnimator { get { return GetComponent<Animator>(); } }
    private Color initialColor;
    private float damageCurrentDuration = 0.0f;
    private Vector3 originalPos;
    private float currentHP;
    private float currentRage;
    private bool haveCooldown = false;
    private bool alreadyRaged = false;
    private bool dying = false;
    private bool hasStopTouching = true;

    void Awake()
    {
        initialColor = mySpriteRenderer.color;
        originalPos = transform.position;
        currentHP = maxHealthPoints;
        currentRage = 0.0f;
    }

    public bool GetTouchInput()
    {
        return (Input.touchCount > 0 && Input.GetTouch(0).position.x < Screen.width / 2);
    }

    public float GetCurrentHP()
    {
        return currentHP;
    }

    public float GetCurrentRage()
    {
        return currentRage;
    }

    public bool IsDying()
    {
        return dying;
    }

    protected virtual void Update()
    {
        if (!dying && currentHP <= 0)
            Death();
        else if (dying)
            return;

        if (!alreadyRaged && currentRage >= neededRagePoints)
        {
            AudioController.Instance.StartCoroutine(AudioController.Instance.PlayCthulhuRageStarts());
            AudioController.Instance.StartCoroutine(AudioController.Instance.PlayCthulhuRage());
            myAnimator.SetTrigger("Rage");
            alreadyRaged = true;
            Invoke("KillCoelho", lightningDelay);
        }

        if (DoDefense())
            this.Defense();

        if (IsBeingDamaged())
        {
            transform.localPosition = originalPos + Vector3.right * (Random.insideUnitSphere * shakeAmount).x;
            damageCurrentDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        // Reset damage effect
        {
            ResetDamageEffect();
        }
        if (Input.touchCount == 0)
            hasStopTouching = true;
        else
            hasStopTouching = false;
    }

    private bool DoDefense()
    {
        if (!AIEnabled)
        {
            bool input;
            if (Application.platform == RuntimePlatform.Android)
                input = GetTouchInput() && hasStopTouching;
            else
                input = Input.GetKeyDown(GameController.Instance.cthulhuDefenseKey);
            return (!alreadyRaged && !haveCooldown && input && GameController.Instance.IsInputEnabled());
        }

        else
            return (!alreadyRaged && !haveCooldown && GetAIInput() && GameController.Instance.IsInputEnabled());
    }

    private bool GetAIInput()
    {
        RaycastHit2D[] hits2D = Physics2D.RaycastAll(AimingAI.position, Vector2.down);
        bool targetFound = (hits2D.Length > 0);
        Debug.Log(targetFound);
        return targetFound;
    }

    private void KillCoelho()
    {
        GameController.Instance.KillCoelho();
    }

    private void ResetDamageEffect()
    {
        mySpriteRenderer.color = initialColor;
        damageCurrentDuration = 0f;
        transform.localPosition = originalPos;
    }

    public bool IsBeingDamaged()
    {
        return (damageCurrentDuration > 0);
    }

    private void Defense()
    {
        myAnimator.SetTrigger("Defense");
        AudioController.Instance.PlayCthulhuDefenseWhiplash();
        RaycastHit2D[] hits2D = Physics2D.RaycastAll(defenseVertical.position, Vector2.down);
        foreach (RaycastHit2D hit in hits2D)
        {
            if (hit.collider.tag == "Word")
            {
                hit.collider.GetComponent<WordProjectile>().DestroyWord(destroyDelay);
                this.currentRage += rageIncrement;
                Notify();
            }
        }
        haveCooldown = true;
        Invoke("ResetCooldown", defenseCooldown + Random.Range(-cooldownRandom, cooldownRandom));
    }

    // Another way of implementing cooldown
    private void ResetCooldown()
    {
        this.haveCooldown = false;
    }

    public Vector2 GetCoelhoTarget()
    {
        return coelhoTarget.position;
    }

    private void Death()
    {
        GameController.Instance.CthulhuIsDead();
        AudioController.Instance.StartCoroutine(AudioController.Instance.PlayCthulhuDeath());
        dying = true;
        ResetDamageEffect();
        myAnimator.SetTrigger("Death");
        Notify();
    }

    private void DamageByWord()
    {
        if (alreadyRaged || dying)
            return;
        mySpriteRenderer.color = Color.red;
        this.damageCurrentDuration = damageEffectTime;
        this.currentHP -= this.hitHPDamage;
        AudioController.Instance.PlayCthulhuPain();
        Notify();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Word")
            this.DamageByWord();
    }
}
