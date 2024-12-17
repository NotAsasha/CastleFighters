using UnityEngine;

namespace Assets.Script
{
    public class DestroyEverything : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("Collided Water" +  other.name);
            if (other.CompareTag("Obstacle") || other.CompareTag("Projectile"))
            {
                Destroy(other.gameObject);
            }
        }
    }
}
