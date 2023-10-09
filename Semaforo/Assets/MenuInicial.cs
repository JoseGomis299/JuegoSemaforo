using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour
{
    public GameObject menu;
    public static bool menuactive = true;
    public GameObject pausa;
    public void Jugar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        menuactive = false;
    }
    private void Start()
    {
        if (menuactive == false)
        {
            menu.SetActive(false);
        }
    }

    public void Salir()
    {
        Debug.Log("Salir...");
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

