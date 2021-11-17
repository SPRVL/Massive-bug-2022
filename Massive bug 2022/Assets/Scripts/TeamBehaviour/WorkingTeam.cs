using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyDict;
using MyDict.Stat;

public abstract class WorkingTeam : MonoBehaviour
{
    public const int idealWorkHourPerDay = 8;
    public const int maxWorkHourPerDay = 14;
    public const int maxStressPoint = 100;
    public const int maxLevel = 10;

    [SerializeField] TeamData teamData;

    [Range(0,maxWorkHourPerDay)] protected int _workHourPerDay = idealWorkHourPerDay; //test, remove serialize field when done
                                    public int workHourPerDay { get { return _workHourPerDay; } }
   
    //[HideInInspector] public AnimationCurve uWorkStressCurve;         //decrease stress when work less
   
                                                                     //[HideInInspector] public AnimationCurve uWorkPerformanceCurve;    //decrease performance when work less

    //-----------------------------------------------STRESS--------------------------------------
    [Range(0, 100)] protected int _stressPoint;
                    public int stressPoint { get { return _stressPoint; } }

                     protected AnimationCurve workStressCurve;         //increase stress when work more
                     public Stat stressPerDay;
                     protected StatMod stressPerDayMod;
                     protected float maxWorkStressMod;              // is positive (when work more, stress increase)
                     protected int stressPerDayOff;

    //protected int _stressPerDay;                                 // speed stress change per day
    //public int stressPerDay { get { return _stressPerDay; } }
    //[SerializeField] protected int uWorkStressMod;              // is negative (when work less, stress decrease)
    //------------------------------------------------------------------------------------------

    //---------------------------------------------PERFORMANCE----------------------------------
                      protected AnimationCurve workPerformanceCurve;    //increase performance when work more

                      public Stat performance;
                      protected StatMod performanceMod;

                      protected float maxPerformanceMod;

    
    //[SerializeField] protected float uWorkPerformanceMod;
    //----------------------------------------------------------------------------------------------



    //-----------------------------EXPERIENCE AND LEVEL------------------------------------

                     protected int expPerWorkHour;
                     protected int currentLevel = -1;
                     protected int _teamExp;
                     public int teamExp { get { return _teamExp; } }
    //--------------------------------------------------------------------------------


                     int salaryPerWorkHour;
    public int salaryPerDay { get { return salaryPerWorkHour * workHourPerDay; } }

    protected bool _isAvailable = true;
    public bool isAvailable { get { return _isAvailable; } }


    protected void OnEnable()
    {
        MyEventSystem.instance.DayPassedEvent.OnEventOccur += OnDayPassed;   
    }
    protected void OnDisable()
    {
        MyEventSystem.instance.DayPassedEvent.OnEventOccur -= OnDayPassed;
    }

    protected virtual void OnDayPassed()
    {
        _stressPoint += (int)stressPerDay.value;
        _stressPoint = Mathf.Clamp(_stressPoint, 0, maxStressPoint);
        _teamExp += expPerWorkHour * workHourPerDay;

        int thisDayLevel = CaculateLevel();
        if(currentLevel != thisDayLevel)
        {
            currentLevel = thisDayLevel;
            performance.ChangeModifierOfSource(this, teamData.teamLevelDatas[thisDayLevel].performanceModValue);
        }
        
        if (_stressPoint == 100) GoOnStrike();
        else if(!isAvailable && stressPoint == 0) ReturnToWork();
    }
    private void Awake()
    {
        SetupProperties();
        OnWorkHourChange();       
    }
    private void SetupProperties()
    {
        workStressCurve = teamData.workStressCurve;
        workPerformanceCurve = teamData.workPerformanceCurve;
        maxWorkStressMod = teamData.maxWorkStressMod;
        maxPerformanceMod = teamData.maxPerformanceMod;
        stressPerDayOff = teamData.stressPerDayOff;
        expPerWorkHour = teamData.expPerWorkHour;
        salaryPerWorkHour = teamData.salaryPerWorkHour;
        
    }
    public void IncreaseWorkHour()
    {
        if(isAvailable) _workHourPerDay += 1;
        OnWorkHourChange();
    }
    public void DecreaseWorkHour ()
    {
        if(isAvailable) _workHourPerDay -= 1;
        OnWorkHourChange();
    }
    protected void OnWorkHourChange()
    {
        // xPoint on curve
        float xPoint;
                
        //use over work curve
        //make sure curve's x = (0,1) and curve's y = (0,1)
               
        if(workHourPerDay == 0)
        {
            performance.ForceValue(0); //no work, nothing happens
            stressPerDay.ForceValue(stressPerDayOff);
        }
        else
        {
            xPoint = Mathf.InverseLerp(idealWorkHourPerDay, maxWorkHourPerDay, workHourPerDay);

            WorkingTeamCaculator.UpdateStatMod(xPoint, maxWorkStressMod,true, workStressCurve, ref stressPerDay, ref stressPerDayMod);


            WorkingTeamCaculator.UpdateStatMod(xPoint, maxPerformanceMod,false, workPerformanceCurve, ref performance, ref performanceMod);
        }
        CompanyManager.instance.moneyCostPerDay.ChangeModifierOfSource(this, salaryPerDay);

    }

    protected void UpdateTeamStats()
    {
        //update money cost per day
    }
    protected void GoOnStrike()
    {
        _isAvailable = false;
        _workHourPerDay = 0;
        OnWorkHourChange();
    }
    protected void ReturnToWork()
    {
        _isAvailable = true;
        if (_workHourPerDay == 0)
        {
            _workHourPerDay = idealWorkHourPerDay;
            OnWorkHourChange();
        }
    }

    protected int CaculateLevel()
    {
        int level = teamData.teamLevelDatas.Length - 1;
        for (int i = 0; i < teamData.teamLevelDatas.Length - 1; i++)
        {
            if((_teamExp > teamData.teamLevelDatas[i].expToReachLevel) && (_teamExp <= teamData.teamLevelDatas[i + 1].expToReachLevel))
            {
                level = i;
                break;
            }
        }
        return level;
    }
   /* protected void ApplyLevel(int level)
    {
        performance.RemoveModifierOfSource(this);

        StatMod performanceMod = new StatMod(this,teamLevelDatas[level].performanceModValue);
        performance.AddModifier(performanceMod);
    }*/
}

public static class WorkingTeamCaculator
{
    public static void UpdateStatMod(float xPoint, float maxMod, bool isInteger, AnimationCurve curve, ref Stat stat, ref StatMod mod)
    {
        stat.UnforceValue();

        float modVal = curve.Evaluate(xPoint) * maxMod;
        if (isInteger) modVal = Mathf.Round(modVal);

        StatMod newStressMod = new StatMod(modVal);

        if (stat == null) stat.AddModifier(newStressMod);
        else if (!stat.ChangeModifier(mod, newStressMod)) stat.AddModifier(newStressMod);

        mod = newStressMod;
    }
}

