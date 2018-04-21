using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CthulhuController : MonoBehaviour {

	[Header("MainProperties")]
	public float damageFlashingTime = 0.5f;

	[Space(20)]
	[Header("Referencies")]
	public Transform coelhoTarget;

	private SpriteRenderer mySpriteRenderer{get{return GetComponent<SpriteRenderer>();}}
	private Color initialColor;

	// Use this for initialization
	void Start () {
		initialColor = mySpriteRenderer.color;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public Vector2 GetCoelhoTarget()
	{
		return coelhoTarget.position;
	}

	private void DamageByWord()
	{
		Debug.Log("Cthulhu receive damage!");
		mySpriteRenderer.color = Color.red;
		Invoke("ResetColor", damageFlashingTime);
	}
	
	private void ResetColor()
	{
		mySpriteRenderer.color = initialColor;
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Word")
			this.DamageByWord();
    }
}
