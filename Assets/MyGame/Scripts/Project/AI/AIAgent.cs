using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIAgent : MonoBehaviour
{
    public Transform playerTransform;

    public AIStateMachine StateMachine;
    public AIStateID initState;
    public NavMeshAgent navMeshAgent;
    public Ragdoll ragdoll;
    public UIHealthBar UIHealthBar;
    public AIWeapon weapons;

    public float maxTime = 1f;
    public float maxDistance = 5f;
    public float dieForce;
    public float maxSightDistance = 10f;


    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        
        navMeshAgent = GetComponent<NavMeshAgent>();
        ragdoll = GetComponent<Ragdoll>();
        UIHealthBar = GetComponentInChildren<UIHealthBar>();
        weapons = GetComponentInChildren<AIWeapon>();

        navMeshAgent.stoppingDistance = maxDistance;
        StateMachine = new AIStateMachine(this);

        StateMachine.RegisterState(new AIChasePlayerState());
        StateMachine.RegisterState(new AIDeathState());
        StateMachine.RegisterState(new AIIdleState());
        StateMachine.RegisterState(new AIFindWeaponState());
        StateMachine.RegisterState(new AIAttackPlayerState());

        StateMachine.ChangeState(initState);
    }

    // Update is called once per frame
    void Update()
    {
        StateMachine.update();
    }

    public void DisableAll() // fix lỗi enemy chết bị văng
    {
        var allComponents = GetComponents<MonoBehaviour>();

        foreach (var comp in allComponents)
        {
            comp.enabled = false;
        }

        navMeshAgent.enabled = false;
    }
}
