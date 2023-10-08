using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuInicial : MonoBehaviour
{
    public GameObject menuprincipal;
    public void Jugar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        menuprincipal.SetActive(false);
    }
    public void Salir()
    {
        Debug.Log("Salir...");
        Application.Quit();
    }
}

