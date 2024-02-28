using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;


    // Start is called before the first frame update
    void Start()
    {
        
        SetupHitBox();
        OnStart();
    }

    private void SetupHitBox()
    {
        var rigidBodies = GetComponentsInChildren<Rigidbody>();
        foreach (var rigidBody in rigidBodies)
        {
            rigidBody.gameObject.AddComponent<HitBox>().Health = this;
            if (rigidBody.gameObject != gameObject)
            {
                rigidBody.gameObject.layer = LayerMask.NameToLayer("Hitbox");
            }
        }
    }

    public void TakeDamage(float damageAmount, Vector3 direction)
    {
        OnDamaged(direction);
        currentHealth -= damageAmount;
        
        if (currentHealth <= 0f)
        {
            Die(direction);
        }
    }

    public void TakeHealth(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Min(currentHealth, maxHealth);
        // Onhealth(amount)
    }

    private void Die(Vector3 direction)
    {
        OnDeath(direction);
        
    }

    public bool IsDead()
    {
        return currentHealth <= 0;
    }
    protected virtual void OnHealth(float amount)
    {

    }

    protected virtual void OnStart()
    {

    }

    protected virtual void OnDeath(Vector3 direction)
    {

    }

    protected virtual void OnDamaged(Vector3 direction)
    {

    }

}
