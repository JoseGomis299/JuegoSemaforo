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

    [SerializeField] private int startingDifficulty;
    private int _difficulty;

    public int GetDifficulty => _difficulty;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        GameManager.instance.onGameStart += Initialize;
    }

    private void OnDestroy()
    {
        GameManager.instance.onGameStart -= Initialize;
    }

    private void Initialize()
    {
        _difficulty = startingDifficulty;
        currentMission = new Mission(_difficulty++);
        
        StartCoroutine(startMission());
    }
    
    IEnumerator startMission()
    {
        yield return null;
        StartMission(currentMission);
    }

    public void EndedCurrentMission()
    {
        currentMission = new Mission(_difficulty++);
        StartMission(currentMission);
    }

    private void StartMission(Mission mission)
    {
        onStartMission?.Invoke(mission);
    }
    
    public void DoObjective(MissionType type)
    {
        Mission current = currentMission;
        MissionObjective objective = currentMission.ObjectiveDone(type);
        
        //Check if we have not completed the current mission before updating visuals
        if(current == currentMission) onObjectiveDone?.Invoke(objective);
    }
}
