using UnityEngine;

[System.Serializable]
public class PortalSet
{
    public GameObject characterPrefab;
    public Transform spawnPoint;
    public Transform[] treadmillPath;
    public ParticleSystem spawnBurst;
}

public class PortalManager : MonoBehaviour
{
    public PortalSet[] portals;
    public float minInterval = 2f;
    public float maxInterval = 6f;

    private int lastIndex = -1;

    void Start()
    {
        ScheduleNextSpawn();
    }

    void ScheduleNextSpawn()
    {
        float delay = Random.Range(minInterval, maxInterval);
        Invoke(nameof(SpawnFromRandomPortal), delay);
    }

    void SpawnFromRandomPortal()
    {
        int index = GetNonRepeatingIndex();
        lastIndex = index;

        PortalSet chosen = portals[index];

        if (chosen.spawnBurst != null)
            chosen.spawnBurst.Play();

        GameObject character = Instantiate(
            chosen.characterPrefab,
            chosen.spawnPoint.position,
            chosen.spawnPoint.rotation
        );

        Transform[] fullPath = new Transform[chosen.treadmillPath.Length + 1];
        fullPath[0] = chosen.spawnPoint;
        chosen.treadmillPath.CopyTo(fullPath, 1);

        TreadmillPath treadmill = character.AddComponent<TreadmillPath>();
        treadmill.waypoints = fullPath;

        StartCoroutine(EmergeEffect(character.transform));

        ScheduleNextSpawn();
    }

    int GetNonRepeatingIndex()
    {
        if (portals.Length <= 1)
            return 0;

        int index;
        do
        {
            index = Random.Range(0, portals.Length);
        } while (index == lastIndex);

        return index;
    }

    System.Collections.IEnumerator EmergeEffect(Transform t)
    {
        t.localScale = Vector3.zero;
        float elapsed = 0f;
        while (elapsed < 0.3f)
        {
            elapsed += Time.deltaTime;
            t.localScale = Vector3.one * (elapsed / 0.3f);
            yield return null;
        }
        t.localScale = Vector3.one;
    }
}
