using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;
    public float offsetBack = 3f;
    public float offsetUp = 2f;
    public float lookAtOffsetUp = 0.5f;
    public float followSpeed = 10f;

    void Start()
    {
        if (target == null) ResolveTarget();
        SnapToTarget();
    }

    void LateUpdate()
    {
        if (target == null)
        {
            ResolveTarget();
            if (target == null) return;
        }

        Vector3 desired = target.position
            - target.forward * offsetBack
            + Vector3.up * offsetUp;

        transform.position = Vector3.Lerp(transform.position, desired, followSpeed * Time.deltaTime);
        transform.LookAt(target.position + Vector3.up * lookAtOffsetUp);
    }

    void ResolveTarget()
    {
        GameObject hero = GameObject.Find("Hero");
        if (hero != null) target = hero.transform;
    }

    void SnapToTarget()
    {
        if (target == null) return;
        transform.position = target.position
            - target.forward * offsetBack
            + Vector3.up * offsetUp;
        transform.LookAt(target.position + Vector3.up * lookAtOffsetUp);
    }
}
