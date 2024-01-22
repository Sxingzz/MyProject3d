using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLocomotion : MonoBehaviour
{
    public Animator rigController;
    public float jumpHeight;
    public float gravity;
    public float stepDown;
    public float airControl;
    public float jumpDamp;
    public float groundSpeed;

    private Animator animator;
    private Vector2 userInput;
    private CharacterController playerController;

    private Vector3 rootMotion;
    private Vector3 velocity;
    private bool isJumping;

    private int isSprintingParam = Animator.StringToHash("IsSprinting");

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        UpdateAnimation();
    }
    private void GetInput()
    {
        userInput.x = Input.GetAxis("Horizontal");
        userInput.y = Input.GetAxis("Vertical");
        print($"User Input: {userInput}");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        UpdateIsSprinting();
    }

    private void UpdateIsSprinting()
    {
        bool isSprinting = Input.GetKey(KeyCode.LeftShift);
        Debug.Log("isSprintingParam: " +  isSprintingParam);
        animator.SetBool(isSprintingParam, isSprinting);
        rigController.SetBool(isSprintingParam, isSprinting);
    }

    private void UpdateAnimation()
    {
        animator.SetFloat("InputX", userInput.x);
        animator.SetFloat("InputY", userInput.y);

    }

    private void OnAnimatorMove() // Hàm update transform theo animator
    {
        rootMotion += animator.deltaPosition;
    }

    private void FixedUpdate()
    {
        if(isJumping) // In Air State
        {
            UpdateInAir();
        }
        else // Ground State
        {
            UpdateOnGround();
        }
    }

    private void UpdateOnGround()
    {
        Vector3 stepForwardAmount = rootMotion * groundSpeed;
        Vector3 stepDownAmount = Vector3.down * stepDown;

        playerController.Move(stepForwardAmount + stepDownAmount);
        rootMotion = Vector3.zero;

        if (!playerController.isGrounded)
        {
            SetInAirVelocity(0);
        }
    }

    private void UpdateInAir()
    {
        velocity.y -= gravity * Time.fixedDeltaTime;
        Vector3 displacement = velocity * Time.fixedDeltaTime;
        displacement += CalculateAirControl();
        playerController.Move(displacement);
        isJumping = !playerController.isGrounded;
        rootMotion = Vector3.zero;
        animator.SetBool("IsJumping", isJumping);
    }

    private Vector3 CalculateAirControl()
    {
        return ((transform.forward * userInput.y) + (transform.right * userInput.x)) * ( airControl / 100 );
    }

    private void Jump()
    {
        if(!isJumping)
        {
            float jumpVelocity = Mathf.Sqrt(2 * gravity * jumpHeight);
            SetInAirVelocity(jumpVelocity);
        }
    }

    private void SetInAirVelocity(float jumpVelocity)
    {
        isJumping = true;
        velocity = animator.velocity * jumpDamp * groundSpeed;
        velocity.y = jumpVelocity;
        animator.SetBool("IsJumping", true);
    }
}
