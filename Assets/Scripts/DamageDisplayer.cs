using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class DamageDisplayer : MonoBehaviour
{
    private Health _health;
    private void Start()
    {
        //if there is a health script on this gameobject we acces it.
        _health = gameObject.GetComponent<Health>();
        print(_health);
        _health.RemoveHealthEvent?.AddListener(TakeDamage);
        _health.dieEvent?.AddListener(Die);
    }

    private void Die()
    {
        Destroy(this.gameObject);
    }

    private void TakeDamage(float amount)
    {
        //display logic
        Debug.Log("DISPLAYING");
    }
}
