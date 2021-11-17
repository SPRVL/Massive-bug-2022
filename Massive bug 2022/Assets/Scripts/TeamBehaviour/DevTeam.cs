using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevTeam : WorkingTeam
{
    [SerializeField] int baseBugFixPerDay;

    protected override void OnDayPassed()
    {
        base.OnDayPassed();

        CompanyManager.instance.FixedBug((int)(baseBugFixPerDay * performance.value));
    }

}
