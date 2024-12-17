using Assets.Build.Castle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Assets.Build.Castle.CollisionDestruct;

public class FireProjectile : Projectile
{
    public override void CalculateDamage(Collider2D _collider)
    {
        if (!_canCollide) return;
        if (_collider.gameObject.CompareTag("Obstacle"))
        {
            float _damage = 0;
            CollisionDestruct collisionDestruct = _collider.GetComponent<CollisionDestruct>();


            if (collisionDestruct.barType == BarType.wood) collisionDestruct.DestroyThis();
            else
            {
                float _collidedMass = collisionDestruct._mass;
                float _collidedSpeed = _collider.GetComponent<Rigidbody2D>().velocity.magnitude;
                float _currentSpeed = _rigidbody.velocity.magnitude;
                _damage = (_collidedMass / _damageDivider) * (_collidedSpeed + _currentSpeed);
                if (_debugMode)
                {
                    Debug.Log(_currentSpeed);
                    Debug.Log(_collidedSpeed);
                    Debug.Log(_damage);
                }
            }


            _healthPoints -= _damage;
            if (_healthPoints <= 0) Invoke("DestroyThis", 0);
            StartCoroutine(Timer());
        }
    }
}