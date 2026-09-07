using UnityEngine;
using UnityEngine.UI;

public class AmmoBarUI : MonoBehaviour
{
    public AmmoSystem ammo;
    public Image[] bulletIcons;
    public Image reloadOverlay;

    void Update()
    {
        for (int i = 0; i < bulletIcons.Length; i++)
            bulletIcons[i].enabled = i < ammo.CurrentAmmo;

        reloadOverlay.enabled = ammo.IsReloading;
        if (ammo.IsReloading)
            reloadOverlay.fillAmount = ammo.ReloadProgress;
    }
}
