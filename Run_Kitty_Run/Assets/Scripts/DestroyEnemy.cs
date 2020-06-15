using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEnemy : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.transform.name);
        if (!(collision.transform.tag == "Player"))
        {
            Destroy(gameObject);
        }

        if (collision.transform.tag == "Enemy")
        {
            Destroy(collision.gameObject);
            
        }
    }
}
