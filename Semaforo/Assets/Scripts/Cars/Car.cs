using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] protected float speed;
    protected Vector3 direction;

    [Header("AI")] 
    [SerializeField] protected LayerMask carLayer;
    [SerializeField] protected float maxDistanceCheck = 2f;
    [SerializeField] protected float minDistance = 0.2f;

    [Header("Sounds")] 
    [SerializeField] protected AudioClip movingSound;
    [SerializeField] protected AudioClip crashSound;

    protected virtual void SetDirection()
    {
        direction = Vector3.right;
    }

    protected virtual float GetSpeed()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position+transform.right*minDistance, transform.right, maxDistanceCheck, carLayer);
        if (hit)
        {
            return speed * (hit.distance / maxDistanceCheck);
        }
        
        return speed;
    }

    private void Move()
    {
        SetDirection();
        transform.right = Vector3.Lerp(transform.right, direction, Time.deltaTime * 10);    
        transform.position += direction * (GetSpeed() * Time.deltaTime);
    }

    protected void Start()
    {
        minDistance += GetComponent<SpriteRenderer>().bounds.extents.x;
    }
    
    protected void Update()
    {
        Move();
    }
    
    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position+transform.right*minDistance, transform.position+transform.right*maxDistanceCheck);
    }
}
