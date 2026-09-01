using UnityEngine;

public class HallwaySceneBuilder : MonoBehaviour
{
    [Header("Hallway Dimensions")]
    public float hallwayLength = 35f;
    public float hallwayWidth = 10f;
    public float hallwayHeight = 6f;

    [Header("Conveyor Belts (Flat Escalators)")]
    public int conveyorCount = 3;
    public float conveyorWidth = 2.6f;
    public float conveyorLength = 18f;
    public float conveyorHeight = 0.3f;
    public float conveyorGap = 0.4f;
    // Conveyors sit a couple feet in front of hero, ending just before him
    public float conveyorEndZ = 5f;
    public float conveyorSpeed = 2f;

    [Header("Hero (Static at Entrance)")]
    public float heroHeight = 1.8f;
    public float heroRadius = 0.4f;
    public Vector3 heroPosition = new Vector3(0, 0.9f, 1.5f);

    [Header("Camera")]
    public float cameraOffsetBack = 4f;
    public float cameraOffsetUp = 2.5f;
    public float cameraLookAtOffsetUp = 1f;

    [Header("Materials")]
    public Material floorMaterial;
    public Material wallMaterial;
    public Material conveyorMaterial;
    public Material heroMaterial;
    public Material headMaterial;

    void Start()
    {
        BuildScene();
    }

    void BuildScene()
    {
        BuildMaterials();
        BuildHallway();
        BuildConveyors();
        BuildHero();
        BuildHordeSpawner();
        BuildThirdPersonCamera();
        BuildLighting();
    }

    void BuildMaterials()
    {
        if (floorMaterial == null) floorMaterial = CreateColorMaterial(new Color(0.85f, 0.85f, 0.82f));
        if (wallMaterial == null) wallMaterial = CreateColorMaterial(new Color(0.92f, 0.92f, 0.90f));
        if (conveyorMaterial == null) conveyorMaterial = CreateColorMaterial(new Color(0.25f, 0.25f, 0.28f));
        if (heroMaterial == null) heroMaterial = CreateColorMaterial(new Color(0.6f, 0.6f, 0.65f));
        if (headMaterial == null) headMaterial = CreateColorMaterial(new Color(0.8f, 0.7f, 0.6f));
    }

    void BuildHallway()
    {
        GameObject hallway = new GameObject("Hallway");

        GameObject floor = GameObject.CreatePrimitive(PrimitiveType.Cube);
        floor.name = "Floor";
        floor.transform.SetParent(hallway.transform);
        floor.transform.localScale = new Vector3(hallwayWidth, 0.2f, hallwayLength);
        floor.transform.localPosition = new Vector3(0, -0.1f, hallwayLength / 2f);
        floor.GetComponent<Renderer>().sharedMaterial = floorMaterial;

        GameObject ceiling = GameObject.CreatePrimitive(PrimitiveType.Cube);
        ceiling.name = "Ceiling";
        ceiling.transform.SetParent(hallway.transform);
        ceiling.transform.localScale = new Vector3(hallwayWidth, 0.2f, hallwayLength);
        ceiling.transform.localPosition = new Vector3(0, hallwayHeight + 0.1f, hallwayLength / 2f);
        ceiling.GetComponent<Renderer>().sharedMaterial = wallMaterial;

        GameObject wallLeft = GameObject.CreatePrimitive(PrimitiveType.Cube);
        wallLeft.name = "Wall_Left";
        wallLeft.transform.SetParent(hallway.transform);
        wallLeft.transform.localScale = new Vector3(0.2f, hallwayHeight, hallwayLength);
        wallLeft.transform.localPosition = new Vector3(-hallwayWidth / 2f - 0.1f, hallwayHeight / 2f, hallwayLength / 2f);
        wallLeft.GetComponent<Renderer>().sharedMaterial = wallMaterial;

        GameObject wallRight = GameObject.CreatePrimitive(PrimitiveType.Cube);
        wallRight.name = "Wall_Right";
        wallRight.transform.SetParent(hallway.transform);
        wallRight.transform.localScale = new Vector3(0.2f, hallwayHeight, hallwayLength);
        wallRight.transform.localPosition = new Vector3(hallwayWidth / 2f + 0.1f, hallwayHeight / 2f, hallwayLength / 2f);
        wallRight.GetComponent<Renderer>().sharedMaterial = wallMaterial;

        // Back wall left OPEN so camera behind hero can see in (otherwise camera is outside and sees white)
        // No Wall_Back - entrance is open

        GameObject wallFront = GameObject.CreatePrimitive(PrimitiveType.Cube);
        wallFront.name = "Wall_Front";
        wallFront.transform.SetParent(hallway.transform);
        wallFront.transform.localScale = new Vector3(hallwayWidth, hallwayHeight, 0.2f);
        wallFront.transform.localPosition = new Vector3(0, hallwayHeight / 2f, hallwayLength + 0.1f);
        wallFront.GetComponent<Renderer>().sharedMaterial = wallMaterial;
    }

