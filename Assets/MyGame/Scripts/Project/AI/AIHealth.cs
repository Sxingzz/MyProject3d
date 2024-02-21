using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHealth : Health
{
    public float blinkDuration = 0.1f;


    private AIAgent agent;
    private SkinnedMeshRenderer meshRenderer;
    private UIHealthBar healthBar;

    protected override void OnStart()
    {
        agent = GetComponent<AIAgent>();
        meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        healthBar = GetComponentInChildren<UIHealthBar>();
    }
    protected override void OnDamaged(Vector3 direction)
    {
        StartCoroutine(EnemyFlash());
        healthBar.SetHealthBarPercentage(currentHealth / maxHealth);
    }
    protected override void OnDeath(Vector3 direction)
    {
        AIDeathState deathState = agent.StateMachine.GetState(AIStateID.Death) as AIDeathState;
        deathState.direction = direction;
        agent.StateMachine.ChangeState(AIStateID.Death);
    }
    private IEnumerator EnemyFlash()
    {
        meshRenderer.material.EnableKeyword("_EMISSION");
        yield return new WaitForSeconds(blinkDuration);
        meshRenderer.material.DisableKeyword("_EMISSION");
        StopCoroutine(nameof(EnemyFlash));
    }
}