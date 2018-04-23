using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [HideInInspector]
    public CthulhuController cthulhuController;
    [HideInInspector]
    public CoelhoController coelhoController;

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
        Invoke("EnableInput", inputWaitTime);
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
        inputEnabled = false;
    }

    public void KillCoelho()
    {
        inputEnabled = false;
        Instantiate(lightningPrefab, this.DynamicTransform).transform.position = coelhoController.headPosition.position;
        AudioController.Instance.PlayThunderStarts();
        AudioController.Instance.StartCoroutine(AudioController.Instance.PlayThunder(0.7f));
        coelhoController.Death();
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
