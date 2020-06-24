using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Abilities/Blink")]
public class Blink : Ability
{
    public float aRange = 0f;
    public LayerMask mylayerMask;
    private SelfCastTriggerable launcher;

    public override void Initialize(GameObject obj)
    {
        launcher = obj.GetComponent<SelfCastTriggerable>();
    }

    public override void TriggerAbility()
    {
        Transform playerTransform = launcher.player.GetComponent<Transform>();

        ContactFilter2D filter = new ContactFilter2D();
        List<RaycastHit2D> res = new List<RaycastHit2D>();
        int hitint = Physics2D.Raycast(playerTransform.position, launcher.spawnPoint.transform.up, filter.NoFilter(), res, aRange);
        foreach(RaycastHit2D hit in res)
        {
            if(hit.collider.name == "Tilemap")
            {
                Debug.Log(hit.collider.name);
                playerTransform.position += launcher.spawnPoint.transform.up * hit.collider.Distance(launcher.player.GetComponent<CapsuleCollider2D>()).distance;
            }
        }

        //playerTransform.position += launcher.spawnPoint.transform.up.normalized * aRange;
    }
}
