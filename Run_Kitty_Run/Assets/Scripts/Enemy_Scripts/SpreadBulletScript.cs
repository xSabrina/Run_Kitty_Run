using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadBulletScript : MonoBehaviour
{
    public float lifeTime = 5f;
    public Rigidbody2D rb;
    public Animation animation;
    public AnimationClip animationClip;
    private float speed;
    // Start is called before the first frame update
    void Start()
    {

        GameObject.Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame

    public void FireBullet(float angle, float speed)
    {

        this.speed = speed;
        gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        gameObject.transform.position += transform.right*1;
        animation.Play();
        Invoke("BulletFired", animationClip.length);

    }
    public void BulletFired()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject, 0.03f);
    }
}
