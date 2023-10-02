using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DrunkCar : Car
{
    [Header("AI")]
    [SerializeField] private LayerMask streetLayer;

    [Header("Stats")] 
    [SerializeField] private float amplitude;
    [SerializeField] private float frequency;
    [SerializeField] private float changeDirSmoothness = 1;
    private float _frequency;
    private float _amplitude;

    private int _piCount;

    private void OnEnable()
    {
       SetDirection();
        _amplitude = amplitude * Random.Range(1f, 1.25f);
        _frequency = 2 * Mathf.PI * frequency * Random.Range(0.75f, 1.25f);
    }

    protected override void SetDirection()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.up, 1, streetLayer);
        if(hit) direction = Vector2.Lerp(direction, Vector2.right + Vector2.down, Time.deltaTime / changeDirSmoothness);
        hit = Physics2D.Raycast(transform.position, Vector3.down, 1, streetLayer);
        if(hit) direction = Vector2.Lerp(direction, Vector2.right + Vector2.up, Time.deltaTime / changeDirSmoothness);
        else direction = Vector2.Lerp(direction, Vector2.right + Vector2.up * GetCos(), Time.deltaTime / changeDirSmoothness);
        direction.Normalize();
    }

    private float GetCos()
    {
        int loops = (int)(Time.time / Mathf.PI*2);
        if (_piCount < loops)
        {
            _piCount = loops;
            _amplitude = amplitude * Random.Range(0.75f, 1.5f);
            _frequency = 2 * Mathf.PI * frequency * Random.Range(0.8f, 1.5f);
        }
        
        return _amplitude * Mathf.Cos(Time.time * _frequency);
    }
}
