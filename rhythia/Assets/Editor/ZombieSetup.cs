using UnityEngine;
using UnityEditor;

public class ZombieSetup
{
    [MenuItem("Tools/Setup Selected Zombie")]
    static void SetupZombie()
    {
        GameObject zombie = Selection.activeGameObject;

        if (zombie == null)
        {
            Debug.LogError("No GameObject selected in Hierarchy.");
            return;
        }

        // Tag
        zombie.tag = "Zombie";

        // Capsule Collider
        CapsuleCollider col = zombie.GetComponent<CapsuleCollider>();
        if (col == null)
            col = zombie.AddComponent<CapsuleCollider>();

        col.radius = 0.4f;
        col.height = 1.8f;
        col.center = new Vector3(0, 0.9f, 0);

        // ZombieHealth
        if (zombie.GetComponent<ZombieHealth>() == null)
            zombie.AddComponent<ZombieHealth>();

        Debug.Log("Zombie setup complete on: " + zombie.name);
    }
}
