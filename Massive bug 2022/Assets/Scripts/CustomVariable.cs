using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MyDict
{
    [System.Serializable]
    public struct RangedFloat
    {
        public float min;
        public float max;
        public float val { get { return val; } set {val = Mathf.Clamp(value,min,max);} }
        public bool isMin { get { return (val == min) ? true : false; } }
        public bool isMax { get { return (val == min) ? true : false; } }
    }
}
