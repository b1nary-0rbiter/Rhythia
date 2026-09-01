// Zombie enemy for VR rhythm game
using UnityEngine;

public class Zombie : MonoBehaviour
{
    [Header("Health")]
    public float maxHealth = 100f;
    public float currentHealth;
    
    [Header("Behavior")]
    public float moveSpeed = 3f;
    public float pulseSpeed = 2f;
    
    private bool isAlive = true;
    private float ringTimer = 0f;
    
    // Reference to ring child object
    [HideInInspector] public Transform ringTransform;
    
    void Start()
    {
        currentHealth = maxHealth;
        // Find ring child if not assigned
        if (ringTransform == null)
        {
            ringTransform = transform.Find("Ring");
        }
    }
    
    void Update()
    {
        if (!isAlive) return;
        
        // Pulse effect on ring
        ringTimer += pulseSpeed * Time.deltaTime;
        if (ringTransform != null)
        {
            // Pulse scale using sine wave
            float scale = 1f + Mathf.Sin(ringTimer) * 0.2f;
            ringTransform.localScale = Vector3.one * scale;
        }
        
        // Basic zombie movement toward player
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime, Space.World);
    }
    
    public void TakeDamage(float dmg)
    {
        currentHealth -= dmg;
        
        // Visual feedback
        Debug.Log("Zombie took " + dmg + " damage. Health: " + currentHealth);
        
        if (currentHealth <= 0 && isAlive)
        {
            Die();
        }
    }
    
    private void Die()
    {
        isAlive = false;
        Debug.Log("Zombie killed!");
        
        // Simple death effect
        GetComponent<Collider>().enabled = false;
        // Destroy after brief delay
        Destroy(gameObject, 2f);
    }
}