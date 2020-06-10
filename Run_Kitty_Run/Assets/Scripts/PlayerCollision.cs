using System.Collections;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Handle enemy collision
        if (collision.gameObject.tag == "Enemy")
        {
            animator.SetBool("isDead", true);
            float animTime = animator.GetCurrentAnimatorClipInfo(0).Length;
            StartCoroutine(WaitingTime(animTime));
            GetComponent<PlayerMovement>().enabled = false;
        }
    }

    IEnumerator WaitingTime(float Float)
    {
        yield return new WaitForSeconds(Float);
        GetComponent<PlayerMovement>().enabled = true;
        animator.SetBool("isDead", false);
        GameManagerScript.instance.StartLevel();
    }

}
