using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthManager : MonoBehaviour
{
    [SerializeField] private int maxOrderLayer = 11;
    [SerializeField] private float baseOffset = 5.5f;

    private SpriteRenderer sprite;
    
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }
    
    void Update()
    {
        float height = transform.position.y + baseOffset;

        sprite.sortingOrder = Mathf.RoundToInt(Mathf.Lerp(maxOrderLayer * 2, 0, height / (baseOffset * 2))) - maxOrderLayer;
    }
}
