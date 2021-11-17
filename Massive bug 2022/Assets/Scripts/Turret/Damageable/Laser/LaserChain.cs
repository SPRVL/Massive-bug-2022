using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserChain : Laser ,ITargetLocker
{
    [SerializeField] protected LayerMask targetLayerMask;
    [SerializeField] float jointAngle;
    [SerializeField] float maxChainLength;
    [SerializeField] int maxChainCount;

    [SerializeField]private List<Vector3> laserPos = new List<Vector3>();
    private Transform firstTarget;

    public void LockTarget(Transform _target)
    {
        firstTarget = _target;
    }
    private void Start()
    {
        laserPos.Add(transform.position);
        FindAndAttackTarget();
    }

    private void FindAndAttackTarget()
    { 
        Vector2 start = transform.position;
        Vector2 end = firstTarget.position;

        Vector2 currentDir = (end - start).normalized;

        Transform currentTarget = firstTarget;

        AttackTarget(currentDir, currentTarget);

        while (laserPos.Count <= maxChainCount && (currentTarget = NextTarget(currentTarget,currentDir)) != null)
        {
            Vector2 temp = end;
            end = currentTarget.position;
            start = temp;

            currentDir = end - start;

            AttackTarget(currentDir, currentTarget);
        }
        DrawLaser();
    }

    private Transform NextTarget(Transform currentTarget, Vector2 currentNormalizedDir)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(currentTarget.position, maxChainLength, targetLayerMask);

        for (int i = 0; i < colliders.Length; i++)
        {
            Collider2D collider = colliders[i];
            if (collider.CompareTag(targetTag) && jointAngle >
                Caculator.AngleOf2VectorInDegree(currentNormalizedDir, collider.transform.position - currentTarget.position) &&
                colliders[i].transform != currentTarget) 
            {
                return collider.transform;
            }
        }
        return null;
    }

    private void AttackTarget(Vector2 nomalizedDir, Transform targetToAttack)
    {
        nomalizedDir = nomalizedDir.normalized;

        targetToAttack.GetComponent<BugController>()?.GetHit(damage, knockbackTime, nomalizedDir * knockbackForce);

        laserPos.Add(targetToAttack.position);
    }
    private void DrawLaser()
    {
        lineRenderer.positionCount = laserPos.Count;
        lineRenderer.SetPositions(laserPos.ToArray());

        StartCoroutine(Decay());
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, maxChainLength);

        Gizmos.DrawLine(transform.position, (Vector2)transform.position + Caculator.NormalizedDirection2D(-jointAngle / 2) * maxChainLength);
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + Caculator.NormalizedDirection2D(jointAngle / 2) * maxChainLength);
    }
}
