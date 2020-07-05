using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadBulletScript : MonoBehaviour
{
    public float lifeTime = 3f;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {

        GameObject.Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame

    public void FireBullet(float angle, float speed)
    {
      
        gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        rb.velocity = transform.right * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject, 0.03f);
    }
}
