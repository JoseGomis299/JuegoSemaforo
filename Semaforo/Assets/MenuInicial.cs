using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour
{
    public GameObject menu;
    static bool menuactive=true;
    public void Jugar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        menuactive=false; 
    }
    private void Start()
    {
        if (menuactive==false)
        {
            menu.SetActive(false);
        }
    }

    public void Salir()
    {
        Debug.Log("Salir...");
        Application.Quit();
    }
}

