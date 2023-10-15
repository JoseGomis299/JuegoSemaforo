using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using System;

public class TimeController : MonoBehaviour 
{
    public GameObject over;
    public GameObject time;
    [SerializeField] int sec;
    [SerializeField] TextMeshProUGUI tiempo;

    private float restante;
    private bool enmarcha;

    private void Awake()
    {
        restante = sec;
        enmarcha = true;
    }

    public void spawntimer()
    {
        time.SetActive(true);  
    }


    private void Start()
    {
        GameManager.instance.onBeginPlay += spawntimer;
        GameManager.instance.onBeginPlay += onDestroy();

    }

    private Action onDestroy()
    {
        throw new NotImplementedException();
    }

    private void Update()
    {
        if (enmarcha)
        {
            restante -= Time.deltaTime;
            if (restante < 1)
            {
                Time.timeScale = 0;
                over.SetActive(true);
            }
            int TempSec = Mathf.FloorToInt(restante % 60);
            tiempo.text = string.Format("{0}", TempSec);
        }
    }

}
