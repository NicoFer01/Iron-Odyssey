using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    [SerializeField] private GameObject UIGameplay;
    [SerializeField] private GameObject UIPause;
    [SerializeField] private StateMachine _State;

    public void btnResume()
    {
        Time.timeScale = 1f;
        _State.STATE_MACHINE = "Play";
    }

    public void btnRestart()
    {
        SceneManager.LoadScene("Gameplay");
        _State.STATE_MACHINE = "Play";
        Time.timeScale = 1f;
    }

    public void btnBackToMenu()
    {
        UIPause.SetActive(false);
        UIGameplay.SetActive(false);
        _State.STATE_MACHINE = "Bienvenido";
        SceneManager.LoadScene("Menu");
    }
}
