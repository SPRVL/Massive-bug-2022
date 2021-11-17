using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] GameObject damageableObj;

    public float damageMultiplier = 1;

    public Vector3 spawnPos = Vector3.zero;

    public void Attack(Transform target)
    {
        GameObject obj = 
            Instantiate(damageableObj, spawnPos, 
                        Quaternion.Euler(0, 0, Caculator.Vector2ToAngleInDegree(target.position - spawnPos)));

        ITargetLocker objLocker = (ITargetLocker)obj.GetComponent(typeof(ITargetLocker));
        objLocker?.LockTarget(target);

        obj.GetComponent<DamageRelated>()?.SetDamageMultiplier(damageMultiplier);
    }
}
