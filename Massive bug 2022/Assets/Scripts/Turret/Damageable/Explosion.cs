using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : Damageable
{
    [SerializeField] protected LayerMask targetLayerMask;
    [SerializeField] float knockbackForce;
    [SerializeField] float knockbackTime;
    [SerializeField] float explosionRadius;

    public void DoExplosion()
    {
        Collider2D[] bugColliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius,targetLayerMask);
        for (int i = 0; i < bugColliders.Length; i++)
        {
            if (bugColliders[i].CompareTag(targetTag))
            {
                BugController bugScript = bugColliders[i].GetComponent<BugController>();
                if (bugScript != null) bugScript.GetHit(damage, knockbackTime, (bugColliders[i].transform.position - transform.position).normalized * knockbackForce);
            }
        }
    }

    public void Destruct()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
