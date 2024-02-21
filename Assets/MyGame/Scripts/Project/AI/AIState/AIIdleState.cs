using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIIdleState : AIState
{
    AIStateID AIState.GetID()
    {
        return AIStateID.Idle;
    }

    void AIState.Enter(AIAgent agent)
    {
        agent.weapons.DeActiveWeapon();
        agent.navMeshAgent.ResetPath();
    }

    void AIState.Exit(AIAgent agent)
    {

    }

    void AIState.Update(AIAgent agent)
    {
        if (agent.playerTransform.GetComponent<Health>().IsDead()) return;

        Vector3 playerDirection = agent.playerTransform.position - agent.playerTransform.position;
        if (playerDirection.sqrMagnitude > agent.maxSightDistance * agent.maxSightDistance)
        {
            return;
        }

        Vector3 agentDirection = agent.transform.forward;

        playerDirection.Normalize();

        float dotProduct = Vector3.Dot(playerDirection, agentDirection);
        if (dotProduct > 0)
        {
            agent.StateMachine.ChangeState(AIStateID.ChasePlayer);
        }
    }
}