    void BuildConveyors()
    {
        GameObject conveyors = new GameObject("ConveyorSystem");

        float totalWidth = conveyorCount * conveyorWidth + (conveyorCount - 1) * conveyorGap;
        float startX = -totalWidth / 2f + conveyorWidth / 2f;

        for (int i = 0; i < conveyorCount; i++)
        {
            float x = startX + i * (conveyorWidth + conveyorGap);

            GameObject belt = GameObject.CreatePrimitive(PrimitiveType.Cube);
            belt.name = "Conveyor_" + (i + 1);
            belt.transform.SetParent(conveyors.transform);
            belt.transform.localScale = new Vector3(conveyorWidth, conveyorHeight, conveyorLength);
            // Center so end is at conveyorEndZ, start is further down hallway
            float centerZ = conveyorEndZ + conveyorLength / 2f;
            belt.transform.localPosition = new Vector3(x, conveyorHeight / 2f, centerZ);
            belt.GetComponent<Renderer>().sharedMaterial = conveyorMaterial;

            // Belt moves zombies toward hero (negative Z). Add trigger to detect zombies on top.
            BoxCollider col = belt.GetComponent<BoxCollider>();
            col.isTrigger = false;

            ConveyorBelt cb = belt.AddComponent<ConveyorBelt>();
            cb.speed = conveyorSpeed;
            cb.heroZ = heroPosition.z;
            cb.beltIndex = i;

            // Visual arrow showing direction (small thin cube on top)
            GameObject arrow = GameObject.CreatePrimitive(PrimitiveType.Cube);
            arrow.name = "DirectionArrow";
            arrow.transform.SetParent(belt.transform);
            arrow.transform.localScale = new Vector3(0.4f, 0.05f, 0.8f);
            arrow.transform.localPosition = new Vector3(0, 0.55f, 0);
            arrow.GetComponent<Renderer>().sharedMaterial = CreateColorMaterial(new Color(0.9f, 0.9f, 0.2f));
            Destroy(arrow.GetComponent<BoxCollider>());

            // Spawn point at far end of this conveyor (where zombies appear)
            GameObject spawn = new GameObject("SpawnPoint_" + (i + 1));
            spawn.transform.SetParent(belt.transform);
            spawn.transform.localPosition = new Vector3(0, 0.6f, conveyorLength / 2f - 1f);
        }
    }

