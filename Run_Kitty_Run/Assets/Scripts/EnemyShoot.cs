﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public float shootingInterval = 4f;
    public float bulletSpeed;
    private Transform player;
    public GameObject bullet;
    public GameObject bulletPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Debug.Log("ENtered");
            player = collision.transform;
            InvokeRepeating("LaunchBullet", 1.0f, shootingInterval);

        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            CancelInvoke();
        }
    }
    // Update is called once per frame

    private void LaunchBullet()
    {
        Debug.Log("BulletLaunched");
        bullet = Instantiate(bulletPrefab, transform.position,transform.rotation);
        bullet.GetComponent<BulletScript>().FireBullet(player.position,bulletSpeed);
        Debug.Log(bullet.name);
    }
    
}
