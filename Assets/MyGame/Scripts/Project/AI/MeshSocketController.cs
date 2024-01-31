using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshSocketController : MonoBehaviour
{

    Dictionary<SocketID, MeshSockets> socketMap = new Dictionary<SocketID, MeshSockets>();

    // Start is called before the first frame update
    void Start()
    {
        MeshSockets[] sockets = GetComponentsInChildren<MeshSockets>();
        foreach ( var socket in sockets)
        {
            socketMap[socket.socketID] = socket;
        }
    }

    public void Attach(Transform objectTransform, SocketID socketID)
    {
        socketMap[socketID].Attach(objectTransform);
    }

  
}
