using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[CreateAssetMenu(menuName = "Abilities/Blink")]
public class Blink : Ability
{
    public float aRange = 0f;

    private SelfCastTriggerable launcher;
    private ContactFilter2D filter = new ContactFilter2D();
    private List<RaycastHit2D> res = new List<RaycastHit2D>();
    private Transform playerTransform;
    private CapsuleCollider2D playerCollider;
    private Animator animator;
    private AudioSource audioSource;

    private float colliderOffsetX;
    private float colliderOffsetY;
    private float colliderSizeHalfX;
    private float colliderSizeHalfY;

    private float blinkDuration = 0.35F;

    public override void Initialize(GameObject obj)
    {
        launcher = obj.GetComponent<SelfCastTriggerable>();
        playerTransform = launcher.player.GetComponent<Transform>();
        playerCollider = launcher.player.GetComponent<CapsuleCollider2D>();
        animator = launcher.player.GetComponent<Animator>();
        audioSource = launcher.player.GetComponent<AudioSource>();

        colliderOffsetX = playerCollider.offset.x * playerTransform.localScale.x;
        colliderOffsetY = playerCollider.offset.y * playerTransform.localScale.y;
        colliderSizeHalfX = playerCollider.size.x * playerTransform.localScale.x / 2;
        colliderSizeHalfY = playerCollider.size.y * playerTransform.localScale.y / 2;
    }

    public override void TriggerAbility()
    {
        launcher.player.GetComponent<PlayerMovement>().enabled = false;
        launcher.player.GetComponent<Collider2D>().enabled = false;
        PlayAnimation();
        PlayerAbilities.instance.StartCoroutine(BlinkingTime(blinkDuration/2));
    }

    void CalculateBlink()
    {
        bool triggered = false;

        // check for borders in blink direction
        int hitint = Physics2D.Raycast(launcher.spawnPoint.position, launcher.spawnPoint.transform.up, filter.NoFilter(), res, aRange); 
        foreach (RaycastHit2D hit in res)
        {
            if(hit.collider.tag == "Border")
            {
                playerTransform.position = PositionAfterBlink(hit.point);
                triggered = true;
                break;
            }
        }

        // no border in blink direction
        if (!triggered)
        {
            Vector2 hitPoint = launcher.spawnPoint.position + launcher.spawnPoint.transform.up.normalized * aRange;
            playerTransform.position = PositionAfterBlink(hitPoint);
        }
    }

    private Vector2 PositionAfterBlink(Vector2 hitPoint)
    {
        // adjust position after blink with player collider offset and size
        Vector2 hitPointToPlayer = (Vector2)playerTransform.position - hitPoint;
        int xSign = Math.Sign(hitPointToPlayer.x);
        int ySign = Math.Sign(hitPointToPlayer.y);
        return hitPoint + new Vector2(-colliderOffsetX + xSign * colliderSizeHalfX, -colliderOffsetX + ySign * colliderSizeHalfY);
    }

    void PlayAnimation ()
    {
        // trigger blink animation and wait for it to finish
        animator.SetTrigger("isBlinking");
        audioSource.PlayOneShot(abilitySound);
        PlayerAbilities.instance.StartCoroutine(WaitingTime(castTime));
    }

    IEnumerator WaitingTime(float Float)
    {
        yield return new WaitForSeconds(Float);
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Die"))
        {
            launcher.player.GetComponent<PlayerMovement>().enabled = true;
            launcher.player.GetComponent<Collider2D>().enabled = true;
        }
    }

    IEnumerator BlinkingTime(float Float)
    {
        yield return new WaitForSeconds(Float);
        CalculateBlink();
    }

    private void ClearMovementAnimations()
    {
        animator.SetBool("isWalkingSide", false);
        animator.SetBool("isWalkingUp", false);
        animator.SetBool("isWalkingDown", false);
    }

}
