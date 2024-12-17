using Assets.Build.Castle;
using Assets.Script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Damage Settings")]
    public float _mass = 100;
    public float _maxSpeed = 20f;
    public float CooldownDuration = 0.3f;
    public float _damageDivider = 10;

    [Header("Settings")]
    [SerializeField] protected GameObject particle;

    [Header("Info")]
    public float _healthPoints;

    [Header("Debug")]
    [SerializeField] protected bool _debugMode = false;


    public Rigidbody2D _rigidbody;
    protected bool _canCollide = true;
    private void Start()
    {

        _rigidbody = GetComponent<Rigidbody2D>();
        _healthPoints = _mass;
    }

    private void OnTriggerEnter2D(Collider2D _collider)
    {
        CalculateDamage(_collider);
    }

    public virtual void CalculateDamage(Collider2D _collider)
    {
        if (!_canCollide) return;
        if (_collider.gameObject.CompareTag("Obstacle"))
        {
            float _collidedMass = _collider.GetComponent<CollisionDestruct>()._mass;
            float _collidedSpeed = _collider.GetComponent<Rigidbody2D>().velocity.magnitude;
            float _currentSpeed = _rigidbody.velocity.magnitude;
            float _damage = (_collidedMass / _damageDivider) * (_collidedSpeed + _currentSpeed);
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
        if (_debugMode) Debug.Log("DESTROYED PROJECTILE");
        Destroy(gameObject);
    }
    protected IEnumerator Timer()
    {
        _canCollide = false;
        yield return new WaitForSeconds(CooldownDuration);
        _canCollide = true;
    }
    // Speed Limiter

    // private void FixedUpdate()
    // {
    //     float _currentSpeed = _rigidbody.velocity.magnitude;
    //     if (_debugMode) Debug.Log(_currentSpeed);
    //     if (_currentSpeed > _maxSpeed || _rigidbody.drag < 0) _rigidbody.drag += 0.1f;
    //     if (_currentSpeed < _maxSpeed && _rigidbody.drag > 0) _rigidbody.drag -= 0.1f;
    // }
}
