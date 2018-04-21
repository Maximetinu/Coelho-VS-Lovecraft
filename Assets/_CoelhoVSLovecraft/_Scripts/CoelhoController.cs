using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoelhoController : MonoBehaviour {

	public float wordCooldown = 1.0f;
	public List<string> coelhoWords;

	[Space(20)]
	[Header("References")]
	public Transform wordSpawn;
	public GameObject wordProjectilePrefab;
	
	private float lastShotTime = -999.9f;
	private Animator myAnimator{get{return GetComponent<Animator>();}}
	
	void Update () {
		if (!HaveCooldown() && Input.GetKeyDown(GameController.Instance.coelhoAttackKey))
			FireWord();
	}

	private bool HaveCooldown(){
		return (Time.time - lastShotTime < wordCooldown);
	}

	private void FireWord()
	{
		GameObject firingWord = Instantiate(wordProjectilePrefab, GameController.Instance.DynamicTransform);
		firingWord.GetComponent<WordProjectile>().wordText = GetRandomWord();
		firingWord.transform.position = wordSpawn.position;
		firingWord.GetComponent<WordProjectile>().target = GameController.Instance.cthuluController.GetCoelhoTarget();
		lastShotTime = Time.time;

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
}
