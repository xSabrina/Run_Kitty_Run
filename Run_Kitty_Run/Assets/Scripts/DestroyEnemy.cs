using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEnemy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!(collision.gameObject.name == "Player"))
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.name == "Enemy")
        {
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!(collision.gameObject.name == "Player"))
        {
            Destroy(gameObject);
        }

        if(collision.gameObject.name == "Enemy")
        {
            Destroy(collision.gameObject);
        }
    }
}
