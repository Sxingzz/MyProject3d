using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootTest : MonoBehaviour
{
    [SerializeField]
    private float maximumForce;

    [SerializeField]
    private float maximumForceTime;

    private float timeMouseButtonDown; // thời gian giữ chuột trái

    private Camera mainCamera;


  
    void Awake()
    {
        mainCamera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            timeMouseButtonDown = Time.time;
        }
        if(Input.GetMouseButtonUp(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition); // tạo 1 cái tia từ input.mouseposition

            if(Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                ZombieTest  zombie = hitInfo.collider.GetComponentInParent<ZombieTest>();

                if(zombie != null)
                {
                    float mouseButtonDownDuration = Time.time - timeMouseButtonDown; // thời gian giữ chuột
                    float forcePercentage = mouseButtonDownDuration / maximumForceTime;
                    float forceMagnitude = Mathf.Lerp(1, maximumForce, forcePercentage); // Lerp: nội suy tuyến tính

                    Vector3 forceDirection = zombie.transform.position - mainCamera.transform.position;
                    forceDirection.y = 1; // Khi bắn vào con zombie lên cao 1 xíu
                    forceDirection.Normalize();

                    Vector3 force = forceMagnitude * forceDirection;

                    zombie.TriggerRagdoll(force, hitInfo.point);
                }
            }
        }
    }
}
