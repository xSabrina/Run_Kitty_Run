using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEnemy : MonoBehaviour
{
    public GameObject death;
    private void Start()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

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
            Instantiate(death, collision.transform.position, collision.transform.rotation);
        }
    }

}
