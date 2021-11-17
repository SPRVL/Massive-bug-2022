using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Damageable : DamageRelated
{
    [SerializeField] protected string targetTag;
    [SerializeField] private int baseDamage;

    protected float damageMultiplier { get; private set; } = 1;
    protected int damage { get { return (int)(baseDamage * damageMultiplier); } }

    public override void SetDamageMultiplier(float multiplier)
    {
        damageMultiplier = multiplier;
    }

}
