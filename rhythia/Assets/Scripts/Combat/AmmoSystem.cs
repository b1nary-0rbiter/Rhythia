using UnityEngine;

public class AmmoSystem : MonoBehaviour
{
    public int maxAmmo = 6;
    public float reloadTime = 5f;

    public int CurrentAmmo { get; private set; }
    public bool IsReloading { get; private set; }
    public float ReloadProgress { get; private set; }

    void Awake()
    {
        CurrentAmmo = maxAmmo;
    }

    public bool TryConsumeAmmo()
    {
        if (IsReloading || CurrentAmmo <= 0) return false;

        CurrentAmmo--;
        if (CurrentAmmo <= 0)
            StartCoroutine(Reload());

        return true;
    }

    System.Collections.IEnumerator Reload()
    {
        IsReloading = true;
        float elapsed = 0f;

        while (elapsed < reloadTime)
        {
            elapsed += Time.deltaTime;
            ReloadProgress = elapsed / reloadTime;
            yield return null;
        }

        CurrentAmmo = maxAmmo;
        ReloadProgress = 1f;
        IsReloading = false;
    }
}
