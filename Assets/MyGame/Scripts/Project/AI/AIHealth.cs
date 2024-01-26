using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    private Ragdoll ragdoll;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        ragdoll = GetComponent<Ragdoll>();
        SetupHitBox();
    }

    private void SetupHitBox()
    {
        var rigidBodies = GetComponentsInChildren<Rigidbody>();
        foreach (var rigidBody in rigidBodies)
        {
            rigidBody.gameObject.AddComponent<HitBox>().AIHealth = this;
        }
    }

    public void TakeDamage(float damageAmount, Vector3 direction)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        if (ragdoll != null)
        {
            ragdoll.ActiveRagdoll();
        }
        
    }
}