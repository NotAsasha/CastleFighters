using System;
using UnityEngine;

namespace Assets.Build.Castle
{
    [System.Serializable]
    public class BarStorage
    {
        public int _barId;
        public string _barType;
        public Vector3 _barPosition;
        public Quaternion _barRotation;
        public Vector3 _barScale;
        public Vector2 _startPosition;
        public Vector2 _connectedPoint1;
        public Vector2 _connectedPoint2;

        public BarStorage(int BarId, string barType, Vector3 barPosition, Quaternion barRotation, Vector3 barScale, Vector2 connectedPoint1, Vector2 connectedPoint2)
        {
            _barId = BarId;
            _barType = barType;
            _barPosition = barPosition;
            _barRotation = barRotation;
            _barScale = barScale;
            _connectedPoint1 = connectedPoint1;
            _connectedPoint2 = connectedPoint2;
        }

    }
}
