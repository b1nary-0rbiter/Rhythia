using UnityEngine;

/// Flat moving walkway. Anything standing on it gets carried toward the hero (negative Z).
/// Attach to each Conveyor_ cube. Uses trigger volume on top to find riders.
public class ConveyorBelt : MonoBehaviour
{
    [Tooltip("Units per second toward hero (-Z)")]
    public float speed = 2f;
    public float heroZ = 1.5f;
    public int beltIndex = 0;

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Zombie"))
            MoveRider(other.transform);
    }

    void Update()
    {
        // Move any Zombie whose XZ is over the belt (height doesn't matter)
        Collider beltCol = GetComponent<Collider>();
        if (beltCol == null) return;

        Bounds b = beltCol.bounds;
        foreach (var zombie in FindObjectsByType<Zombie>(FindObjectsSortMode.None))
        {
            if (zombie == null) continue;
            Vector3 p = zombie.transform.position;
            bool overBelt = p.x >= b.min.x && p.x <= b.max.x
                         && p.z >= b.min.z && p.z <= b.max.z;
            if (overBelt)
                MoveRider(zombie.transform);
        }
        // Also check BeatRing zombies (from Asset Store prefabs that have no Zombie.cs yet)
        foreach (var br in FindObjectsByType<BeatRing>(FindObjectsSortMode.None))
        {
            if (br == null) continue;
            Vector3 p = br.transform.position;
            bool overBelt = p.x >= b.min.x && p.x <= b.max.x
                         && p.z >= b.min.z && p.z <= b.max.z;
            if (overBelt)
                MoveRider(br.transform);
        }
    }

    void MoveRider(Transform t)
    {
        t.Translate(Vector3.back * speed * Time.deltaTime, Space.World);

        // Reached the end just in front of hero -> life lost
        if (t.position.z <= heroZ + 1f)
        {
            var hud = FindFirstObjectByType<HUD>();
            if (hud != null) hud.TakeHeartDamage();
            else Debug.Log("Zombie reached hero! Life lost on belt " + beltIndex);

            // Despawn - in real game, return to pool
            Destroy(t.gameObject);
        }
    }
}
