using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyDict.Stat
{
    [System.Serializable]
    public class Stat 
    {
        [SerializeField] private float baseValue;
        [SerializeField] private float minValue;

        private float _value;
        public float value 
        { 
            get 
            {
                if (isForced) return forcedValue;
                else
                {
                    if (isDirty) _value = CaculateStatValue();
                    return _value;
                }
            } 
        }

        private bool isForced = false;      //value return forced value if true
        private float forcedValue = 0;

        private bool isDirty = true;

        private List<StatMod> statMods = new List<StatMod>();

        public void AddModifier(StatMod mod)
        {
            statMods.Add(mod);
            isDirty = true;
            _value = CaculateStatValue();
        }
        public void RemoveModifierOfSource(object source)
        {
            for (int i = statMods.Count - 1; i >= 0; i--)
            {
                if (statMods[i].source == source)
                {
                    statMods.RemoveAt(i);
                    isDirty = true;
                    _value = CaculateStatValue();
                }
            }
        }
        public bool ChangeModifier(StatMod oldMod, StatMod newMod)
        {
            for (int i = 0; i < statMods.Count; i++)
            {
                if(statMods[i] == oldMod)
                {
                    statMods[i] = newMod;
                    isDirty = true;
                    _value = CaculateStatValue();
                    return true;

                }
            }
            return false;
        }
        public void ChangeModifierOfSource(object source, float modValue)
        {
            RemoveModifierOfSource(source);
            StatMod newMod = new StatMod(source, modValue);
            AddModifier(newMod);
        }
        private float CaculateStatValue()
        {
            isDirty = false;

            float flatMod = 0;
            for (int i = 0; i < statMods.Count; i++)
            {
                flatMod += statMods[i].value;
            }

            return Mathf.Clamp(baseValue + flatMod,minValue,float.MaxValue);
        }

        public void ForceValue(float value)
        {
            isForced = true;
            forcedValue = value;
        }
        public void UnforceValue()
        {
            isForced = false;
        }

    }
}
