using Assets.Build.Castle;
using Assets.Script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SlingshotCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Projectile"))
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
