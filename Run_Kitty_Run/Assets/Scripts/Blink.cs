using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Abilities/Blink")]
public class Blink : Ability
{
    public float aRange = 0f;

    private SelfCastTriggerable launcher;
    private GameObject player;
    private Animator animator;

    public override void Initialize(GameObject obj)
    {
        launcher = obj.GetComponent<SelfCastTriggerable>();
        player = GameObject.FindGameObjectWithTag("Player");
        animator = player.GetComponent<Animator>();
    }

    public override void TriggerAbility()
    {
        ClearMovementAnimations();

        Transform playerTransform = launcher.player.GetComponent<Transform>();
        playerTransform.position += launcher.spawnPoint.transform.up.normalized * aRange;

        // trigger blink animation and wait for it to finish
        animator.SetTrigger("isBlinking");
        float animTime = animator.GetCurrentAnimatorStateInfo(0).length;
        AbilityCoolDown.instance.StartCoroutine(WaitingTime(animTime));
    }

    IEnumerator WaitingTime(float Float)
    {
        yield return new WaitForSeconds(Float);
        launcher.player.GetComponent<PlayerMovement>().enabled = true;
    }

    private void ClearMovementAnimations()
    {
        animator.SetBool("isWalkingSide", false);
        animator.SetBool("isWalkingUp", false);
        animator.SetBool("isWalkingDown", false);
    }

}