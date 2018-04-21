using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CthulhuController : MonoBehaviour {

	[Header("DamagingProperties")]
	public float damageEffectTime = 0.5f;
	public float damageShakes = 4;
	public float shakeAmount = 1.0f;
	public float decreaseFactor = 1.0f;

	private float damageCurrentDuration = 0.0f;

	[Space(20)]
	[Header("Referencies")]
	public Transform coelhoTarget;

	private SpriteRenderer mySpriteRenderer{get{return GetComponent<SpriteRenderer>();}}
	private Color initialColor;

	private Vector3 originalPos;

	void Start () {
		initialColor = mySpriteRenderer.color;
		originalPos = transform.position;
	}
	
	void Update () {
        if (damageCurrentDuration > 0)
        {
            transform.localPosition = originalPos + Vector3.right * (Random.insideUnitSphere * shakeAmount).x;
            damageCurrentDuration -= Time.deltaTime * decreaseFactor;
        }
        else
		// Reset damage effect
        {
            mySpriteRenderer.color = initialColor;
            damageCurrentDuration = 0f;
            transform.localPosition = originalPos;
        }
	}

	public Vector2 GetCoelhoTarget()
	{
		return coelhoTarget.position;
	}

	private void DamageByWord()
	{
		mySpriteRenderer.color = Color.red;
		this.damageCurrentDuration = damageEffectTime;
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Word")
			this.DamageByWord();
    }
}
