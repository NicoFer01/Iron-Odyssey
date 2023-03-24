using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    [SerializeField] private GameObject UIGameplay;
    [SerializeField] private GameObject UIPause;
    [SerializeField] private GameObject UIGameOver;
    public string STATE_MACHINE;
    //Menu, Play, Pause, GameOver
    private void Awake()
    {
        Time.timeScale = 0f;
        STATE_MACHINE = "Bienvenido";
        STATE_MACHINE = "Play";
    }

    void Update()
    {
        if (STATE_MACHINE == "Pause")
        {
            UIGameplay.SetActive(false);
            UIPause.SetActive(true);
        }

        if (STATE_MACHINE == "Play")
        {
            UIPause.SetActive(false);
            UIGameplay.SetActive(true);
            Time.timeScale = 1f;
        }

        if (STATE_MACHINE == "GameOver")
        {
            UIGameplay.SetActive(false);
            UIGameOver.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
