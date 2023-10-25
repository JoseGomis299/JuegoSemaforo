using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject menu;
    public static bool menuactive = true;
    public GameObject pausa;
    public GameObject misiones;
    public GameObject charOverlay;
    public GameObject cross;
    public GameObject greenLight;
    public GameObject redLight;

    public static GameManager instance;
    public event Action onGameStart;
    public event Action onBeginPlay;

    public int spawnedTanks = 0;
    public int maxTanks = 3;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    public void Jugar()
    {
        menuactive = false;
        menu.SetActive(false);
        misiones.SetActive(true);
        charOverlay.SetActive(true);
        redLight.SetActive(false);
        greenLight.SetActive(true);
        spawnedTanks = 0;
        onGameStart?.Invoke();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        if (onBeginPlay != null)
        {
            onBeginPlay.Invoke();
        }
    }
    
    private void Start()
    {
        if (!menu.activeInHierarchy || menuactive == false)
        {
          Jugar();
        }
    }

    public void Salir()
    {
        Application.Quit();
    }
    private void Update()
    {
        Pausa();

        if (spawnedTanks >= maxTanks)
        {
            cross.SetActive(true);
        }
        else
        {
            cross.SetActive(false);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        pausa.SetActive(false);
        Time.timeScale = 1;
        Jugar();
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