    void BuildHero()
    {
        GameObject hero = new GameObject("Hero");
        hero.tag = "Player";
        hero.transform.position = heroPosition;

        GameObject body = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        body.name = "Body";
        body.transform.SetParent(hero.transform);
        body.transform.localPosition = Vector3.zero;
        body.transform.localScale = new Vector3(heroRadius * 2f, heroHeight / 2f, heroRadius * 2f);
        body.GetComponent<Renderer>().sharedMaterial = heroMaterial;
        Destroy(body.GetComponent<Collider>());

        GameObject head = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        head.name = "Head";
        head.transform.SetParent(hero.transform);
        head.transform.localPosition = new Vector3(0, heroHeight / 2f + 0.15f, 0);
        head.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        head.GetComponent<Renderer>().sharedMaterial = headMaterial;
        Destroy(head.GetComponent<Collider>());

        // Placeholder gun (cube) in front of hero - will be replaced with real gun asset
        GameObject gun = GameObject.CreatePrimitive(PrimitiveType.Cube);
        gun.name = "Gun_Placeholder";
        gun.transform.SetParent(hero.transform);
        gun.transform.localPosition = new Vector3(0.3f, 0.15f, 0.5f);
        gun.transform.localScale = new Vector3(0.08f, 0.08f, 0.4f);
        gun.GetComponent<Renderer>().sharedMaterial = CreateColorMaterial(new Color(0.15f, 0.15f, 0.15f));
        Destroy(gun.GetComponent<Collider>());

        Rigidbody rb = hero.AddComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.useGravity = false;
        rb.isKinematic = true;

        // Hero is static - no movement controller. Faces down hallway (+Z).
        hero.transform.rotation = Quaternion.identity;

        // Shooting - space / left-click, beat window check via BeatRing
        var gunCtrl = hero.AddComponent<GunController>();
        gunCtrl.muzzle = gun.transform;
        gunCtrl.range = hallwayLength;
    }

    void BuildHordeSpawner()
    {
        GameObject spawner = new GameObject("ZombieSpawner");
        spawner.AddComponent<ZombieSpawner>();
        // Prefabs must be assigned in Inspector after first Play - see instructions
    }

    void BuildThirdPersonCamera()
    {
        Camera[] existing = FindObjectsByType<Camera>(FindObjectsSortMode.None);
        foreach (Camera c in existing) Destroy(c.gameObject);

        GameObject camObj = new GameObject("MainCamera");
        camObj.tag = "MainCamera";
        Camera cam = camObj.AddComponent<Camera>();
        cam.nearClipPlane = 0.1f;
        cam.farClipPlane = 200f;
        cam.fieldOfView = 60f;
        cam.clearFlags = CameraClearFlags.Skybox;
        camObj.AddComponent<AudioListener>();

        ThirdPersonCamera tpc = camObj.AddComponent<ThirdPersonCamera>();
        tpc.offsetBack = cameraOffsetBack;
        tpc.offsetUp = cameraOffsetUp;
        tpc.lookAtOffsetUp = cameraLookAtOffsetUp;
    }

    void BuildLighting()
    {
        Light[] existingLights = FindObjectsByType<Light>(FindObjectsSortMode.None);
        foreach (Light l in existingLights) Destroy(l.gameObject);

        // Bright ambient + small warm point light near hero (not dim hallway)
        RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Flat;
        RenderSettings.ambientLight = new Color(0.7f, 0.7f, 0.72f);
        RenderSettings.ambientIntensity = 1f;

        GameObject dirLight = new GameObject("Directional Light");
        dirLight.transform.position = new Vector3(0, 10f, 0);
        dirLight.transform.rotation = Quaternion.Euler(50f, -30f, 0f);
        Light light = dirLight.AddComponent<Light>();
        light.type = LightType.Directional;
        light.intensity = 1f;
        light.shadows = LightShadows.Soft;
        light.color = new Color(1f, 0.98f, 0.92f);

        // Small warm light source near hero - gives the "small light source" you wanted
        GameObject pointLight = new GameObject("HeroLight");
        pointLight.transform.position = heroPosition + new Vector3(2f, 2.5f, 1f);
        Light pl = pointLight.AddComponent<Light>();
        pl.type = LightType.Point;
        pl.intensity = 2f;
        pl.range = 12f;
        pl.color = new Color(1f, 0.9f, 0.7f);
        pl.shadows = LightShadows.Soft;
    }

    Material CreateColorMaterial(Color color)
    {
        Shader shader = Shader.Find("Universal Render Pipeline/Lit");
        if (shader == null) shader = Shader.Find("Standard");
        if (shader == null) shader = Shader.Find("Sprites/Default");
        Material mat = new Material(shader);
        if (mat.HasProperty("_BaseColor")) mat.SetColor("_BaseColor", color);
        else if (mat.HasProperty("_Color")) mat.SetColor("_Color", color);
        return mat;
    }
}
