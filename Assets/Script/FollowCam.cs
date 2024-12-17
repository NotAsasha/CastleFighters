using UnityEngine;

namespace Assets.Script
{
    public class FollowCam : MonoBehaviour
    {
        static public Transform POI;


        [Header("Set Dynamically")]
        public float easing = 0.05f;
        public Transform _yourSlingShot;
        public Transform _enemySlingShot;
        public Transform _showBothPosition;
        public Vector2 minXY = Vector2.zero;
        public static CameraState _cameraState;

        public enum CameraState
        {
            ShowBoth = 0,
            YourSlingshot = 1,
            EnemySlingshot = 2,
            Projectile = 3
        }
        public void ShowBoth()
        {
            _cameraState = CameraState.ShowBoth;
            POI = null;
        }
        public void ShowYourSlingshot()
        {
            _cameraState = CameraState.YourSlingshot;
            POI = null;
        }
        public void ShowEnemySlingshot()
        {
            _cameraState = CameraState.EnemySlingshot;
            POI = null;
        }
    private void FixedUpdate()
        {
            switch (_cameraState)
            {
                case CameraState.YourSlingshot:
                    MoveCamera(new Vector3(_yourSlingShot.position.x, _yourSlingShot.position.y + 10f, _yourSlingShot.position.z - 30f));
                    break;
                case CameraState.EnemySlingshot:
                    MoveCamera(new Vector3(_enemySlingShot.position.x, _enemySlingShot.position.y + 10f, _enemySlingShot.position.z - 30f));
                    break;
                case CameraState.ShowBoth:
                    MoveCamera(_showBothPosition.position);
                    break;
                case CameraState.Projectile:
                    if (!POI || POI.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude == 0)
                    {
                        _cameraState = CameraState.ShowBoth;
                        POI = null ;
                        break;
                    }
                    MoveCamera(new Vector3(POI.position.x, POI.position.y, POI.position.z - 20f));
                    break;
            }

        }
        private void MoveCamera(Vector3 movePosition)
        {
            Vector3 destination;
            if (movePosition == null)
            {
                destination = new Vector3(_showBothPosition.position.x, _showBothPosition.position.y + 10f, _showBothPosition.position.z);
            }
            else
            {
                destination = movePosition;
            }
            destination.x = Mathf.Max(minXY.x, destination.x);
            destination.y = Mathf.Max(minXY.y, destination.y);
            destination.z = Mathf.Lerp(transform.position.z, destination.z, 5f);
            destination = Vector3.Lerp(transform.position, destination, easing);

            transform.position = destination;
        }
    }
}
