using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIAgent : MonoBehaviour
{
    public AIStateMachine StateMachine;
    public AIStateID initState;
    public NavMeshAgent navMeshAgent;
    public Ragdoll ragdoll;
    public UIHealthBar UIHealthBar;

    public float maxTime = 1f;
    public float maxDistance = 5f;
    public float dieForce;


    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        ragdoll = GetComponent<Ragdoll>();
        UIHealthBar = GetComponentInChildren<UIHealthBar>();

        navMeshAgent.stoppingDistance = maxDistance;
        StateMachine = new AIStateMachine(this);

        StateMachine.RegisterState(new AIChasePlayerState());
        StateMachine.RegisterState(new AIDeathState());

        StateMachine.ChangeState(initState);
    }

    // Update is called once per frame
    void Update()
    {
        StateMachine.update();
    }
}
