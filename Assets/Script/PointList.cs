using Assets.Build.Scripts;
using UnityEngine;

namespace Assets.Script
{
    public class PointList
    {
        private Vector2 _pointsVector;
        private Point _point;

        public PointList(Vector2 newVector, Point newPoint)
        {
            _pointsVector = newVector;
            _point = newPoint;
        }

    }
}
