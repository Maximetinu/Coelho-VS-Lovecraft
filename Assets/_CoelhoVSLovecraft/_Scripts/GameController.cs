using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    // Singleton
    private static GameController m_instance;
    public static GameController Instance { get { return m_instance; } }

    [Header("Game Controls")]
    public KeyCode coelhoAttackKey = KeyCode.K;
    public KeyCode cthulhuDefenseKey = KeyCode.S;

    [Space(5)]
    public float inputWaitTime = 5.0f;
    private bool inputEnabled = false;

    [Space(20)]
    [Header("References")]
    public GameObject lightningPrefab;
    public AudioClip endingMusic;
    [Space(5)]
    public GameObject CoelhoWinsPrefab;
    public GameObject CthulhuWinsPrefab;

    [HideInInspector]
    public CthulhuController cthulhuController;
    [HideInInspector]
    public CoelhoController coelhoController;

    private bool videoAIScene = false;

    public Transform DynamicTransform
    {
        get
        {
            GameObject dynamicGameObject = GameObject.Find("_Dynamic");
            if (dynamicGameObject == null)
                dynamicGameObject = new GameObject("_Dynamic");
            return dynamicGameObject.transform;
        }
    }

    void Start()
    {
        if (coelhoController.AIEnabled && cthulhuController.AIEnabled)
            videoAIScene = true;
        Invoke("EnableInput", inputWaitTime);
    }

    void Update()
    {
        if (!videoAIScene)
            return;
        else if (Input.anyKey)
        {
            MusicController.Instance.FadeOut();
            GetComponent<SceneFadeInOut>().FadeOutToBlack();
            Invoke("ReturnToMainMenu", 5f);
        }

    }

    private void Ending(bool coelhoWins)
    {
        float endingWait;
        float winWait;
        if (coelhoWins)
        {
            endingWait = 14.0f;
            winWait = 16.0f;
        }
        else
        {
            endingWait = 8.0f;
            winWait = 10.0f;
        }
        MusicController.Instance.FadeOut();
        Invoke("EndingWait", endingWait);
        if (coelhoWins)
            Invoke("CoelhoWins", winWait);
        else
            Invoke("CthulhuWins", winWait);
    }

    private void EndingWait()
    {
        GetComponent<SceneFadeInOut>().Image.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        GetComponent<SceneFadeInOut>().FadeOut();
    }

    private void CoelhoWins()
    {
        Instantiate(CoelhoWinsPrefab, GetComponent<SceneFadeInOut>().Image.transform);
    }

    private void CthulhuWins()
    {
        //MusicController.Instance.SetNewSong(endingMusic);
        //MusicController.Instance.FadeIn();
        Instantiate(CthulhuWinsPrefab, GetComponent<SceneFadeInOut>().Image.transform);
    }

    private void EnableInput()
    {
        inputEnabled = true;
    }

    public bool IsInputEnabled()
    {
        return inputEnabled;
    }

    public Vector2 GetCthulhuTarget()
    {
        return cthulhuController.GetCoelhoTarget();
    }

    public bool IsCthulhuDead()
    {
        return cthulhuController.IsDying();
    }

    public void CthulhuIsDead()
    {
        this.Ending(true);
        inputEnabled = false;
    }

    public void KillCoelho()
    {
        this.Ending(false);
        inputEnabled = false;
        Instantiate(lightningPrefab, this.DynamicTransform).transform.position = coelhoController.headPosition.position;
        AudioController.Instance.PlayThunderStarts();
        AudioController.Instance.StartCoroutine(AudioController.Instance.PlayThunder(0.7f));
        coelhoController.Death();
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

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
        cthulhuController = GameObject.FindGameObjectWithTag("Cthulhu").GetComponent<CthulhuController>();
        coelhoController = GameObject.FindGameObjectWithTag("Coelho").GetComponent<CoelhoController>();
    }
}
