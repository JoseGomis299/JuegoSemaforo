using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] protected float speed;
    protected Vector3 direction;
    protected Vector3 center;
    public Bounds Bounds { get; set; }

    [Header("AI")] 
    [SerializeField] protected LayerMask carLayer;
    [SerializeField] protected float maxDistanceCheck = 2f;
    [SerializeField] protected float minDistance = 0.2f;
    [SerializeField] protected float stopPos;

    [Header("Sounds")] 
    [SerializeField] protected AudioClip movingSound;
    [SerializeField] protected AudioClip crashSound;
    [Range(0, 1)] protected float loudness = 0;

    public bool isRush = false;

    protected virtual void SetDirection()
    {
        direction = Vector3.right;
    }

    protected virtual float GetSpeed()
    {
        if (isRush)
        {
            return 8f;
        }
        
        RaycastHit2D hit = Physics2D.BoxCast(center, Bounds.size/2f, 0, transform.right, minDistance+Bounds.extents.x, carLayer);
        if (hit)
        {
            if (GameManager.menuactive)
            {
                float newSpeed = speed * (hit.distance / (3 * Bounds.size.x));

                if (newSpeed < 0.5f)
                {
                    newSpeed = 0f;
                }

                return newSpeed;
            }
            return speed * (hit.distance / Bounds.size.x);
        }
        
        return speed;
    }

    private void Move()
    {
        SetDirection();
        //transform.right = Vector3.Lerp(transform.right, direction, direction == Vector3.right ? Time.deltaTime *10 : Time.deltaTime * 4);    
        transform.position += direction * (GetSpeed() * Time.deltaTime);

        if (GameManager.menuactive)
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -Mathf.Infinity, stopPos), transform.position.y, 0);
        }
    }

    protected void Start()
    {
        Bounds = GetComponent<SpriteRenderer>().bounds;
        minDistance += Bounds.extents.x;

        if (GetComponent<AudioSource>() != null)
        {
            GetComponent<AudioSource>().volume = 0;
        }
    }
    
    protected void Update()
    {
        center = transform.position + Vector3.up * Bounds.extents.y;
        Move();
        if (GetComponents<AudioSource>() != null)
        {
            foreach(AudioSource AS in GetComponents<AudioSource>())
            {
                if (AS.clip.name == "Motor")
                {
                    AS.volume = Mathf.Clamp(1f - Mathf.Abs(transform.position.x / 8f), 0, 1f);
                }
                else
                {
                    AS.volume = Mathf.Clamp(1f - Mathf.Abs(transform.position.x / 8f), 0, 0.5f);
                }
            }
        }
    }

    protected void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("KillZone"))
        {
            isRush = false;
            gameObject.SetActive(false);
        }   
    }
    
    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(center + transform.right*(minDistance+Bounds.extents.x), Bounds.size);
    }
}
