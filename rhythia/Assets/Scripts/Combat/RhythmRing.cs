using UnityEngine;
using UnityEngine.UI;

public class RhythmRing : MonoBehaviour
{
    public Image fillBar;

    public float cycleTime = 2f;
    public float shootableThreshold = 0.9f;

    private float fill = 0f;

    public bool IsWindowOpen { get; private set; }

    void Update()
    {
        fill += Time.deltaTime / cycleTime;

        if (fill >= 1f)
            fill = 0f;

        fillBar.fillAmount = fill;

        IsWindowOpen = fill >= shootableThreshold;

        fillBar.color = IsWindowOpen ? Color.green : Color.white;
    }
}
