using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    GameObject damageableObj;

    public float damageMultiplier = 1;

    [HideInInspector] public Vector3 spawnPos;

    public void Attack(Transform target)
    {
        Debug.Log("attack");
        GameObject obj = 
            Instantiate(damageableObj, spawnPos, 
                        Quaternion.Euler(0, 0, Caculator.Vector2ToAngleInDegree(target.position - spawnPos)));

        ITargetLocker objLocker = (ITargetLocker)obj.GetComponent(typeof(ITargetLocker));
        objLocker?.LockTarget(target);

        obj.GetComponent<DamageRelated>()?.SetDamageMultiplier(damageMultiplier);
    }
    public void SetDamageableObject(GameObject obj)
    {
        damageableObj = obj;
    }
}
