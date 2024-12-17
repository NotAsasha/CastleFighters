using Assets.Build.Castle;
using Assets.Plugins.TimeRecorder;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

namespace Assets.Build.Scripts
{
    [ExecuteInEditMode]
    public class Point : MonoBehaviour
    {
        public bool Runtime = true;
        public Rigidbody2D rbd;
        public Vector2 PointID;
        public List<Bar> ConnectedBars;

        private void Start()
        {
            if (Runtime != false) return;
            rbd.bodyType = RigidbodyType2D.Static;
            PointID = transform.localPosition;
            GameManager.AllPoints.TryAdd(PointID, this);
        }


        void Update()
        {
            PointID = transform.localPosition;
            if (Runtime != false) return;
            if (transform.hasChanged != true) return;
            transform.hasChanged = false;
            transform.position = Vector3Int.RoundToInt(transform.position);
        }
        public PointStorage SavePoint()
        {
            PointID = transform.localPosition;
            var pointInfo = new PointStorage(ConnectedBars, PointID);
            return pointInfo;
        }
    }
}
