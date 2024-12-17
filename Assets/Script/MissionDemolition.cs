using UnityEngine;
using UnityEngine.UI;

namespace Assets.Script
{
    public class MissionDemolition : MonoBehaviour
    {
        static private MissionDemolition S;

        [Header("Set in Inspector")]

        public Transform ShowBothPOV;

        public Text uitLevel;
        public Text uitShots;
        public Text uitButton;

        [Header("Set Dynamically")]
        public int level;
        public int levelMax;
        public int shotsTaken;
        public GameObject castle;

        void Start()
        {
            S = this;
            Time.timeScale = 1f;
            StartLevel();
        }
        void StartLevel()
        {
            SwitchView(ShowBothPOV);
            /*   ProjectileLine.S.Clear();*/
            UpdateGUI();
        }
        void UpdateGUI()
        {
            uitLevel.text = "Level: " + (level + 1) + " of " + levelMax;
            uitShots.text = "Shots Taken: " + shotsTaken;
        }
        void Update()
        {
            UpdateGUI();

                //SwitchView(ShowBothPOV);
               // Invoke(nameof(NextLevel), 2f);

        }
        public void SwitchView(Transform transform)
        {
            FollowCam._cameraState = FollowCam.CameraState.ShowBoth;
        }
        public static void ShotFired()
        {
            S.shotsTaken++;
        }
    }
}