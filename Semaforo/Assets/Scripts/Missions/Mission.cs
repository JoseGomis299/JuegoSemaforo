using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class Mission
{
    [SerializeField] private List<MissionObjective> objectives;
    
    public Mission(int difficulty)
    {
        objectives = new List<MissionObjective>();

        int n = difficulty;
        for (int i = 0; i <= n; i++)
        {
            MissionType type = GetMissionType(difficulty);
            MissionObjective objective = objectives.Find(x => x.Type == type);
            if (objective == null) objectives.Add(new MissionObjective(type, 1));
            else objective.AddToNeeded();
        }
    }

    public MissionObjective ObjectiveDone(MissionType type)
    {
        MissionObjective objective = objectives.Find(x => x.Type == type);
        if(objective == null) return null;
        
        if(objective.CompleteObjective()) CompleteObjective();
        return objective;
    }

    private void CompleteObjective()
    {
        if (objectives.All(x => x.IsCompleted))
        {
            MissionManager.instance.EndedCurrentMission();
        }
    }

    public List<MissionObjective> GetObjectives() => objectives;
    
    private MissionType GetMissionType(int difficulty)
    {
        if (difficulty < 1) return 0;

        float n = Random.value;
        float value = 1;
        float last = 0.5f;
        
        for (int i = 0; i < 4; i++)
        {
            value -= last;
            if (n >= value) return (MissionType) i;
            if(difficulty > i+1) last /= 2;
        }
        return 0;
    }
}
