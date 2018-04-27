using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterSelection : MonoBehaviour
{
    public UnityEvent onSelect;
    public UnityEvent onUnselect;

    private bool currentlySelected = false;
    private bool touchEnabled = true;

    void Start()
    {
        GetComponent<SpriteRenderer>().color = new Color(0.3f, 0.3f, 0.3f, 0.3f);
        GetComponent<Animator>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1 && touchEnabled)
        {
            Vector3 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            Vector2 touchPos = new Vector2(wp.x, wp.y);
            if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos))
            {
                ToggleSelect();
                touchEnabled = false;
                Invoke("RestoreTouch", 2.0f);
            }
        }
    }

    private void RestoreTouch()
    {
        touchEnabled = true;
    }

    private void ToggleSelect()
    {
        currentlySelected = !currentlySelected;
        if (currentlySelected)
        {
            Select();
        }
        else
        {
            Unselect();
        }
    }

    private void Select()
    {
        onSelect.Invoke();
        GetComponent<SpriteRenderer>().color = Color.white;
        GetComponent<Animator>().enabled = true;
    }

    private void Unselect()
    {
        onUnselect.Invoke();
        GetComponent<SpriteRenderer>().color = new Color(0.3f, 0.3f, 0.3f, 0.3f);
        GetComponent<Animator>().enabled = false;
    }

    private void Hover()
    {
        GetComponent<SpriteRenderer>().color = new Color(0.7f, 0.7f, 0.7f, 0.7f);
        GetComponent<Animator>().enabled = true;
    }

    private void UnHover()
    {
        GetComponent<SpriteRenderer>().color = new Color(0.3f, 0.3f, 0.3f, 0.3f);
        GetComponent<Animator>().enabled = false;
    }

    void OnMouseDown()
    {
        ToggleSelect();
        MainMenuController.Instance.PlayClickAudio();
    }

    void OnMouseEnter()
    {
        if (currentlySelected)
            return;
        MainMenuController.Instance.PlayHoverAudio();
        Hover();
    }

    void OnMouseExit()
    {
        if (currentlySelected)
            return;
        UnHover();
    }
}
