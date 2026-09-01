// Player controller for VR sword gameplay
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    
    [Header("Combat")]
    public float swingForce = 10f;
    
    private Rigidbody rb;
    private XRBaseController xrController;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Auto-assign if not set
        xrController = GetComponentInChildren<XRBaseController>();
    }
    
    void Update()
    {
        // Simple forward movement toward hallway target
        Vector3 dir = Vector3.forward;
        transform.Translate(dir * moveSpeed * Time.deltaTime, Space.World);
        
        // Lateral movement is handled in HeroController via WASD
    }
    
    public void AttemptSwing(float angle)
    {
        // Trigger sword attack animation via XR interaction
        Debug.Log("Swing initiated at angle: " + angle);
        
        // In a real implementation, trigger animation and check collisions
    }
}