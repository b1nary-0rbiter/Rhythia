using UnityEngine;

public class Shooter : MonoBehaviour
{
    public Camera playerCamera;
    public float range = 50f;
    public LayerMask hittableLayers;
    public AmmoSystem ammo;
    public AudioClip missSfx;
    public AudioClip emptySfx;
    public float fireCooldown = 0.4f;

    private float lastShotTime = -999f;

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= lastShotTime + fireCooldown)
            Shoot();
    }

    void Shoot()
    {
        lastShotTime = Time.time;

        if (!ammo.TryConsumeAmmo())
        {
            AudioSource.PlayClipAtPoint(emptySfx, transform.position);
            return;
        }

        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out RaycastHit hit, range, hittableLayers))
        {
            if (hit.collider.CompareTag("Zombie"))
            {
                RhythmRing ring = hit.collider.GetComponentInChildren<RhythmRing>();
                ZombieHealth zh = hit.collider.GetComponent<ZombieHealth>();

                if (ring != null && ring.IsWindowOpen)
                    zh.TakeDamage(999);
                else
                    AudioSource.PlayClipAtPoint(missSfx, hit.point);
            }
        }
    }
}
