using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ZombieTest : MonoBehaviour
{
    private enum ZombieState
    {
        Walking,
        Ragdoll
    }
    [SerializeField]
    private Camera mainCamera;
    [SerializeField]
    private PhysicMaterial zombiePhysicsMaterials;

    private Rigidbody[] zombieRBs;
    private CharacterJoint[] zombieJoint;
    private Collider[] zombieColliders;

   
   
    private ZombieState currentState = ZombieState.Walking;
    private Animator zombieAnimator;
    private CharacterController zombieCC;



    private void Awake()
    {
        zombieRBs = GetComponentsInChildren<Rigidbody>();
        zombieJoint = GetComponentsInChildren<CharacterJoint>();
        zombieColliders = GetComponentsInChildren<Collider>();
        zombieAnimator = GetComponent<Animator>();
        zombieCC = GetComponent<CharacterController>();
        DisableRagdoll();
        SetUpCHaracterJoint();
        SetUpCollider();
    }


    // Update is called once per frame
    void Update()
    {
       switch(currentState)
        {
            case ZombieState.Walking:
                WalkingBehavior();
                    break;
            case ZombieState.Ragdoll:
                RagdollBehavior(); 
                    break;
        }
    }
    private void SetUpCHaracterJoint()
    {
        foreach (var joint in zombieJoint)
        {
            joint.enableProjection = true;
        }
    }

    private void SetUpCollider()
    {
            foreach (var col in zombieColliders)
        {
            col.material = zombiePhysicsMaterials;
        }
    }

    private void DisableRagdoll()
    {
        foreach (var rigidbody in zombieRBs)
        {
            rigidbody.isKinematic = true;
        }

        zombieAnimator.enabled = true;
        zombieCC.enabled = true;
    }

    private void EnableRagdoll()
    {
        foreach (var rigidbody in zombieRBs)
        {
            rigidbody.isKinematic = false;
        }

        zombieAnimator.enabled = false;
        zombieCC.enabled = false;
    }

    private void WalkingBehavior()
    {
        Vector3 direction = mainCamera.transform.position - transform.position; // điều khiển Zombie đi về hướng camera
        direction.y = 0; // ngăn chặn zombie di chuyển lên xuống khi đi thì set y = 0
        direction.Normalize(); // chuẩn hóa vector để tốc độ zombie k thay đổi khi chi chuyển chéo.

        Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up); // tạo 1 góc quay cho character hường lên, theo 1 cái hướng dc set ở trên
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 20 * Time.deltaTime); // xoay zombie từ góc quay hiện tại đến góc quay ở trên, với tốc độ

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    EnableRagdoll();
        //    currentState = ZombieState.Ragdoll;
        //}
    }

    private void RagdollBehavior()
    {
        this.enabled = false; // tắt scripts để khi died hết xoay về phía camera
    }

    public void TriggerRagdoll(Vector3 force, Vector3 hitPoint) 
    {
        EnableRagdoll();

        //Rigidbody hitRB = zombieRBs.OrderBy(Rigidbody => Vector3.Distance(Rigidbody.position, hitPoint)).First(); // dùng linQ xác định vị trí bị bắn
        Rigidbody hitRb = FindHitRigidbody(hitPoint);

        hitRb.AddForceAtPosition(force, hitPoint, ForceMode.Impulse); // ForceMode.Impulse: tác động liền lập tức, tạo hiệu ứng nảy
        currentState = ZombieState.Ragdoll;

    }

    private Rigidbody FindHitRigidbody(Vector3 hitPoint) // dùng vòng lặp xác định vị trí bị bắn
    {
        Rigidbody closestRigidbody = null;
        float closestDistance = 0;
        foreach (var rb in zombieRBs)
        {
            float distance = Vector3.Distance(rb.position, hitPoint);

            if (closestRigidbody == null || distance < closestDistance)
            {
                closestDistance = distance;
                closestRigidbody = rb;
            }
        }

        return closestRigidbody;
    }
   
}
