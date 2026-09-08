using UnityEngine;
using UnityEditor;

public class PortalManagerSetup
{
    [MenuItem("Tools/Setup Portal Manager")]
    static void Setup()
    {
        GameObject go = Selection.activeGameObject;

        if (go == null)
        {
            Debug.LogError("No GameObject selected. Select your PortalManager object first.");
            return;
        }

        if (go.GetComponent<PortalManager>() == null)
            go.AddComponent<PortalManager>();

        Debug.Log("PortalManager component added to: " + go.name);
    }
}
