using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void Battle()
    {
        SceneManager.LoadScene("Battle");
    }
    public void Exit()
    {
        Application.Quit();
    }
}
