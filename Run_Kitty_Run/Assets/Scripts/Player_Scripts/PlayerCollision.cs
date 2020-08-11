using System.Collections;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public GameObject aimArrow;

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
            // disable aimArrow on death. Does not need to be activated again
            aimArrow.SetActive(false);
            GameManagerScript.instance.abilitiesEnabled = false;
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
        GameManagerScript.instance.abilitiesEnabled = true;
    }

}
