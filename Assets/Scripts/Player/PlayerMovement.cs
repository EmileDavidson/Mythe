using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 6f;
    [SerializeField] private Vector3 moveDirection = Vector3.zero;
    private bool _canMove = true;
    
    [Header("Sprinting")]
    [SerializeField] private float sprintAmplifier = 2;
    [SerializeField] private bool _canSprint = true;
    private bool _isSprinting = false;

    [Header("Gravity")]
    [SerializeField] private float gravity = 20f;
    [SerializeField] private Transform groundedChecker;
    
    [Header("Jumping")]
    [SerializeField] private float jumpSpeed = 8f;
    [SerializeField] private int jumps = 2;
    [SerializeField]private int jumpCount;

    [Header("Connections")]
    [SerializeField] private CharacterController controller;
    [SerializeField] private Animator anim;

    //background variables
    private float _moveHor;
    private float _moveVer;
    private bool isGrounded;

    private void Start()
    {
        if (groundedChecker == null) groundedChecker = this.gameObject.transform;
    }

    private void Update()
    {
        _moveHor = Input.GetAxis("Horizontal");
        _moveVer = Input.GetAxis("Vertical"); 
        Move();
    }

    private void Move()
    {
        if (_canMove == false) return;
        if (!_isSprinting) _isSprinting = Input.GetKeyDown(KeyCode.LeftControl);
        if (isGrounded) moveDirection.y = 0;
        moveDirection = new Vector3((_moveHor / 2) * (_canSprint ? (_isSprinting ? sprintAmplifier : 1) : 1), 
            moveDirection.y, _moveVer * (_canSprint ? (_isSprinting ? sprintAmplifier : 1) : 1));
        
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection.x *= speed;
        moveDirection.z *= speed;
        
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < jumps)
        {
            moveDirection.y = jumpSpeed;
            isGrounded = false;
            jumpCount++;
        }
        
        //custom is grounded check for when you move off a cliff.
        RaycastHit hit;
        Debug.DrawRay(groundedChecker.position, -groundedChecker.up * .1f, Color.yellow);
        bool ray = Physics.Raycast(groundedChecker.position, -groundedChecker.up, out hit, .1f);
        if (!ray)
        {
            //we dont hit anything
            isGrounded = false;
        }

        if (_moveVer == 0 && _moveHor == 0) _isSprinting = false;
        if(!isGrounded) moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);

        if (!isGrounded) isGrounded = controller.isGrounded; //when we arent grounded check if we are.
        if (isGrounded) jumpCount = 0;
    }


}
