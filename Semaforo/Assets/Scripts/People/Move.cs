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

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
            return;

        transform.position += new Vector3(0, speed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Car")
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

    void Die()
    {
        gameObject.SetActive(false);
    }
}
