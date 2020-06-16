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

    private ProjectileShootTriggerable launcher;

    public override void Initialize(GameObject obj)
    {
        launcher = obj.GetComponent<ProjectileShootTriggerable>();
        launcher.projectileForce = projectileForce;
        launcher.projectilePrefab = projectilePrefab;
        launcher.projectileRange = projectileRange;
        player = GameObject.FindGameObjectWithTag("Player");
        animator = player.GetComponent<Animator>();
    }

    public override void TriggerAbility()
    {
        PlayShootingAnimation();
        launcher.Launch();
    }

    private void PlayShootingAnimation()
    {
        ClearMovementAnimations();
        Vector2 direction = launcher.spawnPoint.transform.up.normalized;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (angle < -225 && angle > -315 || angle < 135 && angle > 45)
        {
            animator.SetTrigger("isShootingUp");
            float animTime = animator.GetCurrentAnimatorStateInfo(0).length;
            AbilityCoolDown.instance.StartCoroutine(WaitingTime(animTime));
        }
        else if (angle >= -45 && angle <= 45)
        {
            animator.SetTrigger("isShootingSide");
            float animTime = animator.GetCurrentAnimatorStateInfo(0).length;
            AbilityCoolDown.instance.StartCoroutine(WaitingTime(animTime));
        }
        else if (angle < -45 && angle > -125)
        {
            animator.SetTrigger("isShootingDown");
            float animTime = animator.GetCurrentAnimatorStateInfo(0).length;
            AbilityCoolDown.instance.StartCoroutine(WaitingTime(animTime));
        }
        else
        {
            player.transform.rotation = Quaternion.Euler(0, 180, 0);
            animator.SetTrigger("isShootingSide");
            float animTime = animator.GetCurrentAnimatorStateInfo(0).length;
            AbilityCoolDown.instance.StartCoroutine(WaitingTime(animTime));
        }
    }

    IEnumerator WaitingTime(float Float)
    {
        Debug.Log(Time.time + ": " + Float);
        yield return new WaitForSeconds(Float);
        player.GetComponent<PlayerMovement>().enabled = true;
        Debug.Log(Time.time + ": movement enabled");
    }

    private void ClearMovementAnimations()
    {
        animator.SetBool("isWalkingSide", false);
        animator.SetBool("isWalkingUp", false);
        animator.SetBool("isWalkingDown", false);
    }

}