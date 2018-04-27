using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    // Singleton
    private static MainMenuController m_instance;
    public static MainMenuController Instance { get { return m_instance; } }

    public enum SelectedCharacter
    {
        NONE,
        CTHULHU,
        COELHO,
        BOTH
    }

    public SelectedCharacter currentCharacter = SelectedCharacter.NONE;
    public float videoAITime = 60.0f;
    public Button playButton;
    public AudioClip MouseClick;
    public AudioClip MouseHover;

    void Awake()
    {
        if (m_instance == null)
        {
            m_instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Invoke("RunAIVideo", videoAITime);
    }

    void Update()
    {
        if (currentCharacter == SelectedCharacter.NONE)
        {
            playButton.interactable = false;
        }
        else
        {
            playButton.interactable = true;
        }

        if (Input.anyKey || MouseMoved())
        {
            CancelInvoke("RunAIVideo");
            Invoke("RunAIVideo", videoAITime);
        }
    }

    private bool MouseMoved()
    {
        return (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0);
    }

    public void CthulhuSelected()
    {
        if (currentCharacter == SelectedCharacter.COELHO)
            currentCharacter = SelectedCharacter.BOTH;
        else
            currentCharacter = SelectedCharacter.CTHULHU;
    }

    public void CoelhoSelected()
    {
        if (currentCharacter == SelectedCharacter.CTHULHU)
            currentCharacter = SelectedCharacter.BOTH;
        else
            currentCharacter = SelectedCharacter.COELHO;
    }

    public void CthulhuUnselected()
    {
        if (currentCharacter == SelectedCharacter.BOTH)
            currentCharacter = SelectedCharacter.COELHO;
        else
            currentCharacter = SelectedCharacter.NONE;
    }

    public void CoelhoUnselected()
    {
        if (currentCharacter == SelectedCharacter.BOTH)
            currentCharacter = SelectedCharacter.CTHULHU;
        else
            currentCharacter = SelectedCharacter.NONE;
    }

    private void RunAIVideo()
    {
        SceneManager.LoadScene("BattleBothAI");
    }

    public void Battle()
    {
        switch (currentCharacter)
        {
            case (SelectedCharacter.BOTH):
                SceneManager.LoadScene("BattleMultiplayer");
                break;
            case (SelectedCharacter.COELHO):
                SceneManager.LoadScene("BattleCoelhoPlayer");
                break;
            case (SelectedCharacter.CTHULHU):
                SceneManager.LoadScene("BattleCthulhuPlayer");
                break;
            default:
                break;
        }

    }
    public void Exit()
    {
        Application.Quit();
    }
    public void PlayClickAudio()
    {
        GetComponent<AudioSource>().PlayOneShot(MouseClick);
    }
    public void PlayHoverAudio()
    {
        GetComponent<AudioSource>().PlayOneShot(MouseHover);
    }
}
