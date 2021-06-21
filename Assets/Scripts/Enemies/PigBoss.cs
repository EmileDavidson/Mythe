using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PigBoss : MonoBehaviour
{
    public UnityEvent onSpecialMove = new UnityEvent();
    public UnityEvent onSpecialMoveFinish = new UnityEvent();
    public Health health;

    private void Start()
    {
        if(health == null) health = GetComponent<Health>();
        health.OnHealthChanged.AddListener(value =>
        {
            if(value <= health.startHealth / 100 * 30) SpecialMove();
        });
    }
    

    //trigger special move at 30% of hp
    private void SpecialMove()
    {
     onSpecialMove.Invoke();   
    }

    public void ActiveObject(GameObject target)
    {
        target.SetActive(true);
    }
    
    //some build in functions
    public void DeactivateObject(GameObject target)
    {
        target.SetActive(false);
    }
}
