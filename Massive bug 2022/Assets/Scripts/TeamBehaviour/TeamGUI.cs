using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MyDict;

public class TeamGUI : MonoBehaviour
{
    [System.Serializable]
    private class ModifierButton
    {
        public Button btn;
        public Sprite enabledSprite;
        public Sprite unabledSprite;
        public void EnableButton()
        {
            btn.enabled = true;
            btn.GetComponent<Image>().sprite = enabledSprite;
        }
        public void DisableButton()
        {
            btn.enabled = false;
            btn.GetComponent<Image>().sprite = unabledSprite;
        }
    }

    [SerializeField] WorkingTeam workingTeam;

    [Header("Attribute GUI")]
    [SerializeField] private Text workHourPerDayText;
    [SerializeField] private Text performanceText;
    [SerializeField] private Text stressPerDayText;
    [SerializeField] private Text stressPointText;
    [SerializeField] private Text expText;
    [SerializeField] private Text salaryPerDayText;
    [SerializeField] private Text levelText;
    [SerializeField] private ModifierButton workMoreButton;         //press -> +1 work hour per day
    [SerializeField] private ModifierButton workLessButton;         //press -> -1 work hour per day
    [SerializeField] private Sprite activePanelSprite;
    [SerializeField] private Sprite inactivePanelSprite;


    private void OnEnable()
    {
        MyEventSystem.instance.DayPassedLateEvent.OnEventOccur += OnDayPassedLate;
    }
    private void OnDisable()
    {
        MyEventSystem.instance.DayPassedLateEvent.OnEventOccur -= OnDayPassedLate;
    }

    private void Start()
    {
        UpdateAttributeDisplay();
        WorkButtonUpdate();
    }

    public void IncreaseWorkHour()
    {
        workingTeam.IncreaseWorkHour();
        WorkButtonUpdate();
        UpdateAttributeDisplay();
    }
    public void DecreaseWorkHour()
    {
        workingTeam.DecreaseWorkHour();
        WorkButtonUpdate();
        UpdateAttributeDisplay();
    }
    private void WorkButtonUpdate()
    {
        if (workingTeam.workHourPerDay == WorkingTeam.idealWorkHourPerDay || !workingTeam.isAvailable)
            workLessButton.DisableButton();
        else
            workLessButton.EnableButton();

        if (workingTeam.workHourPerDay == WorkingTeam.maxWorkHourPerDay || !workingTeam.isAvailable)
            workMoreButton.DisableButton();
        else
            workMoreButton.EnableButton();
    }
    private void TeamPanelUpdate()
    {

    }

    private void OnDayPassedLate()
    {
        UpdateAttributeDisplay();
        WorkButtonUpdate();
    }
    private void UpdateAttributeDisplay()
    {
        //Update attribute GUI
        workHourPerDayText.text = "Work hour : " + workingTeam.workHourPerDay.ToString();
        performanceText.text = "Performance : " + ((int)(workingTeam.performance.value * 100)).ToString() + "%";
        stressPerDayText.text = "Stress : " + workingTeam.stressPerDay.value.ToString() +"/day";
        stressPointText.text = "Stress : " + workingTeam.stressPoint.ToString();
        expText.text = "Exp : " + workingTeam.teamExp.ToString();
        salaryPerDayText.text = "Salary : " + workingTeam.salaryPerDay.ToString() + "/day";
    }
}
