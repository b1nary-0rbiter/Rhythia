using UnityEngine;
using UnityEditor;

public class TreadmillLaneSetup : EditorWindow
{
    Transform startPoint;
    Transform endPoint;
    int waypointCount = 5;
    string laneName = "Lane1";

    [MenuItem("Tools/Setup Treadmill Lane")]
    static void ShowWindow()
    {
        GetWindow<TreadmillLaneSetup>("Treadmill Lane Setup");
    }

    void OnGUI()
    {
        GUILayout.Label("Treadmill Lane Generator", EditorStyles.boldLabel);

        laneName = EditorGUILayout.TextField("Lane Name", laneName);
        startPoint = (Transform)EditorGUILayout.ObjectField("Start Point (Portal)", startPoint, typeof(Transform), true);
        endPoint = (Transform)EditorGUILayout.ObjectField("End Point (Near Player)", endPoint, typeof(Transform), true);
        waypointCount = EditorGUILayout.IntField("Waypoint Count", waypointCount);

        GUILayout.Space(10);

        if (GUILayout.Button("Generate Lane"))
        {
            if (startPoint == null || endPoint == null)
            {
                Debug.LogError("Assign both Start Point and End Point first.");
                return;
            }

            GenerateLane();
        }
    }

    void GenerateLane()
    {
        GameObject laneRoot = new GameObject(laneName);

        // SpawnPoint at the start
        GameObject spawnPoint = new GameObject("SpawnPoint");
        spawnPoint.transform.SetParent(laneRoot.transform);
        spawnPoint.transform.position = startPoint.position;

        // Waypoints interpolated between start and end
        for (int i = 0; i <= waypointCount; i++)
        {
            float t = (float)i / waypointCount;
            Vector3 pos = Vector3.Lerp(startPoint.position, endPoint.position, t);

            GameObject wp = new GameObject("Waypoint_" + i);
            wp.transform.SetParent(laneRoot.transform);
            wp.transform.position = pos;
        }

        // Barricade at the end
        GameObject barricade = GameObject.CreatePrimitive(PrimitiveType.Cube);
        barricade.name = "Barricade";
        barricade.transform.SetParent(laneRoot.transform);
        barricade.transform.position = endPoint.position;
        barricade.transform.localScale = new Vector3(2f, 1.5f, 0.3f);

        Collider col = barricade.GetComponent<Collider>();
        DestroyImmediate(col);
        BoxCollider box = barricade.AddComponent<BoxCollider>();
        box.isTrigger = true;

        barricade.AddComponent<BarricadeTrigger>();

        Debug.Log("Lane \"" + laneName + "\" generated with " + (waypointCount + 1) + " waypoints and a barricade.");

        Selection.activeGameObject = laneRoot;
    }
}
