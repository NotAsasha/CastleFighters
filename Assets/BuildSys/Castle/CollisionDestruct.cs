using Assets.Build.Scripts;
using System.Collections;
using UnityEngine;
using static Assets.Build.Castle.CollisionDestruct;

namespace Assets.Build.Castle
{
    public class CollisionDestruct : MonoBehaviour
    {
        [Header("Damage Settings")]
        public float _mass = 100;
        public float CooldownDuration = 0.3f;
        public float _damageDivider = 10;

        [Header("Settings")]
        public BarType barType;
        [SerializeField] private GameObject particle;

        [Header("Info")]
        [SerializeField] private float _healthPoints;

        [Header("Debug ")]
        [SerializeField] private bool _debugMode = false;


        private Rigidbody2D _rigidbody;
        private bool _canCollide = true;

        public enum BarType
        {
            iron,
            wood,
            glass,
            door
        }

        private void Start()
        {

            _rigidbody = GetComponent<Rigidbody2D>();
            _rigidbody.mass = _mass;
            _healthPoints = _mass;
        }


        private void OnTriggerEnter2D(Collider2D _collider)
        {
            if (!_canCollide) return;
            if (_collider.gameObject.CompareTag("Obstacle"))
            {
                float _collidedMass = _collider.GetComponent<CollisionDestruct>()._mass;
                float _collidedSpeed = _collider.GetComponent<Rigidbody2D>().velocity.magnitude;
                float _currentSpeed = _rigidbody.velocity.magnitude;
                float _damage = _collidedMass / _damageDivider * (_collidedSpeed + _currentSpeed);
                if (_damage < 1) return;
                if (_debugMode)
                {
                    Debug.Log(_currentSpeed);
                    Debug.Log(_collidedSpeed);
                    Debug.Log(_damage);
                }
                _healthPoints -= _damage;
                if (_healthPoints <= 0) Invoke("DestroyThis", 0);
                StartCoroutine(Timer());
            }
            if (_collider.gameObject.CompareTag("Projectile"))
            {
                float _collidedMass = _collider.GetComponent<Projectile>()._mass;
                float _collidedSpeed = _collider.GetComponent<Rigidbody2D>().velocity.magnitude;
                float _currentSpeed = _rigidbody.velocity.magnitude;
                float _damage = _collidedMass / _damageDivider * (_collidedSpeed + _currentSpeed);
                if (_debugMode)
                {
                    Debug.Log(_currentSpeed);
                    Debug.Log(_collidedSpeed);
                    Debug.Log(_damage);
                }

                _healthPoints -= _damage;
                if (_healthPoints <= 0) Invoke("DestroyThis", 0);
                StartCoroutine(Timer());
            }
        }

        public void DestroyThis()
        {
            GameObject Particle = Instantiate(particle);
            Particle.transform.position = transform.position;
            if (_debugMode) Debug.Log("DESTROYED");
            Destroy(gameObject);
        }
        private IEnumerator Timer()
        {
            _canCollide = false;
            yield return new WaitForSeconds(CooldownDuration);
            _canCollide = true;
        }
    }
}
