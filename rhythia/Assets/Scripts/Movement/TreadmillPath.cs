using UnityEngine;

public class TreadmillPath : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 3f;
    private int currentIndex = 0;

    void Update()
    {
        if (currentIndex >= waypoints.Length - 1) return;

        Transform target = waypoints[currentIndex + 1];
        transform.position = Vector3.MoveTowards(
            transform.position, target.position, speed * Time.deltaTime);

        Vector3 dir = (target.position - transform.position).normalized;
        if (dir != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(dir);

        if (Vector3.Distance(transform.position, target.position) < 0.05f)
            currentIndex++;
    }
}
