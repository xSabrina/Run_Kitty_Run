using UnityEngine;
using System.Collections;

public class ProjectileShootTriggerable : MonoBehaviour
{
    //[HideInInspector] public Rigidbody2D projectile;            // Rigidbody variable to hold a reference to our projectile prefab
    [HideInInspector] public GameObject projectilePrefab;       // GameObject variable to hold a reference to our projectile prefab
    public Transform spawnPoint;                                // Transform variable to hold the location where we will spawn our projectile
    [HideInInspector] public float projectileForce = 250f;      // Float variable to hold the amount of force which we will apply to launch our projectiles
    [HideInInspector] public float projectileRange = 5f;

    Vector2 startPosition;
    Vector2 currentPosition;
    GameObject bullet;
    Rigidbody2D rbBullet;

    private void Update()
    {
        if(bullet)
        {
            currentPosition = rbBullet.position;
            float traveledDistance = (currentPosition - startPosition).magnitude;
            if (traveledDistance >= projectileRange)
            {
                Destroy(bullet);
            }
        }
    }

    public void Launch()
    {
        //Instantiate a copy of our projectile and store it in a new rigidbody variable called clonedBullet
        bullet = Instantiate(projectilePrefab, spawnPoint.position, spawnPoint.rotation);
        rbBullet = bullet.GetComponent<Rigidbody2D>();
        startPosition = rbBullet.position;

        //Add force to the instantiated bullet, pushing it forward away from the bulletSpawn location, using projectile force for how hard to push it away
        rbBullet.AddForce(spawnPoint.transform.up * projectileForce);
    }
}
