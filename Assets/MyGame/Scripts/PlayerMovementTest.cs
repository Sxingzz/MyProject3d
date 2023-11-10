﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementTest : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float turnSpeed; // tốc độ xoay

    [SerializeField]
    private float jumpForce;

    // make variable koyoteTime
    [SerializeField]
    private float jumpButtonGracePeriod;

    private float horizontalInput;
    private float verticalInput;

    private float yForce; // tính toán lực nhảy

    private float originalStepOffset;

    private CharacterController characterController;

    private float? lastGroundedTime; // thời gian cuối cùng character chạm đất, ? có thể null cũng dc
    private float? jumpButtonPressTime; 
  

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        originalStepOffset = characterController.stepOffset;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal"); // trên 3d nên dùng GetAxit thay vì GetAxitRaw
        verticalInput = Input.GetAxis("Vertical");


        Vector3 movementDirection = new Vector3 (horizontalInput, 0, verticalInput);
        
        print($"Vector Magnitude Before normalize: {movementDirection.magnitude}");

        float magnitude = movementDirection.magnitude;
        magnitude = Mathf.Clamp01( magnitude); // giới hạn giá trị magnitude từ 0 đến 1

        movementDirection.Normalize(); // chuẩn hóa vector để khi đi xéo vẫn giữ tốc độ là 1
        print($"Vector Magnitude After normalize: {movementDirection.magnitude}");

        //transform.Translate(magnitude * moveSpeed * Time.deltaTime * movementDirection, Space.World);

        // Jump
        // Koyote time là khoảng thời gian nhỏ mà khi vào khoảng time đó character vẫn jump dc. (chỉ nên dùng trong trường hợp character đi nhanh)

        yForce += Physics.gravity.y * Time.deltaTime; // cho trọng lực tác động, character sẽ tự rơi xuống khi jump

        if (characterController.isGrounded)
        {
            lastGroundedTime = Time.time; // lúc nào mà character trên ground thì biến này sẽ dc set liên tục
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpButtonPressTime = Time.time;
        }

        if (Time.time - lastGroundedTime <= jumpButtonGracePeriod) // rời khỏi vùng ground trong khoảng time mà still press buttton vẫn dc jump
        {
            yForce = -0.5f; // set character chạm đất
            characterController.stepOffset = originalStepOffset; // còn nếu bình thường thì set lại stepoffset như ban đầu

            if (Time.time - jumpButtonPressTime <= jumpButtonGracePeriod)
            {
                yForce = jumpForce;
                jumpButtonPressTime = null;
                lastGroundedTime = null;
            }
        }
        else
        {
            characterController.stepOffset = 0; // đang nhảy k tính đến việc bước lên cầu thang (StepOffset) tránh lỗi bị dính lên tường
        }

        Vector3 velocity = moveSpeed *  magnitude * movementDirection;
        velocity.y = yForce;

        characterController.Move(velocity * Time.deltaTime); // move k tự nhân time.deltatime

        if(movementDirection != Vector3.zero ) // hướng của charactor
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, turnSpeed * Time.deltaTime);
        }
    }
}
