using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Abilities/AbilitySprint")]
public class AbilitySprint : Ability
{
    public float sprintSpeed = 0f;
    public float duration = 0f;
    public AudioClip abilitySound2;

    private float originalSpeed = 0f;

    private SelfCastTriggerable launcher;
    private PlayerMovement playerMovement;
    private AudioSource audioSource;

    public override void Initialize(GameObject obj)
    {
        launcher = obj.GetComponent<SelfCastTriggerable>();
        audioSource = launcher.player.GetComponent<AudioSource>();
        playerMovement = launcher.player.GetComponent<PlayerMovement>();
        originalSpeed = playerMovement.speed;
    }

    public override void TriggerAbility()
    {
        PlayerAbilities.instance.StartCoroutine(WaitingTime(duration));
        playerMovement.speed = sprintSpeed;
        PlayAnimation();
    }

    void PlayAnimation()
    {
        audioSource.PlayOneShot(abilitySound, 0.05f);
    }

    IEnumerator WaitingTime(float Float)
    {
        yield return new WaitForSeconds(Float);
        playerMovement.speed = originalSpeed;
        audioSource.PlayOneShot(abilitySound2, 0.05f);
    }
}
