using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using Pathfinding;

public class EnemyToPlayer : MonoBehaviour
{

    private GameObject player;
    public float speed;
    public float monitoringRadius;
    public float attackingRadius;
    public float angle;
    
    private bool isInside;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
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
                GetComponent<CapsuleCollider2D>().offset = new Vector2(0, -0.005f);
                GetComponent<CapsuleCollider2D>().size = new Vector2(0.115f, 0.195f);
                GetComponent<AIPath>().enabled = false;
                GetComponent<AIDestinationSetter>().enabled = false;
            }
        }
    }

    void WalkTowardsPlayer()
    {
        if (GetComponent<EnemyMovement>().enabled == true && GetComponent<EnemyRotation>().enabled == true)
        {
            GetComponent<EnemyMovement>().enabled = false;
            GetComponent<EnemyRotation>().enabled = false;
            GetComponent<CapsuleCollider2D>().offset = new Vector2(0, 0);
            GetComponent<CapsuleCollider2D>().size = new Vector2(0.02f, 0.02f);
        }
        //GetComponent<Rigidbody2D>().velocity = Vector3.Normalize(player.transform.position - transform.position) * speed;

        GetComponent<AIPath>().maxSpeed = speed;

        if (GetComponent<AIPath>().enabled == false && GetComponent<AIDestinationSetter>().enabled == false)
        {
            GetComponent<AIPath>().enabled = true;
            GetComponent<AIDestinationSetter>().enabled = true;
        }

        angle = Mathf.Atan2((player.transform.position - transform.position).y, (player.transform.position - transform.position).x) * Mathf.Rad2Deg;
        if ((angle <= 135 && angle > 45) && !animator.GetBool("isWalkingUp"))
        {
            ClearMovementAnimations();
            transform.rotation = Quaternion.Euler(Vector3.zero);
            animator.SetBool("isWalkingUp", true);
            if (transform.childCount == 2)
            {
                transform.GetChild(1).localRotation = Quaternion.Euler(0, 0, 0);
                transform.GetChild(1).localPosition = new Vector3(0.08f, 0.08f, 0);
            }
        }
        else if ((angle >= -45 && angle <= 45) && !animator.GetBool("isWalkingSide"))
        {
            ClearMovementAnimations();
            transform.rotation = Quaternion.Euler(Vector3.zero);
            animator.SetBool("isWalkingSide", true);
            if (transform.childCount == 2)
            {
                transform.GetChild(1).localRotation = Quaternion.Euler(0, 0, 0);
                transform.GetChild(1).localPosition = new Vector3(0.08f, 0.08f, 0);
            }
        }
        else if ((angle < -45 && angle >= -135) && !animator.GetBool("isWalkingDown"))
        {
            ClearMovementAnimations();
            transform.rotation = Quaternion.Euler(Vector3.zero);
            animator.SetBool("isWalkingDown", true);
            if (transform.childCount == 2)
            {
                transform.GetChild(1).localRotation = Quaternion.Euler(0, 0, 0);
                transform.GetChild(1).localPosition = new Vector3(0.08f, 0.08f, 0);
            }
        }
        else if((angle < -135 && angle >= -180 || angle <= 180 && angle > 135) && !animator.GetBool("isWalkingSide"))
        {
            ClearMovementAnimations();
            transform.rotation = Quaternion.Euler(0, 180, 0);
            animator.SetBool("isWalkingSide", true);
            if (transform.childCount == 2)
            {
                transform.GetChild(1).localRotation = Quaternion.Euler(0, -180, 0);
                transform.GetChild(1).localPosition = new Vector3(-0.08f, 0.08f, 0);
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            if(transform.childCount == 2)
            {
                GetComponentInChildren<EdgeCollider2D>().enabled = false;
                GetComponentInChildren<Light2D>().color = new Color(1.0f,0.0f,0.0f,1.0f);
                transform.GetChild(1).gameObject.SetActive(true);
                GetComponent<AudioSource>().Play();
            }
            if(player == null)
            {
                player = collision.gameObject;
            }
            isInside = true;
            GetComponent<CircleCollider2D>().radius = attackingRadius;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            if (transform.childCount == 2)
            {
                GetComponentInChildren<EdgeCollider2D>().enabled = true;
                GetComponentInChildren<Light2D>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                transform.GetChild(1).gameObject.SetActive(false);
            }
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
