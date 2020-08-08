using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpreadShoot : MonoBehaviour
{
    //shooting interval
    public float shootingInterval = 4f;
    //speed of bullet
    public float bulletSpeed;
    private Transform player;
    public GameObject bullet;
    public GameObject bulletPrefab;
    //number of bullets and their spread magnitude
    public float spreadMagnitude;
    public int bulletNumber;
    public AnimationClip bulletAnimClip;


    //start Launching Bullets when player enters trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player = collision.transform;
            InvokeRepeating("LaunchBullet", 1.0f, shootingInterval);

        }

    }

    //stop launching bullets when player exits collider
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            CancelInvoke();
        }
    }

    //calculates angle to player and schoots a spread of (bulletNBumber) bullets with (spreadMagnitude) angle difference between each bullet 
    private void LaunchBullet()
    {
       
        float x = player.transform.position.x - transform.position.x;
        float y = player.transform.position.y - transform.position.y;

        float angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg-(bulletNumber/2*spreadMagnitude);
       for (int i = 0; i <= bulletNumber-1; i++)
        {
            bullet = Instantiate(bulletPrefab, transform.position + new Vector3(0,1f), transform.rotation);
            bullet.GetComponent<SpreadBulletScript>().FireBullet(angle, bulletSpeed);
            angle += spreadMagnitude;
        }
        Invoke("PlaySound", bulletAnimClip.length);
    }

    void PlaySound()
    {
        GetComponent<AudioSource>().Play();
    }
}
