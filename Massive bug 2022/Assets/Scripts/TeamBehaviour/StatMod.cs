using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyDict.Stat
{
    public class StatMod
    {
        public readonly object source;
        public readonly float value;

        public StatMod(float _modValue)
        {
            value = _modValue;
        }
        public StatMod(object _source, float _modValue)
        {
            source = _source;
            value = _modValue;
        }

    }
}
