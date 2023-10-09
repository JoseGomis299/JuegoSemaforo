using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Characters
{
    Adult, 
    Granny,
    Kid,
    Paraxodon,
    Tank
}

public class Move : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Characters character;
    [SerializeField] private float maxYPos = 6f;

    private Animator anim;
    private BloodEffects blood;

    private void Start()
    {
        anim = GetComponent<Animator>();
        blood = GetComponent<BloodEffects>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            anim.speed = 0f;
            return;
        }

        anim.speed = 1f;
        transform.position += new Vector3(0, speed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Car"))
        {
            if (character != Characters.Tank)
            {
                Die();
            }
            else
            {
                collision.gameObject.SetActive(false);
                Die();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (transform.position.y > maxYPos)
        {
            gameObject.SetActive(false);
        }
    }

    void Die()
    {
        blood.SpawnDeathEffects(transform.position);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }
}
