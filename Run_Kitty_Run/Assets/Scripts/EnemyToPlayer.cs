using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyToPlayer : MonoBehaviour
{

    public GameObject player;
    public float speed;

    private Animator animator;
    private GameObject enemyLight;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        enemyLight = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyLight.GetComponent<EnemyLight>().isInside)
        {
            WalkTowardsPlayer();
        } 
        else
        {
            GetComponent<EnemyMovement>().enabled = true;
            GetComponent<EnemyRotation>().enabled = true;
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
        ClearMovementAnimations();
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            if (angle == 0)
            {
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

    private void ClearMovementAnimations()
    {
        animator.SetBool("isWalkingSide", false);
        animator.SetBool("isWalkingUp", false);
        animator.SetBool("isWalkingDown", false);
    }
}
