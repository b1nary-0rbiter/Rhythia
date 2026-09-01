// Ring pulse effect on zombie using material property block
using UnityEngine;

public class RingPulse : MonoBehaviour
{
    [Header("Pulse Settings")]
    public float pulseSpeed = 2f;
    public float minScale = 0.8f;
    public float maxScale = 1.2f;
    
    [Header("References")]
    public Material ringMaterial;
    public string propertyName = "_Scale";
    
    private MaterialPropertyBlock props;
    private float timer = 0f;
    
    void Start()
    {
        // Create property block if material is assigned
        if (ringMaterial != null)
        {
            props = new MaterialPropertyBlock();
        }
    }
    
    void Update()
    {
        timer += pulseSpeed * Time.deltaTime;
        
        // Calculate pulse scale using sine wave
        float t = Mathf.Sin(timer);
        float scale = Mathf.Lerp(minScale, maxScale, (t + 1f) * 0.5f);
        
        // Apply to ring material
        if (ringMaterial != null && props != null)
        {
            props.SetFloat(propertyName, scale);
            GetComponent<Renderer>().SetPropertyBlock(props);
        }
        
        // Alternative: direct transform scale if no material
        // transform.localScale = Vector3.one * scale;
    }
}