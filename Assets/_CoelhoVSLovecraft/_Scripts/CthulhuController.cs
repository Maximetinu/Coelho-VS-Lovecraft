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

    void Awake()
    {
        initialColor = mySpriteRenderer.color;
        originalPos = transform.position;
        currentHP = maxHealthPoints;
        currentRage = 0.0f;
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

    void Update()
    {
        if (!dying && currentHP <= 0)
            Death();
        else if (dying)
            return;

        if (!alreadyRaged && currentRage >= neededRagePoints)
        {
            myAnimator.SetTrigger("Rage");
            alreadyRaged = true;
            Invoke("KillCoelho", lightningDelay);
        }

        if (!alreadyRaged && !haveCooldown && Input.GetKeyDown(GameController.Instance.cthulhuDefenseKey))
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
        RaycastHit2D hit2D = Physics2D.Raycast(defenseVertical.position, Vector2.down);
        if (hit2D && hit2D.collider.tag == "Word")
        {
            hit2D.collider.GetComponent<WordProjectile>().DestroyWord(destroyDelay);
            this.currentRage += rageIncrement;
            Notify();
        }
        haveCooldown = true;
        Invoke("ResetCooldown", defenseCooldown);
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
        Notify();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Word")
            this.DamageByWord();
    }
}
