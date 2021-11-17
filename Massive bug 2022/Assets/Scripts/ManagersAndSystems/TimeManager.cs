using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MyDict;

public class TimeManager : MonoBehaviour
{
    [SerializeField] Text dateTimeText;
    [SerializeField] float baseTimeSpeed;

    private DateTime timer;
    private DateTime lastTime;
    private void Awake()
    {
        timer = new DateTime(2021, 12, 30, 0,0,0);
    }

    private void Update()
    {
        UpdateTimer();
        ShowDayTime();
    }
    private void UpdateTimer()
    {
        lastTime = timer;
        timer = timer.AddHours(baseTimeSpeed * Time.deltaTime);
        int timeSpan = timer.DayOfYear - lastTime.DayOfYear;

        //if day passed
        if (timeSpan > 0 || (timeSpan < 0 && timer.Year > lastTime.Year))
        {
            MyEventSystem.instance.DayPassedEvent.InvokeEvent();
            MyEventSystem.instance.DayPassedLateEvent.InvokeEvent();
        }
        

    }
    private bool IsSameDay(DateTime time1, DateTime time2)
    {
        if (time1.Year == time2.Year && time1.DayOfYear == time2.DayOfYear) return true;
        else return false;
    }
    private void ShowDayTime()
    {
        dateTimeText.text = timer.ToString("dd/MM/yyyy\n HH:00");
       // dateTimeText.text = dateTime.ToString();
    }

}
