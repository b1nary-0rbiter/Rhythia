using UnityEngine;
using UnityEngine.UI;

public class RhythmRing : MonoBehaviour
{
    public RectTransform outerArc;
    public RectTransform innerArc;
    public Image outerArcImage;
    public Image innerArcImage;

    public float outerSpeed = 90f;
    public float innerSpeed = -140f;
    public float toleranceDegrees = 12f;

    public bool IsWindowOpen { get; private set; }

    void Update()
    {
        outerArc.Rotate(0, 0, outerSpeed * Time.deltaTime);
        innerArc.Rotate(0, 0, innerSpeed * Time.deltaTime);

        float diff = Mathf.DeltaAngle(outerArc.eulerAngles.z, innerArc.eulerAngles.z);
        IsWindowOpen = Mathf.Abs(diff) <= toleranceDegrees;

        Color c = IsWindowOpen ? Color.green : Color.red;
        outerArcImage.color = c;
        innerArcImage.color = c;
    }
}
