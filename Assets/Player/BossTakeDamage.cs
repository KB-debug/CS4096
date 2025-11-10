using Unity.Behavior;
using UnityEngine;

public class BossTakeDamage : MonoBehaviour
{
    public BehaviorGraphAgent agent;

    private BlackboardVariable<float> BossHealth;
    private static float currentHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<BehaviorGraphAgent>();
        agent.GetVariable("SelfHealth", out BossHealth);
        currentHealth = BossHealth.Value;

    }

    // Update is called once per frame
    void Update()
    { 
        BossHealth.Value = currentHealth;
    }


    public static void TakeDamage(float amount)
    {
        currentHealth -= amount;
    }
}
