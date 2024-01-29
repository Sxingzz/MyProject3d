using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStateMachine 
{
    public AIState[] states;
    public AIAgent agent;
    public AIStateID currentState;

    public AIStateMachine(AIAgent agent)
    {
        this.agent = agent;
        int numOfStates = System.Enum.GetNames(typeof(AIStateID)).Length;
        states = new AIState[numOfStates];
    }

    public void RegisterState(AIState state)
    {
        int index = (int)state.GetID();
        states[index] = state;
    }
    public AIState GetState(AIStateID stateID)
    {
        int index = (int)stateID;
        return states[index];
    }
    public void update()
    {
        GetState(currentState)?.Update(agent);
    }

    public void ChangeState(AIStateID newState)
    {
        GetState(currentState)?.Exit(agent); // phải Exit State cũ, rồi mới chuyển sang State mới
        currentState = newState;
        GetState(currentState)?.Enter(agent);
    }
}
