// Combat system for scoring and multipliers
using UnityEngine;

public class CombatSystem : MonoBehaviour
{
    [Header("Multiplier Settings")]
    public int maxMultiplier = 3;
    public int initialMultiplier = 1;
    
    [Header("State")]
    public int currentMultiplier;
    public int consecutiveKills;
    
    void Start()
    {
        currentMultiplier = initialMultiplier;
        consecutiveKills = 0;
    }
    
    public void OnKill(float damageDealt)
    {
        consecutiveKills++;
        currentMultiplier = Mathf.Min(consecutiveKills, maxMultiplier);
        
        Debug.Log("Kill! Multiplier: x" + currentMultiplier + ", Damage: " + damageDealt);
        
        // TODO: Apply damage to zombie via reference
    }
    
    public void OnMiss()
    {
        consecutiveKills = 0;
        currentMultiplier = initialMultiplier;
        Debug.Log("Miss! Multiplier reset to x1");
        
        // TODO: Deal damage to player
    }
}