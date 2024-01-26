using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    private Rigidbody[] rigidBodies;
    private Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        rigidBodies = GetComponentsInChildren<Rigidbody>();
        animator = GetComponent<Animator>();
        DeactiveRagdoll();
        
    }

    public void DeactiveRagdoll()
    {
        foreach (var rigidbody in rigidBodies)
        {
            rigidbody.isKinematic = true;
        }
        animator.enabled = true;
    }
    public void ActiveRagdoll()
    {
        foreach (var rigidbody in rigidBodies)
        {
            rigidbody.isKinematic = false;
        }
        animator.enabled = false;

    }

}
