using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float lifeTime = 3f;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        
        GameObject.Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    public void FireBullet(Vector3 target, float speed)
    {
        rb.velocity = (target - gameObject.transform.position).normalized * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject, 0.1f);
    }
}
