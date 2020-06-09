using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Abilities/ProjectileAbility")]
public class ProjectileAbility : Ability
{
    public float projectileForce = 100f;
    public float projectileRange = 5f;
    public GameObject projectilePrefab;
    //public Rigidbody2D projectile;

    private ProjectileShootTriggerable launcher;

    public override void Initialize(GameObject obj)
    {
        launcher = obj.GetComponent<ProjectileShootTriggerable>();
        launcher.projectileForce = projectileForce;
        launcher.projectilePrefab = projectilePrefab;
        launcher.projectileRange = projectileRange;
    }

    public override void TriggerAbility()
    {
        launcher.Launch();
    }

}