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

    [HideInInspector]
    public CthulhuController cthulhuController;

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
    }
}
