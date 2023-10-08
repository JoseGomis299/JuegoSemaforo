using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using ProjectUtils.ObjectPooling;
using Random = UnityEngine.Random;
using Unity.Mathematics;

public class PeopleSpawner : MonoBehaviour
{
    [Header("PREFABS AND INPUTS")]
    [SerializeField]
    private PeopleInput[] people;
    
    [Header("Spawner Info")] [SerializeField]
    private Transform tankSpawn;
    [SerializeField] private Transform leftLimitSpawn;
    [SerializeField] private Transform rightLimitSpawn;

    [SerializeField]
    private float SpawnCountdown = .5f;

    private float lastSpawn;

    private Vector2 tankSpawnPos;
    private Vector2 leftLimitSpawnPos;
    private Vector2 rightLimitSpawnPos;
    
    [Serializable]
    private struct PeopleInput
    {
        public KeyCode input;
        public GameObject prefab;
    }
    
    void Start()
    {
        tankSpawnPos = tankSpawn.position;
        leftLimitSpawnPos = leftLimitSpawn.position;
        rightLimitSpawnPos = rightLimitSpawn.position;

        lastSpawn = float.MinValue;
    }
    
    
    void Update()
    {
        if (lastSpawn + SpawnCountdown < Time.time)
        {
            CheckInput();
        }
    }

    private void CheckInput()
    {
        foreach (PeopleInput person in people)
        {
            if (!Input.GetKeyDown(person.input)) continue;
            SpawnPerson(person.prefab);
            lastSpawn = Time.time;
        }
    }

    private void SpawnPerson(GameObject personPrefab)
    {
        ObjectPool.Instance.InstantiateFromPool(personPrefab, GetSpawnPos(personPrefab), quaternion.identity);
    }

    private Vector2 GetSpawnPos(GameObject person)
    {
        if (person.CompareTag("Tank"))
        {
            return tankSpawnPos;
        }

        float x = Random.value;

        return Vector2.Lerp(leftLimitSpawnPos, rightLimitSpawnPos, x);
    }
}
