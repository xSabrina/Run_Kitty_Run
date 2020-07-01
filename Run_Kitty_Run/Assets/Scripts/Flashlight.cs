using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{

    private Animator animator;
    private float angle;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponentInParent<EnemyRotation>().enabled == true)
        {
            angle = GetComponentInParent<EnemyRotation>().angle;
        } 
        else
        {
            angle = GetComponentInParent<EnemyToPlayer>().angle;
        }
        if(angle == 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, -90);
        }
        else if ((angle >= -45 && angle <= 45))
        {
            transform.rotation = Quaternion.Euler(0, 0, -90);
        } 
        else if ((angle < -135 && angle >= -180 || angle <= 180 && angle > 135))
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else if (angle < 135 && angle > 45) 
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        } 
        else if (angle < -45 && angle > -135)
        {
            transform.rotation = Quaternion.Euler(0, 0, 180);
        }
    }
}
