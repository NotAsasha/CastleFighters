using Assets.Build.Castle;
using Assets.Script;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.SlingShot.Scripts
{
    public class Slingshot : MonoBehaviour
    {
        static private Slingshot S;

        [Header("Set in Inspector")]
        public GameObject NormalPrefabProjectile;
        public GameObject FirePrefabProjectile;
        public GameObject HeavyPrefabProjectile;
        public float velocityMult = 14f;
        public float maxRadius = 4f;
        public Animator _animGameObject;
        public bool _isEnemy;

        [Header("Set Dynamically")]
        public ProjecteType projType = ProjecteType.Normal;
        public GameObject launchPoint;
        public Vector3 launchPos;
        public GameObject projectile;
        public bool aimingMode;
        private Rigidbody2D projectileRigidbody;


        public enum ProjecteType
        {
            Normal = 0,
            Fire = 1,
            Heavy = 2
        }



        static public Vector3 LAUNCH_POS
        {
            get
            {
                if (S == null) return Vector3.zero;
                return S.launchPos;
            }
        }
        void Awake()
        {
            S = this;
            Application.targetFrameRate = 1000;
            Transform launchPointTrans = transform.Find("LaunchPoint");
            launchPoint = launchPointTrans.gameObject;
            launchPoint.SetActive(false);
            launchPos = launchPointTrans.position;
        }
        void OnMouseEnter()
        {
            if (FollowCam.POI != null) return;
            /*print("Slingshot:OnMouseEnter()");*/
            launchPoint.SetActive(true);
        }
        private void OnMouseExit()
        {
            if (aimingMode) return;
            launchPoint.SetActive(false);
            /*print("Slingshot:OnMouseExit()");*/
        }
        void OnMouseDown()
        {
            if (FollowCam.POI != null) return;
            aimingMode = true;
            projectile = (projType == ProjecteType.Normal) ? Instantiate(NormalPrefabProjectile) :
                         (projType == ProjecteType.Fire) ? Instantiate(FirePrefabProjectile) :
                         (projType == ProjecteType.Heavy) ? Instantiate(HeavyPrefabProjectile) :
                         Instantiate(NormalPrefabProjectile);
            if (!_isEnemy) FollowCam._cameraState = FollowCam.CameraState.YourSlingshot;
            if (_isEnemy) FollowCam._cameraState = FollowCam.CameraState.EnemySlingshot;
            projectile.transform.position = launchPos;
            projectile.GetComponent<Rigidbody2D>().isKinematic = true;
            projectileRigidbody = projectile.GetComponent<Rigidbody2D>();
            _animGameObject.SetTrigger("IsAiming");
            projectileRigidbody.isKinematic = true;
        }
        public void ChangeProjectile(int newProj)
        {
            projType = (ProjecteType)newProj;
        }
        private void Update()
        {

            if (!aimingMode || FollowCam.POI != null) return;
            if (!projectile)
            {
                Trajectory._trajectory.Hide();
                aimingMode = false;
                launchPoint.SetActive(false);
                _animGameObject.SetTrigger("IsShooting");
                FollowCam._cameraState = FollowCam.CameraState.ShowBoth;
                return;
            }
            Trajectory._trajectory.Show();
            Vector3 mousePos2D = Input.mousePosition;
            mousePos2D.z = -Camera.main.transform.position.z;
            Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);
            Vector3 mouseDelta = mousePos3D - launchPos;
            float maxMagnitude = maxRadius;
            if (mouseDelta.magnitude > maxMagnitude)
            {
                mouseDelta.Normalize();
                mouseDelta *= maxMagnitude;
            }
            Vector3 projPos = launchPos + mouseDelta;
            projectile.transform.position = projPos;
            Trajectory._trajectory.UpdateDots(projectile.transform.position, -mouseDelta * velocityMult);
            if (Input.GetMouseButtonUp(0))
            {
                Trajectory._trajectory.Hide();
                aimingMode = false;
                launchPoint.SetActive(false);
                _animGameObject.SetTrigger("IsShooting");
                projectileRigidbody.isKinematic = false;
                projectileRigidbody.velocity = -mouseDelta * velocityMult;
                FollowCam.POI = projectile.transform;
                FollowCam._cameraState = FollowCam.CameraState.Projectile;
                MissionDemolition.ShotFired();
                /* ProjectileLine.S.poi = projectile;*/

            }
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Projectile") && other.gameObject != projectile)
            {
                FollowCam._cameraState = FollowCam.CameraState.ShowBoth;

                other.GetComponent<Projectile>().DestroyThis();

                string currentSceneName = SceneManager.GetActiveScene().name;
                SceneManager.LoadScene(currentSceneName);

            }
            if (other.gameObject.CompareTag("Obstacle"))
            {
                other.GetComponent<CollisionDestruct>().DestroyThis();
            }
        }
    }
}
