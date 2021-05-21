using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class ThrowObject : MonoBehaviour
{
    private float _damage;
    private bool _destroyOnHit;
    private ParticleSystem _particleSystem;
    [SerializeField] private LayerMask layer;

    private void Start()
    {
        layer = LayerMask.NameToLayer("Enemy");
    }

    private void OnTriggerEnter(Collider other)
    {
        Collided(other.gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        Collided(other.gameObject);
    }

    private void Collided(GameObject gameObject)
    {
        if (gameObject.layer == layer)
        {
            Health health = gameObject.GetComponent<Health>();
            if (health != null)
            {
                health.RemoveHealth(_damage);
            }
        }
        if(_particleSystem != null) {} //instantie partical system 
        if(_destroyOnHit) Destroy(this.gameObject);
    }

    public void Instantiate(float damage, bool destroyOnHit, ParticleSystem particleOnHit)
    {
        this._damage = damage / 2;
        this._destroyOnHit = destroyOnHit;
        this._particleSystem = particleOnHit;
    }
}
