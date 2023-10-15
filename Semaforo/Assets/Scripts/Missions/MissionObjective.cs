using System;
using UnityEngine;

[Serializable]
public class MissionObjective
{
    [SerializeField] private MissionType type;
    public MissionType Type => type;
    [SerializeField] private int needed;
    public int Needed => needed;
    private int _count;
    public bool IsCompleted => _count >= needed;

    public MissionObjective(MissionType type, int needed)
    {
        this.type = type;
        this.needed = needed;
    }

    public void AddToNeeded()
    {
        needed++;
    }

    public bool CompleteObjective(int value = 1)
    {
        _count += value;
        return _count >= needed;
    }
    
    public int GetDoneQuantity()
    {
        return _count;
    }
    
}
