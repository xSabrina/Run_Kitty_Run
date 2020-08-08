using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    public float lifeTime = 3f;
    public Rigidbody2D rb;
    public Animation bulletAnim;
    public AnimationClip bulletAnimClip;
    public AnimationClip bulletDestroyClip;
    private Vector3 target;
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyBullet", lifeTime - bulletDestroyClip.length);
    }

    // Displays bullet animation and sets bullet speed and target
    public void FireBullet(Vector3 target, float speed)
    {
        bulletAnim.Play();
        this.target = target;
        this.speed = speed;
        Invoke("BulletFired", bulletAnimClip.length);
    }

    //shoots bullet at specified target with specified speed
    public void BulletFired()
    {
        rb.velocity = (target - gameObject.transform.position).normalized * speed;
    }

    //destroys bullet after playing its destroy animation
    public void DestroyBullet()
    {
        bulletAnim.Play(bulletDestroyClip.name);
        Destroy(gameObject, bulletDestroyClip.length);
    }

    //Destroy Bullet when it hits the player (Bullet only collides with player collider)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject, 0.03f);
    }
}
