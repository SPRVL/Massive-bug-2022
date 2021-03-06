using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyDict;

public class IngameManager : MonoBehaviour
{
    private static IngameManager _instance;
    public static IngameManager instance 
    { 
        get 
        { 
            if (_instance == null) Debug.LogError("Not found IngameManager"); 
            return _instance; 
        } 
    }

    public Transform playerTrans { get; private set; }

    public int currentMoney { get; private set; }
    public int bugKilled { get; private set; }

    [SerializeField] int moneyPerBug;
    [SerializeField] int expPerBug;

    private void OnEnable()
    {
        MyEventSystem.instance.BugDie.OnEventOccur += OnBugDie;
    }
    private void OnDisable()
    {
        MyEventSystem.instance.BugDie.OnEventOccur -= OnBugDie;
    }

    private void Awake()
    {
        bugKilled = 0;
        currentMoney = 0;

        playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
        if (playerTrans == null) Debug.LogError("Player not found");

        if (_instance == null) _instance = this;
        else Destroy(gameObject);
    }

    private void OnBugDie()
    {
        MyEventSystem.instance.IncreaseExp.InvokeEvent(expPerBug);
        currentMoney += moneyPerBug;
        bugKilled++;
    }
    public bool DecreaseMoney(int value)
    {
        if(value >= 0 && value <= currentMoney)
        {
            currentMoney -= value;
            return true;
        }
        return false;
    }
}
