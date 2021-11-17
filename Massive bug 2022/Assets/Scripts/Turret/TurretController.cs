using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyDict;

[RequireComponent(typeof(TurretInspector))]
[RequireComponent(typeof(Turret))]
public class TurretController : MonoBehaviour
{
    private const int maxStress = 100;

    [SerializeField] TurretData turretData;
    [SerializeField] float stressCoolSpeed;
    [SerializeField] int stressPointPerShoot;

    [Range(0, maxStress)] protected float _stressPoint;
                    public float stressPoint { get { return _stressPoint; } }

    private bool isAvailable;

    private TurretInspector turretInspector;
    private Turret turret;
    private Transform currentTarget;
    private RangedFloat cdSpeedMultiplier;
    private float cooldownCounter = 0;
    

    private int currentExp;

    private void OnEnable()
    {
        MyEventSystem.instance.IncreaseExp.OnEventOccur += OnIncreaseExp;
    }
    private void OnDisable()
    {
        MyEventSystem.instance.IncreaseExp.OnEventOccur -= OnIncreaseExp;
    }
    private void Awake()
    {
        turretInspector = gameObject.GetComponent<TurretInspector>();
        turret = gameObject.GetComponent<Turret>();
        turret.spawnPos = IngameManager.instance.playerTrans.position;
    }
    private void Update()
    {
        if(isAvailable) DoCooldown();
        CoolTurret();
        CheckStress();
    }

    private void DoCooldown()
    {
        if (cooldownCounter > 0) cooldownCounter -= cdSpeedMultiplier.val * Time.deltaTime;
        else InvokeAttack();
    }
    private void InvokeAttack()
    {
        if (currentTarget == null) currentTarget = GetTarget();
        if (isAvailable && currentTarget != null)
        {
            turret.Attack(currentTarget);
            _stressPoint += stressPointPerShoot;
            cooldownCounter = turretData.cooldown;
        }
    }

    private void CheckStress()
    {
        if (!isAvailable)
        {
            if (_stressPoint == 0) OnCooled();
        }
        else if (_stressPoint == maxStress) OnOverHeated();
    }
    private void OnIncreaseExp(int exp)
    {
        currentExp += exp;
    }
    private void CheckCurrentExp()
    {

    }
    private void CoolTurret()
    {
        _stressPoint -= stressCoolSpeed;
    }
    private void OnOverHeated()
    {
        isAvailable = false;
    }
    private void OnCooled()
    {
        isAvailable = true;
    }

    private Transform GetTarget()
    {
        if (BugSpawner.instance.bugTrans.Count == 0) return null;
        else return BugSpawner.instance.bugTrans[0];

    }
}
