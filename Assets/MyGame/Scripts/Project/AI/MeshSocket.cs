using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshSockets : MonoBehaviour
{
    public SocketID socketID;
    public Transform attachPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Attach(Transform objectTransform)
    {
        objectTransform.SetParent(attachPoint, false);
    }
}
