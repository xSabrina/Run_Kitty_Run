using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpreadShoot : MonoBehaviour
{
    public float shootingInterval = 4f;
    public float bulletSpeed;
    private Transform player;
    public GameObject bullet;
    public GameObject bulletPrefab;
    public float spreadMagnitude;
    public int bulletNumber;

    // Start is called before the first frame update
    void Start()
    {

    }

    //Launch if player entered collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
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

    }
}
