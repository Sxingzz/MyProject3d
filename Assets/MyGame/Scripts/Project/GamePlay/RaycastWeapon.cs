﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastWeapon : MonoBehaviour
{
    class Bullet
    {
        public float time;
        public Vector3 initialPosition;
        public Vector3 initialVelocity;
        public TrailRenderer tracer;
        
    }

    public ActiveWeapon.WeaponSlot weaponSlot;
    public string weaponName;
    public Transform raycastOrigin;
    public Transform raycastDestination; // đích đến của cái tia
    public ParticleSystem[] muzzleFlash;
    public ParticleSystem hitEffect;

    public TrailRenderer tracerEffect; // hiệu ứng tia đạn

    public bool isFiring = false;

    public int fireRate = 25; // 25 bullet per second

    public float bulletSpeed = 1000f;
    public float bulletDrop = 0f;
   


    private Ray ray;
    private RaycastHit hitInfo;

    private float accumulatedTime;

    private List<Bullet> bullets = new List<Bullet>();
    private float maxLineTime = 3f;

    private Vector3 GetPosition(Bullet bullet)
    {
        // p +  v*t + 0.5*g*t*t
        Vector3 gravity = Vector3.down * bulletDrop;
        return (bullet.initialPosition) + (bullet.initialVelocity * bullet.time) + (0.5f * bullet.time * bullet.time * gravity);
    }

    private Bullet CreateBullet(Vector3 position, Vector3 velocity)
    {
        Bullet bullet = new Bullet();
        bullet.initialPosition = position;
        bullet.initialVelocity = velocity;
        bullet.time = 0f;
        bullet.tracer = Instantiate(tracerEffect, position, Quaternion.identity);
        bullet.tracer.AddPosition(position);
        return bullet;
    }

    public void StartFiring()
    {
        isFiring = true;
        accumulatedTime = 0f;
        FireBullet();

    }

    public void UpdateFiring(float deltaTime) // giả lập đường cong viên đạn
    {
        accumulatedTime += deltaTime;
        float fireInterval = 1.0f / fireRate;
        while(accumulatedTime >= 0f)
        {
            FireBullet();
            accumulatedTime -= fireInterval;
        }
    }
    public void UpdateBullets(float deltaTime)
    {
        SimulateBullets(deltaTime);
        DestroyBullets();
    }
    private void SimulateBullets(float deltaTime)
    {
        bullets.ForEach(bullet =>
        {
            Vector3 p0 = GetPosition(bullet);
            bullet.time += deltaTime;
            Vector3 p1 = GetPosition(bullet);
            RaycastSegment(p0, p1, bullet);
        });
    }

    private void DestroyBullets()
    {
        bullets.RemoveAll(bullet => bullet.time >= maxLineTime);
    }
    private void RaycastSegment(Vector3 start, Vector3 end, Bullet bullet)
    {
        Vector3 direction = end - start;
        float distance = (end - start).magnitude;
        ray.origin = start;
        ray.direction = direction;
        if (Physics.Raycast(ray, out hitInfo))
        {
            hitEffect.transform.position = hitInfo.point;
            hitEffect.transform.forward = hitInfo.normal;
            hitEffect.Emit(1);

            bullet.tracer.transform.position = hitInfo.point;
            bullet.time = maxLineTime;

            var rigidbody = hitInfo.collider.GetComponent<Rigidbody>();
            if (rigidbody)
            {
                rigidbody.AddForceAtPosition(ray.direction * 5, hitInfo.point, ForceMode.Impulse);
            }
        }
        else
        {
            bullet.tracer.transform.position = end;
        }
    }

    private void FireBullet()
    {
        PlayEffect();

        Vector3 velocity = (raycastDestination.position - raycastOrigin.position).normalized * bulletSpeed;
        var bullet = CreateBullet(raycastOrigin.position, velocity);
        bullets.Add(bullet);

        //ray.origin = raycastOrigin.position;
        //ray.direction = raycastDestination.position - raycastOrigin.position;

        //var tracer = Instantiate(tracerEffect, ray.origin, Quaternion.identity);
        //tracer.AddPosition(ray.origin);

        //if (Physics.Raycast(ray, out hitInfo))
        //{
        //    //Debug.DrawLine(ray.origin, hitInfo.point, Color.red, 1f);
        //    hitEffect.transform.position = hitInfo.point;
        //    hitEffect.transform.forward = hitInfo.normal;
        //    hitEffect.Emit(1);

        //    tracer.transform.position = hitInfo.point;
        //}
    }

    public void StopFiring()
    {
        isFiring = false;
        
    }

    private void PlayEffect()
    {
        foreach (var particle in muzzleFlash) 
        { 
            particle.Emit(1);
        }
    }
}
 