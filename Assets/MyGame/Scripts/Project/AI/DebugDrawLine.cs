using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugDrawLine : MonoBehaviour
{
    private void OnDrawGizmos() // hàm của unity vẽ 1 cái line, mở Giz mode mới thấy dc
    {
        Debug.DrawLine(transform.position, transform.position + transform.forward * 100f, Color.red);
    }
}
