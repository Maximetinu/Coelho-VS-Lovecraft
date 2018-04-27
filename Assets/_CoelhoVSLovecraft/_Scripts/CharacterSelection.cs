using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterSelection : MonoBehaviour
{
    public UnityEvent onSelect;
    public UnityEvent onUnselect;
    private bool currentlySelected = false;

    void Start()
    {
        GetComponent<SpriteRenderer>().color = new Color(0.3f, 0.3f, 0.3f, 0.3f);
        GetComponent<Animator>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1)
        {
            Vector3 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            Vector2 touchPos = new Vector2(wp.x, wp.y);
            if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos))
            {
                currentlySelected = !currentlySelected;
                if (currentlySelected)
                {
                    onSelect.Invoke();
                    GetComponent<SpriteRenderer>().color = Color.white;
                    GetComponent<Animator>().enabled = true;
                }
                else
                {
                    onUnselect.Invoke();
                    GetComponent<SpriteRenderer>().color = new Color(0.3f, 0.3f, 0.3f, 0.3f);
                    GetComponent<Animator>().enabled = false;
                }
            }
        }
    }
}
