using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    public event Action<MissionObjective> onObjectiveDone; 
    public event Action<Mission> onStartMission; 
    [SerializeField] private Mission currentMission;
    public static MissionManager instance;
    
    private void Awake()
    {
        if (instance == null) instance = this;
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (currentMission != null) StartCoroutine(startMission());
    }

    IEnumerator startMission()
    {
        yield return null;
        StartMission(currentMission);
    }

    public void StartMission(Mission mission)
    {
        onStartMission?.Invoke(mission);
    }
    
    public void DoObjective(MissionType type)
    {
        onObjectiveDone?.Invoke(currentMission.ObjectiveDone(type));
    }
}
