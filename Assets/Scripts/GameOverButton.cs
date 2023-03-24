using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverButton : MonoBehaviour
{
    [SerializeField] private GameObject UIGameOver;
    [SerializeField] private GameObject UIGameplay;
    [SerializeField] private StateMachine _State;

    public void btnJugarOtraVez()
    {
        SceneManager.LoadScene("Gameplay");
        _State.STATE_MACHINE = "Play";
    }

    public void btnOpciones()
    {
        
    }

    public void btnVolverAlMenu()
    {
        _State.STATE_MACHINE = "Bienvenido";
        SceneManager.LoadScene("Menu");
    }
}
