using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEnemy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        string collisionName = collision.gameObject.name;
        if (!(collisionName == "Player" || collisionName == "LevelStartTrigger" || collisionName == "LevelEndTrigger"))
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.name == "Enemy")
        {
            Destroy(collision.gameObject);
        }
    }
}
