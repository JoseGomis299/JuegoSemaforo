using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour
{
    public GameObject menu;
    public static bool menuactive = true;
    public GameObject pausa;
    public GameObject misiones;
    public void Jugar()
    {
        menuactive = false;
        menu.SetActive(false);
        misiones.SetActive(true);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void Start()
    {
        if (menuactive == false)
        {
            menu.SetActive(false);
            misiones.SetActive(true);
        }
    }

    public void Salir()
    {
        Application.Quit();
    }
    private void Update()
    {
        Pausa();
    }
    public void Pausa()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !menuactive)
        {
            Time.timeScale = 1f - Time.timeScale;
            pausa.SetActive(!pausa.activeInHierarchy);
        }
    }
}

