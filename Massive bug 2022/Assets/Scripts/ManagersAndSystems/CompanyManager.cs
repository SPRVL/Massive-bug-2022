using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyDict;
using MyDict.Stat;

public class CompanyManager : MonoBehaviour
{
    public static CompanyManager instance { get; private set; }

    [SerializeField]
    private int startBudget;
    public int currentBudget { get; private set; }

    public Stat moneyCostPerDay;                                    //summarize money cost per day

    [SerializeField]
    private int totalBug;

    public int bugFound { get; private set; }   //don't use bug found per day because only 1 team found bug
  
    public int bugFixed { get; private set; }  //don't use bug fixed per day, same reason as bug found per day

    private void OnEnable()
    {
        MyEventSystem.instance.DayPassedEvent.OnEventOccur += OnDayPassed;
    }
    private void OnDisable()
    {
        MyEventSystem.instance.DayPassedEvent.OnEventOccur -= OnDayPassed;
    }

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        currentBudget = startBudget;
    }

    private void OnDayPassed()
    {
        currentBudget -= (int)moneyCostPerDay.value;

    }

    public void FoundBug(int bugCount)
    {
        bugFound += bugCount;
        bugFound = Mathf.Clamp(bugFound, 0, totalBug);
    }
    public void FixedBug(int bugCount)
    {
        bugFixed += bugCount;
        bugFixed = Mathf.Clamp(bugFixed, 0, totalBug);
    }
}
