using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float cameraspeed = 5f;

    public Vector3 offset = new Vector3(0, 3, -10);

    public float MinX, MaxX, MinY, MaxY;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPos = target.position + offset;

        float clampedX = Mathf.Clamp(desiredPos.x, MinX, MaxX);
        float clampedY = Mathf.Clamp(desiredPos.y, MinY, MaxY);

        transform.position = Vector3.Lerp(
            transform.position,
            new Vector3(clampedX, clampedY, offset.z),
            cameraspeed * Time.deltaTime
        );
    }
}
