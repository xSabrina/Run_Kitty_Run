using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyToPlayer : MonoBehaviour
{

    private GameObject player;
    public float speed;
    public float monitoringRadius;
    public float attackingRadius;
    
    private bool isInside;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        animator = GetComponent<Animator>();
        GetComponent<CircleCollider2D>().radius = monitoringRadius;
    }

    // Update is called once per frame
    void Update()
    {
        if (isInside)
        {
            WalkTowardsPlayer();
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        } 
        else
        {
            if (GetComponent<EnemyMovement>().enabled == false && GetComponent<EnemyRotation>().enabled == false)
            {
                ClearMovementAnimations();
                GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
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
        GetComponent<Rigidbody2D>().velocity = Vector3.Normalize(player.transform.position - transform.position) * speed;

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
            GetComponent<CircleCollider2D>().radius = attackingRadius;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            isInside = false;
            GetComponent<CircleCollider2D>().radius = monitoringRadius;
        }
    }

    private void ClearMovementAnimations()
    {
        animator.SetBool("isWalkingSide", false);
        animator.SetBool("isWalkingUp", false);
        animator.SetBool("isWalkingDown", false);
    }
}
