using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] protected float speed;
    protected Vector3 direction;
    public Bounds Bounds { get; set; }

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
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, Bounds.size, 0, transform.right, minDistance+Bounds.extents.x, carLayer);
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
        Bounds = GetComponent<SpriteRenderer>().bounds;
        minDistance += Bounds.extents.x;
    }
    
    protected void Update()
    {
        Move();
    }

    protected void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("KillZone")) gameObject.SetActive(false);   
    }
    
    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + transform.right*(minDistance+Bounds.extents.x), Bounds.size);
    }
}
