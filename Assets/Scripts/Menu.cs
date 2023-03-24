using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Menu : MonoBehaviour
{
    //[SerializeField] private GameObject buttonNewGame;
    [SerializeField] private GameObject UIAjustes;
    [SerializeField] private GameObject UICreditos;

    public void btnJugar()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void btnTienda()
    {
        SceneManager.LoadScene("Casillero");
    }

    public void btnAjustes()
    {
        UIAjustes.SetActive(true);
        Debug.Log("Ajustes activos");
    }

    public void btnAjustesVolver()
    {
        UIAjustes.SetActive(false);
        Debug.Log("Ajustes desactivados");
    }

    public void btnCreditos()
    {
        UICreditos.SetActive(true);
        Debug.Log("Créditos activos");
    }

    public void btnSalir()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; //Editor
        #endif
        Application.Quit(); //Aplicacion construida
        Debug.Log("Quit game");
    }
}
