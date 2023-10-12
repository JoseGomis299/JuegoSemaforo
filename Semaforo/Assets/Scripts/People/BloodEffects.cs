using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectUtils.ObjectPooling;
using Random = UnityEngine.Random;
using Unity.Mathematics;

public class BloodEffects : MonoBehaviour
{
    [SerializeField] private Sprite[] bloodStains;
    [SerializeField] private GameObject bloodParticle;
    [SerializeField] private GameObject bloodDeco;

    private float waitingTime;
    private float startEffectTime;

    private SpriteRenderer sprite;

    private Vector2 spawnPos = Vector2.zero;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    public void SpawnDeathEffects(Vector2 pos)
    {
        spawnPos = pos;
        
        GameObject blood = ObjectPool.Instance.InstantiateFromPool(bloodParticle, new Vector3(spawnPos.x, spawnPos.y, 2), quaternion.identity);
        waitingTime = blood.GetComponent<ParticleSystem>().totalTime;
        startEffectTime = Time.time;
        StartCoroutine(SpawnBillboard());
    }

    private IEnumerator SpawnBillboard()
    {
        yield return new WaitForSeconds(0.5f);

        float value = Random.Range(0, 3);

        Sprite spriteUse = value switch
        {
            > 2 => bloodStains[2],
            > 1 => bloodStains[1],
            _ => bloodStains[0]
        };
        
        GameObject bloodDecoObj = ObjectPool.Instance.InstantiateFromPool(bloodDeco, spawnPos, quaternion.identity);
        bloodDecoObj.GetComponent<SpriteRenderer>().sprite = spriteUse;
        bloodDecoObj.GetComponentInChildren<SpriteRenderer>().sprite = spriteUse;
        
        sprite.enabled = true;
        gameObject.SetActive(false);
        
    }
}
