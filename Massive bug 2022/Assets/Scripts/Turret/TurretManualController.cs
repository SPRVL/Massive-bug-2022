using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretManualController : TurretController
{
    [SerializeField] Turret turret;
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if(hit)
            {
                turret.Attack(hit.transform);
            }
        }
    }
}
