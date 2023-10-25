using System;
using System.Collections;
using System.Collections.Generic;
using ProjectUtils.ObjectPooling;
using UnityEngine;
using Random = UnityEngine.Random;

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

    [SerializeField] private AudioClip[] spawnSounds;
    [SerializeField] private AudioClip[] dieSounds;

    private Animator anim;
    [SerializeField] private GameObject blood;

    private void Start()
    {
        anim = GetComponent<Animator>();
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
        ObjectPool.Instance.InstantiateFromPool(blood, transform.position, Quaternion.identity);

        // if(character == Characters.Tank) MissionManager.instance.DoObjective(MissionType.KillTank);
        if(character == Characters.Paraxodon) MissionManager.instance.DoObjective(MissionType.KillParaxodon);
        
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        if(spawnSounds.Length > 0)
            AudioManager.Instance.PlaySound(spawnSounds[Random.Range(0, spawnSounds.Length)]);
    }
    
    private void OnDisable()
    {
        if(dieSounds.Length > 0)
            AudioManager.Instance.PlaySound(dieSounds[Random.Range(0, dieSounds.Length)]);
    }
}
