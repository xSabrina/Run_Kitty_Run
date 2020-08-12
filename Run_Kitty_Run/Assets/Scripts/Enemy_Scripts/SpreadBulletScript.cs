using UnityEngine;

public class SpreadBulletScript : MonoBehaviour
{
    public float lifeTime = 5f;
    public Rigidbody2D rb;
    public new Animation animation;
    public AnimationClip shootClip;
    public AnimationClip destroyClip;
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyBullet", lifeTime-destroyClip.length);
    }

    //sets bullet speed and direction and plays its animation
    public void FireBullet(float angle, float speed)
    {
        this.speed = speed;
        gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        gameObject.transform.position += transform.right*1;
        animation.Play(shootClip.name);
        Invoke("BulletFired", shootClip.length);
    }

    //shoots bullet in specified direction with specified speed
    public void BulletFired()
    {
        rb.velocity = transform.right * speed;
    }

    //destroys bullet after playing its destroy animation
    private void DestroyBullet() {
      
        animation.Play(destroyClip.name);
        Destroy(gameObject,destroyClip.length);
    }

    //destroys bullet if it hits the player (Bullet only collides with player collider)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject, 0.03f);
    }

}
