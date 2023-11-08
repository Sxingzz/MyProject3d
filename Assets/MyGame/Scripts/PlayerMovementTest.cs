using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementTest : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float turnSpeed; // tốc độ xoay

    private float horizontalInput;
    private float verticalInput;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");


        Vector3 movementDirection = new Vector3 (horizontalInput, 0, verticalInput);
        
        print($"Vector Magnitude Before normalize: {movementDirection.magnitude}");
        movementDirection.Normalize(); // chuẩn hóa vector để khi đi xéo vẫn giữ tốc độ là 1
        print($"Vector Magnitude After normalize: {movementDirection.magnitude}");

        transform.Translate(movementDirection * Time.deltaTime, Space.World);

        if(movementDirection != Vector3.zero ) // hướng của charactor
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, turnSpeed * Time.deltaTime);
        }
    }
}
