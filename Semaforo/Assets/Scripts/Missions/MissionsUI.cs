using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MissionsUI : MonoBehaviour
{
    [SerializeField] private GameObject objectiveText;
    private List<MissionObjective> _objectives;
    private Dictionary<MissionType, TMP_Text> _texts;

    private void Start()
    {
        MissionManager.instance.onObjectiveDone += RefreshUI;
        MissionManager.instance.onStartMission += SetUpMission;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        MissionManager.instance.onObjectiveDone -= RefreshUI;
        MissionManager.instance.onStartMission -= SetUpMission;
    }

    private void SetUpMission(Mission mission)
    {
        for (int i = 1; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        
        _texts = new Dictionary<MissionType, TMP_Text>();
        _objectives = mission.GetObjectives();

        foreach (var objective in _objectives)
        {
            TMP_Text ObjText = Instantiate(objectiveText, transform).GetComponent<TMP_Text>();
            _texts.Add(objective.Type, ObjText);

            ObjText.text = GetText(objective);
        }
    }

    private void RefreshUI(MissionObjective objective)
    {
        if (objective != null && _texts.ContainsKey(objective.Type))
        {
            _texts[objective.Type].text = GetText(objective);
        }
    }

    private string GetText(MissionObjective objective)
    { 
        string text = $"{objective.GetDoneQuantity()}/{objective.Needed} "; 
        if (objective.Type == MissionType.CrossKid) text += "JSON crossed the street"; 
        if (objective.Type == MissionType.CrossNormie) text += "JDAD crossed the street";
        if (objective.Type == MissionType.CrossOld) text += "LOLA crossed the street"; 
        if (objective.Type == MissionType.KillParaxodon) text += "PARAXODON killed";
        if (objective.Type == MissionType.KillTank) text += "JUAN killed";
        if(objective.IsCompleted) text = $"<s>{text}</s>";
        return text;
    }
}
