using System.Collections;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private Animator animator;

    private float deathTime = 1.3F;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Handle enemy collision
        if (collision.gameObject.tag == "Enemy")
        {
            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
            animator.SetTrigger("isDead");
            StartCoroutine(WaitingTime(deathTime));
        }
    }

    IEnumerator WaitingTime(float Float)
    {
        yield return new WaitForSeconds(Float);
        GameManagerScript.instance.RestartLevel();
        GetComponent<PlayerMovement>().enabled = true;
        GetComponent<Collider2D>().enabled = true;
    }

}
