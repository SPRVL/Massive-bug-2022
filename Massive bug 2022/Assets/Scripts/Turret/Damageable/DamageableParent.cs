using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableParent : DamageRelated
{
    
    public override void SetDamageMultiplier(float multiplier)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<Damageable>()?.SetDamageMultiplier(multiplier);
        }
    }
    private void OnTransformChildrenChanged()
    {
        if (transform.childCount == 0) Destroy(gameObject);
    }
}
