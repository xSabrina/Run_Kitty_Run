using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Abilities/ProjectileAbility")]
public class ProjectileAbility : Ability
{
    public float projectileForce = 100f;
    public float projectileRange = 5f;
    public GameObject projectilePrefab;
    //public Rigidbody2D projectile;

    private GameObject player;
    private Animator animator;
    private AudioSource audioSource;

    private ProjectileShootTriggerable launcher;

    public override void Initialize(GameObject obj)
    {
        launcher = obj.GetComponent<ProjectileShootTriggerable>();
        launcher.projectileForce = projectileForce;
        launcher.projectilePrefab = projectilePrefab;
        launcher.projectileRange = projectileRange;
        player = GameObject.FindGameObjectWithTag("Player");
        animator = player.GetComponent<Animator>();
        audioSource = player.GetComponent<AudioSource>();
    }

    public override void TriggerAbility()
    {
        player.GetComponent<PlayerMovement>().enabled = false;
        PlayShootingAnimation();
        PlayerAbilities.instance.StartCoroutine(WaitCastPoint(castPoint));
    }

    private void PlayShootingAnimation()
    {
        ClearMovementAnimations();
        Vector2 direction = launcher.spawnPoint.transform.up.normalized;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (angle < -225 && angle > -315 || angle < 135 && angle > 45)
        {
            animator.SetTrigger("isShootingUp");
        }
        else if (angle >= -45 && angle <= 45)
        {
            animator.SetTrigger("isShootingSide");
        }
        else if (angle < -45 && angle > -125)
        {
            animator.SetTrigger("isShootingDown");
        }
        else
        {
            player.transform.rotation = Quaternion.Euler(0, 180, 0);
            animator.SetTrigger("isShootingSide");
        }
        
        PlayerAbilities.instance.StartCoroutine(WaitingTime(castTime));
    }

    IEnumerator WaitCastPoint(float Float)
    {
        yield return new WaitForSeconds(Float);
        launcher.Launch();
        audioSource.PlayOneShot(abilitySound, 0.1f);
    }

    IEnumerator WaitingTime(float Float)
    {
        yield return new WaitForSeconds(Float);
        player.GetComponent<PlayerMovement>().enabled = true;
    }

    private void ClearMovementAnimations()
    {
        animator.SetBool("isWalkingSide", false);
        animator.SetBool("isWalkingUp", false);
        animator.SetBool("isWalkingDown", false);
    }
}