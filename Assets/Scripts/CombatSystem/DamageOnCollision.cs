using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Random = UnityEngine.Random;

public class DamageOnCollision : MonoBehaviour
{
   private bool _active = false;
   private int minDamage;
   private int maxDamage;

   public void SetDamageValues(int smallestDamage, int biggestDamage)
   {
      this.minDamage = smallestDamage;
      this.maxDamage = biggestDamage;
   }

   public bool IsActive
   {
      get => _active;
      set => _active = value;
   }

   private void OnTriggerEnter(Collider other)
   {
      if (!_active) return;
      Damage(other.gameObject);

    }

   private void OnCollisionEnter(Collision other)
   {
      if (!_active) return;
      Damage(other.gameObject);

    }
   
   private void Damage(GameObject target)
   {
      if (target.layer != (int) Layer.Enemy) return;
      Health targetHealth = target.GetComponent<Health>();
      if (targetHealth == null) return;
      targetHealth.RemoveHealth(Random.Range(minDamage, maxDamage));
   }
}
