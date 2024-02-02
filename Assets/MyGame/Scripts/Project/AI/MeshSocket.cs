using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshSockets : MonoBehaviour
{
    public SocketID socketID;
    public HumanBodyBones bone;
    public Vector3 offset;
    public Vector3 rotation;
    private Transform attachPoint;
    private Animator animator;
    

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInParent<Animator>();
        attachPoint = new GameObject("Socket_" + socketID).transform;
        attachPoint.SetParent(animator.GetBoneTransform(bone));
        attachPoint.localPosition = offset;
        attachPoint.localRotation = Quaternion.Euler(rotation);
    }

    public void Attach(Transform objectTransform)
    {
        objectTransform.SetParent(attachPoint, false);
    }
}
