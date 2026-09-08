using UnityEngine;
using UnityEditor;

public class GameManagerSetup
{
    [MenuItem("Tools/Setup Game Manager")]
    static void Setup()
    {
        GameObject go = Selection.activeGameObject;

        if (go == null)
        {
            Debug.LogError("No GameObject selected. Select your GameManager object first.");
            return;
        }

        if (go.GetComponent<GameManager>() == null)
            go.AddComponent<GameManager>();

        Debug.Log("GameManager component added to: " + go.name);
    }
}
