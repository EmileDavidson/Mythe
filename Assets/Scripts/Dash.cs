using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float maxTimeBetweenClicks = .5f;
    [SerializeField] private float dashSpeed = 20;
    [SerializeField] private float dashSlowModifier = 20;
    [SerializeField] private bool isDashing = false;
    [SerializeField] private KeyCode keycode = KeyCode.LeftControl;
    [Header("Connections")]
    [SerializeField] private CharacterController controller;
    
    //background variables
    private float _timePassedBetweenClicks = 0;
    private int _keyClicked = 0;
    private float _currentDashSpeed;


    private void Start()
    {
        _currentDashSpeed = dashSpeed;
    }

    private void Update()
    {
        CheckDash();
        DoDash();
    }

    void CheckDash()
    {
        if (Input.GetKeyDown(keycode)) _keyClicked++;
        if (_keyClicked <= 0) return;
        
        if (_keyClicked >= 2)
        {
            _keyClicked = 0;
            isDashing = true;
        }

        _timePassedBetweenClicks += Time.deltaTime;
        if (_timePassedBetweenClicks > maxTimeBetweenClicks)
        {
            _keyClicked = 0;
            _timePassedBetweenClicks = 0;
        }
    }
    
    
    private void DoDash()
    {
        if (!isDashing) return;

        _currentDashSpeed -= dashSlowModifier * Time.deltaTime;
        if(_currentDashSpeed <= 0)
        {
            //dash is done
            isDashing = false;
            _currentDashSpeed = dashSpeed;
            return;
        }
        
        controller.Move(gameObject.transform.forward * (_currentDashSpeed * Time.deltaTime));
    }
}
