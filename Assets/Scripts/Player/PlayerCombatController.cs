using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    public List<AttackBase> attacks = new List<AttackBase>();
    public AttackBase currentAttack;
    public Animator animator;
    private static readonly int Hurt = Animator.StringToHash("hurt");
    private Health _health;
    private void Start()
    {
        if (animator == null) animator = GetComponent<Animator>();
        if (_health == null) _health = GetComponent<Health>();
        if (_health != null)
        {
            if (animator != null)
            {
                _health.OnHealthChanged.AddListener(value => SetHurt(true));
            }
        }

        
        currentAttack = attacks[0];
    }

    private void Update()
    {
        if (currentAttack == null) return;
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            currentAttack.Use();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            _health.RemoveHealth(10);
        }
    }

    public void CheckCombo()
    {
        currentAttack.ComboCheck();
    }


    public void SetHurt(int value)
    {
        animator.SetBool(Hurt, value == 0);
    }

    public void SetHurt(bool value)
    {
        animator.SetBool(Hurt, value);
    }


}
