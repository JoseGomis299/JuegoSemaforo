using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Mission
{
    [SerializeField] private List<MissionObjective> objectives;
    
    public Mission(int difficulty)
    {
        objectives = new List<MissionObjective>();
        
        //TODO
    }

    public MissionObjective ObjectiveDone(MissionType type)
    {
        MissionObjective objective = objectives.Find(x => x.Type == type);
        if(objective == null) return null;
        
        if(objective.CompleteObjective()) CompleteObjective(objective);
        return objective;
    }

    private void CompleteObjective(MissionObjective objective)
    {
        //TODO
    }

    public List<MissionObjective> GetObjectives() => objectives;
}
