using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : ScriptableObject
{
    public string aName = "New Ability";
    // public Sprite aSprite;
    public AudioClip abilitySound;
    public float aBaseCoolDown = 1f;
    public float castTime = 0f;
    public float castPoint = 0f;

    public abstract void Initialize(GameObject obj);
    public abstract void TriggerAbility();
}
