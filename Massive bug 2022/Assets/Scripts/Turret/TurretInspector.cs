using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TurretController))]
public class TurretInspector : MonoBehaviour
{
    [SerializeField] GameObject upgrdPathBtnPrefab;
    [SerializeField] Image turretIcon;
    [SerializeField] Slider heatSlider;

    private TurretController turretController;

    private void Awake()
    {
        turretController = GetComponent<TurretController>();
    }


    public void IncreaseTurretSpeed()
    {

    }
}
