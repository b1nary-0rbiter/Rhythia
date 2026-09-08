using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class RingSetup
{
    [MenuItem("Tools/Setup Rhythm Ring")]
    static void SetupRing()
    {
        GameObject zombie = Selection.activeGameObject;

        if (zombie == null)
        {
            Debug.LogError("No GameObject selected in Hierarchy.");
            return;
        }

        if (zombie.transform.Find("RingCanvas") != null)
        {
            Debug.LogWarning("RingCanvas already exists on " + zombie.name + ". Delete it first to rebuild.");
            return;
        }

        // Canvas
        GameObject canvasGO = new GameObject("RingCanvas");
        canvasGO.transform.SetParent(zombie.transform);
        canvasGO.transform.localPosition = new Vector3(0, 3.2f, 0);
        canvasGO.transform.localRotation = Quaternion.identity;
        canvasGO.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);

        Canvas canvas = canvasGO.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;
        canvasGO.AddComponent<CanvasScaler>();
        canvasGO.AddComponent<GraphicRaycaster>();

        RectTransform canvasRect = canvasGO.GetComponent<RectTransform>();
        canvasRect.sizeDelta = new Vector2(200, 200);

        // Background boundary ring (static outline, purely cosmetic)
        Sprite ringSprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Textures/CircleRing.png");
        Sprite filledSprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Textures/CircleFilled.png");

        if (ringSprite != null)
        {
            GameObject boundary = new GameObject("BoundaryRing");
            boundary.transform.SetParent(canvasGO.transform);
            boundary.transform.localPosition = Vector3.zero;
            boundary.transform.localRotation = Quaternion.identity;
            boundary.transform.localScale = Vector3.one;

            RectTransform brect = boundary.AddComponent<RectTransform>();
            brect.sizeDelta = new Vector2(170, 170);

            Image bimg = boundary.AddComponent<Image>();
            bimg.sprite = ringSprite;
            bimg.color = Color.white;
        }

        // FillBar (the single Minecraft-style fill indicator)
        GameObject fillGO = new GameObject("FillBar");
        fillGO.transform.SetParent(canvasGO.transform);
        fillGO.transform.localPosition = Vector3.zero;
        fillGO.transform.localRotation = Quaternion.identity;
        fillGO.transform.localScale = Vector3.one;

        RectTransform frect = fillGO.AddComponent<RectTransform>();
        frect.sizeDelta = new Vector2(150, 150);

        Image fillImg = fillGO.AddComponent<Image>();
        fillImg.sprite = filledSprite != null ? filledSprite : AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UISprite.psd");
        fillImg.type = Image.Type.Filled;
        fillImg.fillMethod = Image.FillMethod.Radial360;
        fillImg.fillOrigin = 0;
        fillImg.color = Color.white;

        // RhythmRing script — only needs the one fill bar now
        RhythmRing ring = canvasGO.AddComponent<RhythmRing>();
        ring.fillBar = fillImg;

        // Billboard script
        canvasGO.AddComponent<Billboard>();

        Debug.Log("Rhythm fill bar created on: " + zombie.name);
    }
}
