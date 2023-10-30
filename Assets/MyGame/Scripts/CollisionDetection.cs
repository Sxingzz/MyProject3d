using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OntriggerEnter" +  other.name);
    }

    private void OnTriggerStay(Collider other)
    {

        Debug.Log("OntriggerStay" + other.name);
    }

    private void OnTriggerExit(Collider other)
    {

        Debug.Log("OntriggerExit" + other.name);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("OnCollisionEnter + collision.gameobject.name");
    }

    private void OnCollisionStay(Collision collision)
    {
        Debug.Log("OnCollisionStay + collision.gameobject.name");
    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("OnCollisionExit + collision.gameobject.name");
    }
}
