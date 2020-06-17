using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    Animator animator;
    SpriteRenderer spriteRenderer;
    public Sprite Up;
    public Sprite Down;
    public Sprite Side;

    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void LateUpdate()
    {
        //Calculate angle between player and cursor position
        var direction = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") || GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("UseSkill"))
        {
            //Adjust player sprite depending on angle
            if (angle < -225 && angle > -315 || angle < 135 && angle > 45)
            {
                spriteRenderer.sprite = Up;
            }
            else if (angle >= -45 && angle <= 45)
            {
                spriteRenderer.sprite = Side;
                transform.rotation = Quaternion.Euler(Vector3.zero);
            }
            else if (angle < -45 && angle > -125)
            {
                spriteRenderer.sprite = Down;
            }
            else
            {
                spriteRenderer.sprite = Side;
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }
}
