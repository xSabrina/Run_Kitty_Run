using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    //shooting interval
    public float shootingInterval = 4f;
    //speed of bullet
    public float bulletSpeed;
    public AnimationClip bulletAnimClip;
    private Transform player;
    public GameObject bullet;
    public GameObject bulletPrefab;
    

    //start launching bullets when player enters trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            
            player = collision.transform;
            InvokeRepeating("LaunchBullet", 1.0f, shootingInterval);

        }
        
    }

    //stop launching bullets when player exits trigger
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            CancelInvoke();
        }
    }

    //launches bullet
    private void LaunchBullet()
    {
       
        bullet = Instantiate(bulletPrefab, transform.position+new Vector3(0, 1.7f),transform.rotation);
        bullet.GetComponent<BulletScript>().FireBullet(player.position,bulletSpeed);
        Invoke("PlaySound", bulletAnimClip.length);
        
    }

    void PlaySound() {
        GetComponent<AudioSource>().Play();
    }
    
}
