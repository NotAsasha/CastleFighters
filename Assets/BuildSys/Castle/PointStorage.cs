using Assets.Build.Scripts;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Build.Castle
{
    [System.Serializable]
    public class PointStorage
    {
        public int _pointId;
        public Vector2 PointId;
        public List<Bar> ConnectedBars;
        public PointStorage(List<Bar> connectedBars, Vector2 pointId)
        {

            ConnectedBars = connectedBars;
            PointId = pointId;
        }
    }
}
