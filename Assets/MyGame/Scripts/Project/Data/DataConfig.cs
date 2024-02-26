using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class DataConfig : ScriptableObject
{
    [Header("Player")]
    // character locomotion
    public float JumpHeight;
    public float Gravity;
    public float StepDown;
    public float AirControl;
    public float JumpDamp;
    public float GroundSpeed;
    // character aiming
    public float TurnSpeed = 15f;
    //Health
    public float PlayermaxHealth;

    [Header("AI")]
    //Health
    public float AIMaxHealth;
    public float BlinkDuration = 0.1f;
    //AI Agent
    public float maxTime = 1f;
    public float maxDistance = 5f;
    public float dieForce;
    public float maxSightDistance = 10f;
    //AI Weapon
    public float inaccuracy = 0.4f;
    // AI Weapon IK
    public float angleLimit = 90f;
    public float distanceLimit = 1.5f;

    [Header("UI")]
    public float notifyLoadingTime = 5f;

    [Header("Game Component")]
    public float bounceSpeed = 8;
    public float bounceAmplitude = 0.05f;
    public float rotationSpeed = 90;

}
