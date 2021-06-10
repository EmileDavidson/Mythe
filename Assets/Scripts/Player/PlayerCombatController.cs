﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    public List<AttackBase> attacks = new List<AttackBase>();
    public AttackBase currentAttack;

    private void Start()
    {
        currentAttack = attacks[0];
    }

    private void Update()
    {
        if (currentAttack == null) return;
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            currentAttack.Use();
        }
    }

    public void CheckCombo()
    {
        currentAttack.ComboCheck();
    }


    
    
}
