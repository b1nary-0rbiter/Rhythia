using UnityEngine;
using UnityEngine.InputSystem;

/// Simple ray gun on the hero. Press Space or left-click to shoot.
/// Raycasts down hallway (+Z) and checks the hit zombie's BeatRing timing.
public class GunController : MonoBehaviour
{
    public float range = 40f;
    public LayerMask zombieMask = -1;
    public Transform muzzle; // optional - where bullet comes from

    Camera cam;

    void Start()
    {
        cam = Camera.main;
        if (muzzle == null) muzzle = transform;
    }

    void Update()
    {
        bool shoot = false;
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame) shoot = true;
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame) shoot = true;

        if (shoot) Shoot();
    }

    void Shoot()
    {
        Vector3 origin = muzzle.position;
        Vector3 dir = muzzle.forward;
        // Fallback: shoot down hallway from hero
        if (cam != null) { origin = cam.transform.position; dir = cam.transform.forward; }
        else dir = Vector3.forward;

        Debug.DrawRay(origin, dir * range, Color.red, 0.5f);

        if (Physics.Raycast(origin, dir, out RaycastHit hit, range, zombieMask))
        {
            BeatRing ring = hit.collider.GetComponentInParent<BeatRing>();
            if (ring == null) ring = hit.collider.GetComponent<BeatRing>();

            if (ring != null)
            {
                if (ring.IsInWindow())
                {
                    Debug.Log("PERFECT shot! Zombie killed.");
                    Destroy(ring.transform.gameObject);
                    var combat = FindFirstObjectByType<CombatSystem>();
                    if (combat != null) combat.OnKill(100f);
                }
                else
                {
                    Debug.Log("Bad timing - shot missed the beat. Zombie keeps coming.");
                }
            }
            else
            {
                // Hit a zombie with no ring (still kill for testing)
                if (hit.collider.CompareTag("Zombie"))
                {
                    Debug.Log("Hit zombie (no ring) - destroyed.");
                    Destroy(hit.collider.gameObject);
                }
            }
        }
        else
        {
            Debug.Log("Shot missed - no zombie hit.");
        }
    }
}
