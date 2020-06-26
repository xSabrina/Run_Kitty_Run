using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyToPlayer : MonoBehaviour
{

    public GameObject player;
    public float speed;
    
    private bool isInside;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isInside)
        {
            WalkTowardsPlayer();
        } 
        else
        {
            if (GetComponent<EnemyMovement>().enabled == false && GetComponent<EnemyRotation>().enabled == false)
            {
                GetComponent<EnemyMovement>().enabled = true;
                GetComponent<EnemyRotation>().enabled = true;
            }
        }
    }

    void WalkTowardsPlayer()
    {
        if (GetComponent<EnemyMovement>().enabled == true && GetComponent<EnemyRotation>().enabled == true)
        {
            GetComponent<EnemyMovement>().enabled = false;
            GetComponent<EnemyRotation>().enabled = false;
        }
        var direction = Vector3.MoveTowards(transform.position, player.transform.position, speed);
        transform.position = direction;
        var angle = Mathf.Atan2((player.transform.position - transform.position).y, (player.transform.position - transform.position).x) * Mathf.Rad2Deg;
        if (angle == 0 && !animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            transform.rotation = Quaternion.Euler(Vector3.zero);
        }
        else if ((angle < 135 && angle > 45) && !animator.GetBool("isWalkingUp"))
        {
            ClearMovementAnimations();
            animator.SetBool("isWalkingUp", true);
        }
        else if ((angle >= -45 && angle <= 45) && !animator.GetBool("isWalkingSide"))
        {
            ClearMovementAnimations();
            transform.rotation = Quaternion.Euler(Vector3.zero);
            animator.SetBool("isWalkingSide", true);
        }
        else if ((angle < -45 && angle > -135) && !animator.GetBool("isWalkingDown"))
        {
            ClearMovementAnimations();
            animator.SetBool("isWalkingDown", true);
        }
        else if((angle < -135 && angle >= -180 || angle <= 180 && angle > 135) && !animator.GetBool("isWalkingSide"))
        {
            ClearMovementAnimations();
            transform.rotation = Quaternion.Euler(0, 180, 0);
            animator.SetBool("isWalkingSide", true);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            isInside = true;
            GetComponent<CircleCollider2D>().radius = 0.6f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            isInside = false;
            GetComponent<CircleCollider2D>().radius = 0.4f;
        }
    }

    private void ClearMovementAnimations()
    {
        animator.SetBool("isWalkingSide", false);
        animator.SetBool("isWalkingUp", false);
        animator.SetBool("isWalkingDown", false);
    }
}
