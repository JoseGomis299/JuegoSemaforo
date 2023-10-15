using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ProjectUtils.ObjectPooling;
using Unity.Mathematics;
using UnityEditor;
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
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private int maxMenuCars = 20;

    private Vector2 _range;
    private int carsSpawned = 0;
    private Car[] menuCars;

    private bool lastMenuActive;
    
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

        menuCars = new Car[maxMenuCars];
        lastMenuActive = GameManager.menuactive;
    }

    private void Update()
    {
        if (_lastSpawn + spawnRate < Time.time && (!GameManager.menuactive || carsSpawned < maxMenuCars))
        {
            carsSpawned += 1;
            _lastSpawn = Time.time;
            SpawnCar();
        }

        if (lastMenuActive && !GameManager.menuactive)
        {
            SpeedCars();
            lastMenuActive = false;
        }
    }

    private void SpawnCar()
    {
        GameObject car = SelectCar();
        GameObject spawnedCar = ObjectPool.Instance.InstantiateFromPool(car, GetSpawnPos(true), quaternion.identity);

        if (GameManager.menuactive && carsSpawned <= maxMenuCars)
        {
            menuCars[carsSpawned - 1] = spawnedCar.GetComponent<Car>();
        }
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

    private Vector3 GetSpawnPos(bool usePoints = false)
    {
        return usePoints ? spawnPoints[Random.Range(0, spawnPoints.Length)].position : new Vector3(transform.position.x, Random.Range(_range.x, _range.y));
    }

    private void SpeedCars()
    {
        foreach (Car car in menuCars)
        {
            if (car)
            {
                car.isRush = true;
            }
        }
    }
}
