using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

    public override void Initialize(GameObject obj)
    {
        launcher = obj.GetComponent<SelfCastTriggerable>();
        playerTransform = launcher.player.GetComponent<Transform>();
        playerCollider = launcher.player.GetComponent<CapsuleCollider2D>();
        animator = launcher.player.GetComponent<Animator>();
        audioSource = launcher.player.GetComponent<AudioSource>();
    }

    public override void TriggerAbility()
    {
        launcher.player.GetComponent<PlayerMovement>().enabled = false;
        PlayAnimation();
        PlayerAbilities.instance.StartCoroutine(BlinkingTime(0.15F));
    }

    void CalculateBlink(){
        bool triggered = false;
        Transform playerTransform = launcher.player.GetComponent<Transform>();
        CapsuleCollider2D playerCollider = launcher.player.GetComponent<CapsuleCollider2D>();
        int hitint = Physics2D.Raycast(playerTransform.position, launcher.spawnPoint.transform.up, filter.NoFilter(), res, aRange);
        float adjustedBlinkRange;
        foreach(RaycastHit2D hit in res)
        {
            if(hit.collider.tag == "Border")
            {
                Debug.Log(hit.collider.name);
                // blink direction is downwards
                if (launcher.spawnPoint.transform.up.y <= 0)
                {
                    // adjust blinkrange according to size of player collider
                    // times 10 because player is scaled by 10
                    // devided by 2 because origin of collider is in the middle of the collider
                    adjustedBlinkRange = hit.distance - (playerCollider.size.y * 10) / 2;

                    playerTransform.position += launcher.spawnPoint.transform.up.normalized * adjustedBlinkRange;
                }
                else
                {
                    adjustedBlinkRange = hit.distance - (playerCollider.size.x * 10) / 2;
                    playerTransform.position += launcher.spawnPoint.transform.up.normalized * adjustedBlinkRange;
                }
                triggered = true;
                break;
            }
        }
        // no border in blink direction
        if (!triggered)
        {
            playerTransform.position += launcher.spawnPoint.transform.up.normalized * aRange;
        }
    }

    void PlayAnimation ()
    {
        // trigger blink animation and wait for it to finish
        animator.SetTrigger("isBlinking");
        audioSource.PlayOneShot(abilitySound, 0.05f);
        PlayerAbilities.instance.StartCoroutine(WaitingTime(castTime));
    }

    IEnumerator WaitingTime(float Float)
    {
        yield return new WaitForSeconds(Float);
        launcher.player.GetComponent<PlayerMovement>().enabled = true;
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
