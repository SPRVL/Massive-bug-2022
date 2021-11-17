using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesterTeam : WorkingTeam
{
    [SerializeField] int baseBugFoundPerDay;
    protected override void OnDayPassed()
    {
        base.OnDayPassed();

        CompanyManager.instance.FoundBug((int)(baseBugFoundPerDay * performance.value));
    }
}
