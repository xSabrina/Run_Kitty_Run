using UnityEngine;

public class EnemyRotation : MonoBehaviour
{
    public Rigidbody2D rb;

    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void LateUpdate()
    {
        var direction = rb.velocity;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            if (angle == 0)
            {
                // idle
                transform.rotation = Quaternion.Euler(Vector3.zero);
            }
            else if (angle < -225 && angle > -315 || angle < 135 && angle > 45)
            {
                animator.SetBool("isWalkingUp", true);
            }
            else if (angle >= -45 && angle <= 45)
            {
                transform.rotation = Quaternion.Euler(Vector3.zero);
                animator.SetBool("isWalkingSide", true);
            }
            else if (angle < -45 && angle > -125)
            {
                animator.SetBool("isWalkingDown", true);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                animator.SetBool("isWalkingSide", true);
            }
        }
    }
}