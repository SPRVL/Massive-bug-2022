using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Turret Data")]
public class TurretData : ScriptableObject
{
    [SerializeField] 
    private GameObject _objToSpawn;
    public GameObject objToSpawn { get { return _objToSpawn; } }

    [SerializeField]
    private float _cooldown;
    public float cooldown { get { return _cooldown; } }

    [SerializeField]
    private TurretData[] _nextTurretForms;
    public TurretData[] nextTurretForms { get { return _nextTurretForms; } }

    [SerializeField]
    private int _expToNextForm;
    public int expToNextForm { get { return _expToNextForm; } }

    [SerializeField]
    private Sprite _turretIcon;
    public Sprite turretIcon { get { return _turretIcon; } }
}
