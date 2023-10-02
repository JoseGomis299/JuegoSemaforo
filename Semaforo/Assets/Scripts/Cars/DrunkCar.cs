using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DrunkCar : Car
{
    [Header("Stats")] [SerializeField] private float sineAmplitude;
    [SerializeField] private float sineFrequency;
    private float _sineFrequency;
    private float _sineAmplitude;

    //private int piCount;
    private void OnEnable()
    {
        _sineAmplitude = sineAmplitude * Random.Range(1f, 1.25f);
        _sineFrequency = 2 * Mathf.PI * sineFrequency * Random.Range(0.75f, 1.25f);
    }

    protected override Vector2 GetDirection() => base.GetDirection() + Vector2.up * GetSin();

    private float GetSin()
    {
        // int loops = (int)(Time.time / Mathf.PI*2);
        // if (piCount < loops)
        // {
        //     piCount = loops;
        //     _sineAmplitude = sineAmplitude * Random.Range(0.7f, 1.3f);
        //     _sineFrequency = 2 * Mathf.PI * sineFrequency * Random.Range(0.8f, 1.2f);
        // }
        
        return _sineAmplitude * Mathf.Cos(Time.time * _sineFrequency);
    }
}
