using UnityEngine;
using UnityEngine.InputSystem;

public class HeroController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float turnSpeed = 200f;

    [Header("Bounds")]
    public float hallwayLength = 40f;
    public float hallwayWidth = 10f;

    void Update()
    {
        Keyboard kb = Keyboard.current;
        Vector2 input = Vector2.zero;

        if (kb != null)
        {
            if (kb.wKey.isPressed || kb.upArrowKey.isPressed) input.y += 1f;
            if (kb.sKey.isPressed || kb.downArrowKey.isPressed) input.y -= 1f;
            if (kb.dKey.isPressed || kb.rightArrowKey.isPressed) input.x += 1f;
            if (kb.aKey.isPressed || kb.leftArrowKey.isPressed) input.x -= 1f;
        }

        Vector3 movement = new Vector3(input.x, 0, input.y);
        if (movement.magnitude > 1f) movement.Normalize();

        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);

        if (movement.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                turnSpeed * Time.deltaTime
            );
        }

        float wallMargin = 0.6f;
        float xMin = -hallwayWidth / 2f + wallMargin;
        float xMax = hallwayWidth / 2f - wallMargin;
        float zMin = 0.5f;
        float zMax = hallwayLength - 0.5f;

        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, xMin, xMax);
        pos.z = Mathf.Clamp(pos.z, zMin, zMax);
        transform.position = pos;
    }
}
