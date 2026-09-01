using UnityEngine;

/// Spawns zombies on the conveyor spawn points at intervals.
/// Auto-finds prefabs in Prefabs/Characters/Zombies if not assigned in Inspector.
public class ZombieSpawner : MonoBehaviour
{
    public GameObject[] zombiePrefabs;
    public float spawnInterval = 2.5f;
    float timer;

    Transform[] spawnPoints;

    void Start()
    {
        // Find SpawnPoint_ objects created by HallwaySceneBuilder
        var points = FindObjectsByType<Transform>(FindObjectsSortMode.None);
        var list = new System.Collections.Generic.List<Transform>();
        foreach (var t in points)
            if (t.name.StartsWith("SpawnPoint_")) list.Add(t);
        spawnPoints = list.ToArray();

        // Auto-find prefabs if not assigned in Inspector
        if (zombiePrefabs == null || zombiePrefabs.Length == 0)
        {
#if UNITY_EDITOR
            var found = new System.Collections.Generic.List<GameObject>();
            string[] paths = {
                "Assets/Prefabs/Characters/Zombies/Zombie_A_SuitMan.prefab",
                "Assets/Prefabs/Characters/Zombies/Zombie_B_Pacient.prefab"
            };
            foreach (var p in paths)
            {
                var go = UnityEditor.AssetDatabase.LoadAssetAtPath<GameObject>(p);
                if (go != null) found.Add(go);
            }
            // Also try any other prefabs in that folder
            if (found.Count == 0)
            {
                string[] guids = UnityEditor.AssetDatabase.FindAssets("t:Prefab", new[] { "Assets/Prefabs/Characters/Zombies" });
                foreach (var g in guids)
                {
                    var go = UnityEditor.AssetDatabase.LoadAssetAtPath<GameObject>(UnityEditor.AssetDatabase.GUIDToAssetPath(g));
                    if (go != null) found.Add(go);
                }
            }
            if (found.Count > 0)
            {
                zombiePrefabs = found.ToArray();
                Debug.Log($"ZombieSpawner: Auto-found {zombiePrefabs.Length} prefab(s).");
            }
            else
            {
                Debug.Log("ZombieSpawner: No zombie prefabs found in Assets/Prefabs/Characters/Zombies/. No zombies will spawn.");
            }
#else
            Debug.Log("ZombieSpawner: Assign zombie prefabs in Inspector. No zombies will spawn until then.");
#endif
        }
    }

    void Update()
    {
        if (spawnPoints == null || spawnPoints.Length == 0) return;
        if (zombiePrefabs == null || zombiePrefabs.Length == 0) return;

        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnOne();
        }
    }

    void SpawnOne()
    {
        Transform sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject prefab = zombiePrefabs[Random.Range(0, zombiePrefabs.Length)];
        // Spawn slightly above belt so feet are on surface, not inside it
        Vector3 pos = sp.position + Vector3.up * 0.5f;
        GameObject z = Instantiate(prefab, pos, Quaternion.LookRotation(Vector3.back));
        z.name = prefab.name + "_Clone";
        z.tag = "Zombie";
        // Force visible scale (some packs import at 0.01)
        if (z.transform.localScale.magnitude < 0.5f)
            z.transform.localScale = Vector3.one;
        // Ensure collider for raycast + conveyor
        if (z.GetComponent<Collider>() == null)
        {
            var bc = z.AddComponent<BoxCollider>();
            bc.center = new Vector3(0, 0.9f, 0);
            bc.size = new Vector3(0.6f, 1.8f, 0.6f);
        }
        // Add beat ring above head
        if (z.GetComponent<BeatRing>() == null) z.AddComponent<BeatRing>();
        z.layer = 0;
        Debug.Log($"Spawned {z.name} at {pos} on {sp.name}");
    }
}
