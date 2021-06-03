using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable] public class MyUnityFloatEvent : UnityEvent <float> {}

public class Health : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public float startHealth;

    public UnityEvent<float> RemoveHealthEvent;
    public UnityEvent<float> AddHealthEvent;

    public UnityEvent dieEvent;
    private void Awake()
    {
        health = startHealth;
        if (maxHealth < startHealth) maxHealth = startHealth;

        if (RemoveHealthEvent == null) RemoveHealthEvent = new MyUnityFloatEvent();
        if (AddHealthEvent == null) AddHealthEvent = new MyUnityFloatEvent();
    }

    public void RemoveHealth(float value)
    {
        value = (int) value;
        RemoveHealthEvent?.Invoke(value);

        float newHealth = health - value;
        if (newHealth <= 0) newHealth = 0;
        health = newHealth;
        
        if(health <= 0) { dieEvent.Invoke(); }

        Debug.Log("new hp: " + health);
    }

    public void AddHealth(float value)
    {
        value = (int) value;
        AddHealthEvent.Invoke(value);
        
        float newHealth = health + value;
        if (newHealth >= maxHealth) newHealth = maxHealth;
        health = newHealth;
    }
}
