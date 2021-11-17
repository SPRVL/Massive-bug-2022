using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : Laser
{
    [SerializeField] protected LayerMask targetLayerMask;
    [SerializeField] float laserDistance;
    [SerializeField] float laserDamageInterval;
    [SerializeField] int attackCount;                //how many time of triggering damage to target

    private int attackNum = 0;

    private Vector2 laserStart;
    private Vector2 laserEnd;

    private Vector2 currentDirection;

    private void Start()
    {
        UpdateLaserPositions();
        DrawLaser();
        StartCoroutine(AttackTarget());
    }

    private void Update()
    {
        UpdateLaserPositions();
        DrawLaser();
    }
    private IEnumerator AttackTarget()
    {
        RaycastHit2D[] hits  = Physics2D.LinecastAll(laserStart, laserEnd,targetLayerMask);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].transform.CompareTag(targetTag))
                hits[i].transform.GetComponent<BugController>()?.GetHit(damage, knockbackTime, currentDirection * knockbackForce);
        }
        attackNum++;

        if (attackNum < attackCount)
        {
            yield return new WaitForSeconds(laserDamageInterval);
            StartCoroutine(AttackTarget());
        }
        else
        {
            StartCoroutine(Decay());
            yield return null;
        }


    }
    

    private void UpdateLaserPositions()
    {
        currentDirection = Caculator.NormalizedDirection2D(transform.rotation.eulerAngles.z);
        laserStart = transform.position;
        laserEnd = currentDirection * laserDistance;
    }
    private void DrawLaser()
    {
        lineRenderer.SetPositions(new Vector3[] { laserStart, laserEnd });
    }
}
