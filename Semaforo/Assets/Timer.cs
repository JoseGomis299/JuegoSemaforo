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
    [SerializeField] private TMP_Text scoreText;
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

    public void spawntimer(Mission _)
    {
        time.SetActive(true);
        restante = sec;
    }


    private void Start()
    {
        MissionManager.instance.onStartMission += spawntimer;
        time.SetActive(false);
    }

    private void OnDestroy()
    {
        MissionManager.instance.onStartMission -= spawntimer;
    }

    private void Update()
    {
        if (enmarcha)
        {
            restante -= Time.deltaTime;
            if (restante < 1)
            {
                Time.timeScale = 0;
                scoreText.text = "SCORE: " + (MissionManager.instance.GetDifficulty-1);
                over.SetActive(true);
                time.SetActive(false);
            }
            int TempSec = Mathf.FloorToInt(restante % 60);
            tiempo.text = string.Format("{0}", TempSec);
        }
    }

}
