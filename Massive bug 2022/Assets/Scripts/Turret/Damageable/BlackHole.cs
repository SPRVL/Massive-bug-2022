using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : Damageable
{
    [SerializeField] protected LayerMask targetLayerMask;
    [SerializeField] float gravity;
    [SerializeField] float pullTime;
    [SerializeField] float pullRadius;
    [SerializeField] int pullCount;

    int numOfPull = 0;

    //triggered by anim
    public void PullTarget()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, pullRadius,targetLayerMask);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag(targetTag)) colliders[i].GetComponent<BugController>()?.
                    GetHit(damage, pullTime, (transform.position - colliders[i].transform.position).normalized * gravity);
        }
        numOfPull++;
    }

    //triggered by the end of anim
    public void CheckIfDestroy()
    {
        if (numOfPull >= pullCount) Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, pullRadius);
    }
}
