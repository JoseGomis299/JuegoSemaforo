using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectUtils.ObjectPooling;

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

    private void Update()
    {
        transform.position += new Vector3(0, speed * Time.deltaTime, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("HEERE");
        if (collision.gameObject.tag == "Car")
        {
            if (character != Characters.Tank)
                Die();
            else
            {
                collision.gameObject.SetActive(false);
            }
        }
    }

    void Die()
    {
        this.gameObject.SetActive(false);
    }
}
