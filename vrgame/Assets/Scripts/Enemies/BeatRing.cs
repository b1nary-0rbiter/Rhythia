using UnityEngine;

/// Concentric beat ring above a zombie's head.
/// Outer ring is static. Inner ring scales from small -> outer size on the beat.
/// When inner == outer (within window), shooting scores. Call IsInWindow() from Gun.
public class BeatRing : MonoBehaviour
{
    [Header("Beat")]
    public float bpm = 120f;
    public float perfectWindow = 0.15f; // seconds around alignment that counts as hit

    [Header("Visual")]
    public Color outerColor = new Color(1f, 1f, 1f, 0.6f);
    public Color innerColor = new Color(1f, 0.2f, 0.2f, 0.8f);
    public float ringHeight = 2.2f; // above zombie pivot
    public float outerRadius = 0.6f;

    Transform outerRing;
    Transform innerRing;
    float beatInterval;
    float beatTimer;

    void Start()
    {
        beatInterval = 60f / Mathf.Max(1f, bpm);
        beatTimer = Random.Range(0f, beatInterval); // stagger zombies

        BuildRings();
    }

    void BuildRings()
    {
        // Outer ring - static torus-ish (thin cylinder)
        outerRing = CreateRing("OuterRing", outerRadius, 0.06f, outerColor);
        outerRing.SetParent(transform, false);
        outerRing.localPosition = new Vector3(0, ringHeight, 0);
        outerRing.localRotation = Quaternion.Euler(90, 0, 0);

        // Inner ring - scales with beat
        innerRing = CreateRing("InnerRing", 0.05f, 0.08f, innerColor);
        innerRing.SetParent(transform, false);
        innerRing.localPosition = new Vector3(0, ringHeight + 0.02f, 0);
        innerRing.localRotation = Quaternion.Euler(90, 0, 0);
    }

    Transform CreateRing(string name, float radius, float thickness, Color color)
    {
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        go.name = name;
        // Make it ring-like: flat and with hole via scale trick (thin)
        go.transform.localScale = new Vector3(radius * 2f, 0.02f, radius * 2f);
        var rend = go.GetComponent<Renderer>();
        Material mat = new Material(Shader.Find("Universal Render Pipeline/Lit") ?? Shader.Find("Standard"));
        if (mat.HasProperty("_BaseColor")) mat.SetColor("_BaseColor", color);
        else mat.SetColor("_Color", color);
        // Make somewhat transparent
        if (mat.HasProperty("_Surface")) mat.SetFloat("_Surface", 1);
        rend.sharedMaterial = mat;
        Destroy(go.GetComponent<Collider>());
        return go.transform;
    }

    void Update()
    {
        beatTimer += Time.deltaTime;
        if (beatTimer > beatInterval) beatTimer -= beatInterval;

        // 0 -> 1 over one beat, inner grows from center to outer
        float t = beatTimer / beatInterval;
        float scale = Mathf.Lerp(0.1f, 1f, t);
        if (innerRing != null)
            innerRing.localScale = new Vector3(outerRadius * 2f * scale, 0.02f, outerRadius * 2f * scale);
    }

    /// Call from gun when player shoots this zombie. Returns true if timing was good.
    public bool IsInWindow()
    {
        float t = beatTimer / beatInterval; // 0..1
        // Perfect when t near 1 (inner == outer). Window wraps around beat boundary.
        float distToPerfect = Mathf.Min(Mathf.Abs(t - 1f), t);
        return distToPerfect <= perfectWindow / beatInterval;
    }

    public float BeatProgress => beatTimer / beatInterval;
}
