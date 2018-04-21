using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WordProjectile : MonoBehaviour {

	[Header("Main properties")]
	public string wordText = "PLACEHOLDER";
	public float rotationSpeed = 1.0f;
	public float projectileSpeed = 1.0f;
	public Vector2 target; // CTHULU.pos

	[Space(10)]
	[Header("Scaling")]
	public float initialScale = 0.5f;
	public float timeToResetScale = 1.0f;

	[Space(10)]
	[Header("Referencies")]
	public GameObject explosionPrefab;

	private Vector2 direction; // CTHULU.pos - COELHO.pos

	void Start () {
		GetComponent<TextMeshPro>().text = this.wordText;
		this.direction = ToVector3(target) - transform.position;

		this.transform.localScale = Vector2.one * initialScale;
		StartCoroutine(ScaleOverTime(timeToResetScale));
	}

	private IEnumerator ScaleOverTime(float time)
	{
		Vector3 originalScale = transform.localScale;
        Vector3 destinationScale = Vector2.one;
         
        float currentTime = 0.0f;
         
        do
        {
            transform.localScale = Vector3.Lerp(originalScale, destinationScale, currentTime / time);
            currentTime += Time.deltaTime;
            yield return null;
        } while (currentTime <= time);
	}
	
	void Update () {
		transform.position = transform.position + ToVector3(direction).normalized * projectileSpeed * Time.deltaTime;
		transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
	}

	private Vector3 ToVector3(Vector2 vector)
	{
		return new Vector3(vector.x, vector.y, 0.0f);
	}

	private void DestroyWord()
	{
		if (explosionPrefab != null)
			Instantiate(explosionPrefab, GameController.Instance.DynamicTransform).transform.position = transform.position;
		Destroy(gameObject);
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Cthulhu")
			DestroyWord();
    }
}
