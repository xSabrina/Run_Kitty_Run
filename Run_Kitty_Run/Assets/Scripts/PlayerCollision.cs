using System.Collections;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private Animator animator;
    private Vector3 startPosition;

    private void Start()
    {
        animator = GetComponent<Animator>();
        startPosition = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Handle enemy collision
        if (collision.gameObject.tag == "Enemy")
        {
            animator.SetBool("isDead", true);
            float animTime = animator.GetCurrentAnimatorClipInfo(0).Length;
            StartCoroutine(WaitingTime(animTime));
        }
    }

    IEnumerator WaitingTime(float Float)
    {
        yield return new WaitForSeconds(Float);
        animator.SetBool("isDead", false);
        transform.position = startPosition;
    }

}
