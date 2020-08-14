using UnityEngine;

public class EnemyRotation : MonoBehaviour
{
    public Rigidbody2D rb;

    public float angle;

    public Sprite spriteUp;
    public Sprite spriteSide;
    public Sprite spriteDown;

    Animator animator;

    public string dir;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void LateUpdate()
    {
        var direction = rb.velocity;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            if (angle == 0)
            {
                // idle
                transform.rotation = Quaternion.Euler(Vector3.zero);
                switch (dir)
                {
                    case "up":
                        GetComponent<SpriteRenderer>().sprite = spriteUp;
                        break;

                    case "right":
                        GetComponent<SpriteRenderer>().sprite = spriteSide;
                        break;

                    case "down":
                        GetComponent<SpriteRenderer>().sprite = spriteDown;
                        break;

                    case "left":
                        GetComponent<SpriteRenderer>().sprite = spriteSide;
                        transform.rotation = Quaternion.Euler(0, 180, 0);
                        break;
                }
            }
            else if (angle < -225 && angle > -315 || angle < 135 && angle > 45)
            {
                transform.rotation = Quaternion.Euler(Vector3.zero);
                animator.SetBool("isWalkingUp", true);
                dir = "up";
            }
            else if (angle >= -45 && angle <= 45)
            {
                transform.rotation = Quaternion.Euler(Vector3.zero);
                animator.SetBool("isWalkingSide", true);
                dir = "right";
            }
            else if (angle < -45 && angle > -135)
            {
                transform.rotation = Quaternion.Euler(Vector3.zero);
                animator.SetBool("isWalkingDown", true);
                dir = "down";
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                animator.SetBool("isWalkingSide", true);
                dir = "left";
            }
        }
    }
}