using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Characters
{
    Adult, 
    Kid,
    Granny,
    Paraxodon,
    Tank
}

public class Move : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Characters character;
    [SerializeField] private float maxYPos = 5f;

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

            if (character <= Characters.Kid) MissionManager.instance.DoObjective((MissionType) character);
            if(character == Characters.Granny) MissionManager.instance.DoObjective(MissionType.CrossOld);
        }
    }

    void Die()
    {
        blood.SpawnDeathEffects(transform.position);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        
        if(character == Characters.Tank) MissionManager.instance.DoObjective(MissionType.KillTank);
        if(character == Characters.Paraxodon) MissionManager.instance.DoObjective(MissionType.KillParaxodon);
    }
}
