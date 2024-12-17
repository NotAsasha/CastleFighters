using System.Collections.Generic;
using UnityEngine;

namespace Assets.Build.Castle
{
    [System.Serializable]
    public class CastleData
    {
        public string CastleName;
        public float CastleCost;
        public BarStorage[] BarArray;
        public PointStorage[] PointArray;
    }
}