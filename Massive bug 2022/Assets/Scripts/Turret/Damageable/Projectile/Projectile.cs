using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : Damageable
{
    [SerializeField] private float baseSpeed = 1;
    private float speedMultiplier = 1;
    protected float speed { get { return baseSpeed * speedMultiplier; } }

    [SerializeField] private float knockbackForce;
    [SerializeField] private float knockbackTime;

    [Range(1,1000)]
    [SerializeField] private int health;        //How many hit before destroy this game object
    private int hit = 0;

    [SerializeField] GameObject damageableObjSpawnOnHit;

    public virtual void SetupProjectile(float _speedMultiplier)
    {
        speedMultiplier = _speedMultiplier;
    }

    
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        //damage bug and knockback it
        //spawn damageable object if exists
        //if hit > health => destroy

        if (collision.CompareTag(targetTag))
        {
            BugController bugScript = collision.GetComponent<BugController>();
            if(bugScript != null)
            {
                if (knockbackForce != 0)
                    bugScript.GetHit(damage, knockbackTime, KnockbackDirection() * knockbackForce);
                else if (damage != 0) bugScript.GetHit(damage);
            }

            if(damageableObjSpawnOnHit != null)
            {
                DamageRelated damageable = 
                    Instantiate(damageableObjSpawnOnHit, transform.position, transform.rotation).GetComponent<DamageRelated>();

                damageable.SetDamageMultiplier(damageMultiplier);
            }

            hit++;
            if (hit >= health) Destroy(gameObject);
        }
    }

    protected abstract Vector2 KnockbackDirection();
}
