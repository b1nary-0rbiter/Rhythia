// Input handler for VR controllers
using UnityEngine;
using UnityEngine.InputSystem;

public class VRInputHandler : MonoBehaviour
{
    public float swingAngle = -45f;
    public float swingSpeed = 10f;
    
    void Update()
    {
        Keyboard kb = Keyboard.current;

        // Space to trigger swing activation
        if (kb != null && kb.spaceKey.wasPressedThisFrame)
        {
            Attack();
        }
    }
    
    void Attack()
    {
        // Trigger sword swing
        Debug.Log("Attacking!");
    }
}
