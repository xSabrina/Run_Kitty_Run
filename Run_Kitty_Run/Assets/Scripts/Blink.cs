using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Abilities/Blink")]
public class Blink : Ability
{
    public float aRange = 0f;

    private SelfCastTriggerable launcher;

    public override void Initialize(GameObject obj)
    {
        launcher = obj.GetComponent<SelfCastTriggerable>();
    }

    public override void TriggerAbility()
    {
        Transform playerTransform = launcher.player.GetComponent<Transform>();
        playerTransform.position += launcher.spawnPoint.transform.up.normalized * aRange; 
    }
}