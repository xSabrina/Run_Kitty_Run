﻿using UnityEngine;
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
        Rigidbody2D rbPlayer = launcher.player.GetComponent<Rigidbody2D>();
        rbPlayer.position += (Vector2)launcher.spawnPoint.transform.up.normalized * aRange;
        animator.SetTrigger("isBlinking");
    }

}