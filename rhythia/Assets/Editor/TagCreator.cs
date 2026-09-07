using UnityEngine;
using UnityEditor;

public class TagCreator
{
    [MenuItem("Tools/Add Zombie Tag")]
    static void AddZombieTag()
    {
        SerializedObject tagManager = new SerializedObject(
            AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);

        SerializedProperty tagsProp = tagManager.FindProperty("tags");

        bool alreadyExists = false;
        for (int i = 0; i < tagsProp.arraySize; i++)
        {
            if (tagsProp.GetArrayElementAtIndex(i).stringValue == "Zombie")
            {
                alreadyExists = true;
                break;
            }
        }

        if (!alreadyExists)
        {
            tagsProp.InsertArrayElementAtIndex(tagsProp.arraySize);
            tagsProp.GetArrayElementAtIndex(tagsProp.arraySize - 1).stringValue = "Zombie";
            tagManager.ApplyModifiedProperties();
            Debug.Log("Zombie tag added successfully.");
        }
        else
        {
            Debug.Log("Zombie tag already exists.");
        }
    }
}
