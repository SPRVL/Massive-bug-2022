using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName = "Team Data")]
public class TeamData : ScriptableObject
{
    [HideInInspector] 
    public AnimationCurve workStressCurve;
    [HideInInspector]
    public AnimationCurve workPerformanceCurve;

    public float maxPerformanceMod;
    public float maxWorkStressMod;
    public int stressPerDayOff;

    [System.Serializable]
    public struct TeamLevelData
    {
        public int expToReachLevel;
        public float performanceModValue;
    }

    public TeamLevelData[] teamLevelDatas = new TeamLevelData[WorkingTeam.maxLevel + 1];

    public int expPerWorkHour;
    public int salaryPerWorkHour;
}
#if UNITY_EDITOR
[CustomEditor(typeof(TeamData), true, isFallback = false)]
public class WorkingTeamEdior : Editor
{
    bool curveFoldOut = false;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        TeamData script = (TeamData)target;
        curveFoldOut = EditorGUILayout.Foldout(curveFoldOut, "Work Curves");
        if (curveFoldOut)
        {
            SerializeWorkCurve("Work Time Stress", ref script.workStressCurve, Color.green);
            //SerializeWorkCurve("Under Work Stress",ref script.uWorkStressCurve, Color.green);
            SerializeWorkCurve("Work Time Performance", ref script.workPerformanceCurve, Color.red);
            //SerializeWorkCurve("Under Work Performance",ref script.uWorkPerformanceCurve, Color.red);
        }
    }
    private void SerializeWorkCurve(string curveName, ref AnimationCurve curve, Color curveColor)
    {
        curve = EditorGUILayout.CurveField(curveName, curve, curveColor, new Rect(0, 0, 1, 1));
    }
}
#endif