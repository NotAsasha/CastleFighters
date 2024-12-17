using Assets.Build.Castle;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Assets.Build.Scripts
{
    public class Bar : MonoBehaviour
    {
        public string _barType;
        public int ID = 0;
        public float MaxLength = 3f;
        public Vector2 StartPosition;
        public HingeJoint2D StartJoint;
        public HingeJoint2D EndJoint;
        public Vector2 Start;
        public Vector2 End;
        public void UpdateCreatingBar(Vector2 ToPosition)
        {
            transform.position = (ToPosition + StartPosition) / 2;

            Vector2 dir = ToPosition - StartPosition;
            float angle = Vector2.SignedAngle(Vector2.right, dir);
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            /*Do not touch if it works!*/
            float Length = dir.magnitude / (2 + dir.magnitude / 3);
            transform.localScale = new Vector3(Length, transform.localScale.y, 1.4f + ID * 0.001f);
            /*    If block has scalable texture    rend.material.mainTextureScale = new Vector2(dir.magnitude, 1f);*/

        }


        public BarStorage SaveBar()
        {
            Start = StartJoint.connectedBody.GetComponent<Point>().PointID;
            End = EndJoint.connectedBody.GetComponent<Point>().PointID;
            BarStorage barInfo = new(ID, _barType, transform.localPosition, transform.rotation, transform.localScale, Start, End);
            return barInfo;
        }
    }
}
