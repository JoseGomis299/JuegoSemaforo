using System;
using System.Collections;
using System.Collections.Generic;
using ProjectUtils.ObjectPooling;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class CarSpawner : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField]
    private GameObject[] cars;

    [Header("Spawner config")] 
    [SerializeField]
    private float spawnRate;
    private float _lastSpawn;

    private Vector2 _range;
    private void Start()
    {
        _range = new Vector2(transform.GetChild(0).position.y, transform.GetChild(1).position.y);
        _lastSpawn = float.MinValue;
    }

    private void Update()
    {
        if (_lastSpawn + spawnRate < Time.time)
        {
            _lastSpawn = Time.time;
            SpawnCar();
        }
    }

    private void SpawnCar()
    {
        Vector3 spawnPos;
        Collider2D collider;
        int carIndex = SelectCar();
        Car car = cars[carIndex].GetComponent<Car>();
        
        do
        {
            spawnPos = new Vector3(transform.position.x, Random.Range(_range.x, _range.y));
            collider = Physics2D.OverlapBox(spawnPos, car.Bounds.size, 0);
        } while (collider != null && collider.TryGetComponent(out Car _));

        ObjectPool.Instance.InstantiateFromPool(cars[carIndex].gameObject, spawnPos, quaternion.identity);
    }

    private int SelectCar()
    {
        float n = Random.value;
        if (n > 0.85) return 3;
        if (n > 0.7) return 2;
        return n > 0.4 ? 1 : 0;
    }
}
