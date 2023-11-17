using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI healthText;
    [SerializeField]
    private Animator animator;

    private float maxHealth = 100;
    private float currentHealth;
    private int injuredLayerIndex;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        injuredLayerIndex = animator.GetLayerIndex("Injured Layer");

        print($"Injured Layer Index: {injuredLayerIndex}");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            currentHealth -= maxHealth / 10;

            if(currentHealth < 0)
            {
                currentHealth = maxHealth;
            }
        }
        float healthPercentage = currentHealth / maxHealth;
        healthText.text = $"HP: {healthPercentage * 100}%";  


    }
}
