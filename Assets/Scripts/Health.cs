using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable] public class MyUnityIntEvent : UnityEvent <int> {}

public class Health : MonoBehaviour
{
    [Header("Health")]
    public int health;
    public int maxHealth;
    public int startHealth;

    [Header("Events")]
    public UnityEvent<int> OnRemoveHealth = new MyUnityIntEvent();
    public UnityEvent<int> OnAddHealth = new MyUnityIntEvent();
    public UnityEvent<int> OnHealthChanged = new MyUnityIntEvent();
    public UnityEvent dieEvent;

    [Header("booleans")]
    public bool canTakeDamage = true;
    
    private void Awake()
    {
        health = startHealth;
        if (maxHealth < startHealth) maxHealth = startHealth;
    }

    /// <summary>
    /// Remove Health an trigger events. onRemoveHealth & OnHealthChange
    /// </summary>
    /// <param name="value">the value we want to remove</param>
    public void RemoveHealth(int value)
    {
        if (!canTakeDamage) return;
        
        int newHealth = health - value;
        if (newHealth <= 0) newHealth = 0;
        health = newHealth;
        if(health <= 0) { dieEvent.Invoke(); }
        Debug.Log("new hp: " + health);
        OnRemoveHealth?.Invoke(value);
        
        OnHealthChanged.Invoke(health);
    }

    /// <summary>
    /// AddHealth and trigger events OnAddHealth & OnHealthChange
    /// </summary>
    /// <param name="value">the amount we want to add</param>
    public void AddHealth(int value)
    {
        int newHealth = health + value;
        if (newHealth >= maxHealth) newHealth = maxHealth;
        health = newHealth;
        OnAddHealth.Invoke(value);
        
        OnHealthChanged.Invoke(health);
    }

    /// <summary>
    /// Destroy this GameObject.
    /// </summary>
    public void DestroyThisObject()
    {
        Destroy(this.gameObject);
    }
}
