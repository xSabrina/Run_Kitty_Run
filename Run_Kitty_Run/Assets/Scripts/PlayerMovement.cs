using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float speed;
    Animator animator;
    Rigidbody2D rb;
    
    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        HandleKeyboardInput();
        PlayMovementAnimation();
    }

    private void HandleKeyboardInput()
    {
        //Enable player to move faster while holding shift
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            speed = 20f;
            animator.SetFloat("AnimationSpeed", 2);
        }
        else
        {
            speed = 10f;
            animator.SetFloat("AnimationSpeed", 1);
        }
        //Enable player movement (WASD based)
        if (Input.GetKey(KeyCode.W))
        {

            transform.rotation = Quaternion.Euler(Vector3.zero);
            transform.Translate(0f, speed * Time.deltaTime, 0f);
        }
        if (Input.GetKey(KeyCode.A))
        {

            transform.rotation = Quaternion.Euler(0, 180f, 0);
            transform.Translate(speed * Time.deltaTime, 0f, 0f);
        }
        if (Input.GetKey(KeyCode.D))
        {
    
            transform.rotation = Quaternion.Euler(Vector3.zero);
            transform.Translate(speed * Time.deltaTime, 0f, 0f);
        }
        if (Input.GetKey(KeyCode.S))
        {
            //Adjust player rotation whenever walking downwards diagonally
            if (transform.rotation == Quaternion.Euler(0, 180f, 0))
            {
                transform.rotation = Quaternion.Euler(0, 180f, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(Vector3.zero);
            }
            transform.Translate(0f, -speed * Time.deltaTime, 0f);
        }
    }

    private void PlayMovementAnimation()
    {
        ClearMovementAnimations();
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            animator.SetBool("isWalkingSide", true);
        } else if (Input.GetKey(KeyCode.W))
        {
            animator.SetBool("isWalkingUp", true);
        } else if (Input.GetKey(KeyCode.S)){
            animator.SetBool("isWalkingDown", true);
        }
    }

    private void ClearMovementAnimations()
    {
        animator.SetBool("isWalkingSide", false);
        animator.SetBool("isWalkingUp", false);
        animator.SetBool("isWalkingDown", false);
    }

}
