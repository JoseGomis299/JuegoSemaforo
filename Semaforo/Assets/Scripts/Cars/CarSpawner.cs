using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ProjectUtils.ObjectPooling;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class CarSpawner : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField]
    private RandomObject[] cars;

    [Header("Spawner config")] 
    [SerializeField]
    private float spawnRate;
    private float _lastSpawn;

    private Vector2 _range;
    
    [Serializable]
    private struct RandomObject
    {
        public float probability;
        public GameObject prefab;
    }
    
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
        GameObject car = SelectCar();
        Car carScript = car.GetComponent<Car>();
        
        do
        {
            spawnPos = new Vector3(transform.position.x, Random.Range(_range.x, _range.y));
            collider = Physics2D.OverlapBox(spawnPos, carScript.Bounds.size, 0);
        } while (collider != null && collider.TryGetComponent(out Car _));

        ObjectPool.Instance.InstantiateFromPool(car, spawnPos, quaternion.identity);
    }

    private GameObject SelectCar()
    {
        float n = Random.value;
        float value = 1;
        
        for (int i = 0; i < cars.Length-1; i++)
        {
            value -= cars[i].probability;
            if (n >= value) return cars[i].prefab;
        }
        return cars[^1].prefab;
    }
}
