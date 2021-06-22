using UnityEngine;
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
    [SerializeField] private AudioSource footstep;

    //background variables
    private float _moveHor;
    private float _moveVer;
    public bool isGrounded;
    private static readonly int Animation = Animator.StringToHash("Movement");
    public Vector3 lastLandPosition = Vector3.zero;

    private void Start()
    {
        if (groundedChecker == null) groundedChecker = this.gameObject.transform;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        //get input
        _moveHor = Input.GetAxis("Horizontal");
        _moveVer = Input.GetAxis("Vertical"); 
        
        //check if we want to move
        Move();
        
        //check if we want to jump
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < jumps)
        {
            Jump();
        }
        
        //check if we are grounded 
        IsGroundedCheck();
        
        //are we grounded? if yes reset jump
        CheckJumpReset();
        
        //apply gravity and move to new position
        if (_moveVer == 0 && _moveHor == 0) _isSprinting = false;
        if(!isGrounded) moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);

        //animate it!
        Animate();
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
    }

    private void Jump()
    {
        moveDirection.y = jumpSpeed;
        isGrounded = false;
        jumpCount++;
    }

    private void IsGroundedCheck()
    {
        //custom is grounded check for when you move off a cliff.
        RaycastHit hit;
        Debug.DrawRay(groundedChecker.position, -groundedChecker.up * .1f, Color.yellow);
        bool ray = Physics.Raycast(groundedChecker.position, -groundedChecker.up, out hit, .1f);
        if (!ray)
        {
            //we dont hit anything
            isGrounded = false;
        }

        //fixes that we dont go truw water
        OnWaterCollision(ray, hit);
    }

    private void OnWaterCollision(bool ray, RaycastHit hit)
    {
        if (ray)
        {
            if (hit.transform.gameObject.layer == (int) Layer.Water)
            {
                controller.enabled = false;
                gameObject.transform.position = lastLandPosition;
                controller.enabled = true;
            }
            
            if (hit.transform.gameObject.layer == (int) Layer.Land)
            {
                // lastLandPosition = transform.position;
                lastLandPosition = transform.position;
            }
        }
    }

    private void Animate()
    {
        if (anim != null)
        {
            if (moveDirection.x != 0 || moveDirection.z != 0) anim.SetInteger(Animation, 1);
            if (moveDirection.x == 0 && moveDirection.z == 0) anim.SetInteger(Animation, 0);
        }
    }

    private void CheckJumpReset()
    {
        if (!isGrounded) isGrounded = controller.isGrounded; //when we arent grounded check if we are.
        if (isGrounded) jumpCount = 0;
    }

    public void PlayFootstepSound()
    {
        footstep.Play();
    }

    public void CanMove(int value)
    {
        _canMove = value == 0 ? true : false;
    }
}
