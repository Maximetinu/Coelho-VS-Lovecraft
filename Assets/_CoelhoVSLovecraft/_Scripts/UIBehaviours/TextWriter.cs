using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextWriter : MonoBehaviour
{

    public List<string> quotes;

    public float letterWait = 0.1f;
    public float pointWait = 0.2f;
    public float initialWait = 4.0f;

    private TextMeshProUGUI myText { get { return GetComponent<TextMeshProUGUI>(); } }
    private string initialText;
    private int currentLetter = 0;

    public GameObject returnToMenuPrefab;
    public Transform returnToMenuTransform;

    // Use this for initialization
    void Start()
    {
        if (quotes.Count > 0)
        {
            initialText = quotes[Random.Range(0, quotes.Count - 1)];
            initialText += "\n\nPaulo Coelho.";
        }
        else
            initialText = myText.text;
        myText.text = "";
        Invoke("PrintNextLetter", initialWait);
    }

    private void PrintNextLetter()
    {
        myText.text = myText.text + initialText.ToCharArray()[currentLetter];
        currentLetter++;
        // Play audio?
        if (currentLetter >= initialText.Length)
            Invoke("ReturnToMenu", pointWait * 2);
        else if (initialText.ToCharArray()[currentLetter - 1] == ".".ToCharArray()[0])
            Invoke("PrintNextLetter", pointWait);
        else if (initialText.ToCharArray()[currentLetter - 1] == ",".ToCharArray()[0] || initialText.ToCharArray()[currentLetter - 1] == ":".ToCharArray()[0])
            Invoke("PrintNextLetter", pointWait / 2);
        else
            Invoke("PrintNextLetter", letterWait);
    }

    private void ReturnToMenu()
    {
        Instantiate(returnToMenuPrefab, transform).transform.position = returnToMenuTransform.transform.position;
    }
}
