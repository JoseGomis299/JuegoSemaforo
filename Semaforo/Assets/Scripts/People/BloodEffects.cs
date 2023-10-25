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

    private SpriteRenderer _spriteRenderer;
    private SpriteRenderer _childSpriteRenderer;
    public void OnEnable()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _childSpriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        _spriteRenderer.enabled = false;
        _childSpriteRenderer.enabled = false;
        ObjectPool.Instance.InstantiateFromPool(bloodParticle, new Vector3(transform.position.x, transform.position.y, 2), quaternion.identity, true);
        StartCoroutine(SpawnBillboard());
    }

    private IEnumerator SpawnBillboard()
    {
        yield return new WaitForSeconds(0.4f);
        
        float value = Random.Range(0, 3);
        Sprite spriteUse = value switch
        {
            > 2 => bloodStains[2],
            > 1 => bloodStains[1],
            _ => bloodStains[0]
        };
        _childSpriteRenderer.enabled = true;
        _spriteRenderer.enabled = true;
        _spriteRenderer.color = new Color(1, 1, 1,0);
        _childSpriteRenderer.color = new Color(1, 1, 1,0);
        _spriteRenderer.sprite = spriteUse;
        _childSpriteRenderer.sprite = spriteUse;

        for (float t = 1; t <= 10; t++)
        {
            yield return new WaitForSeconds(0.1f / 10);
            _spriteRenderer.color = new Color(1, 1, 1,t/10f);
            _childSpriteRenderer.color = new Color(1, 1, 1,t/10f);
        }
    }
}
