using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = .125f;

    public Vector3 offset;

    void Start()
    {
        offset = new Vector3(0, 4, -5);

    }

    void FixedUpdate()
    {
        Vector3 desiredPos = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPos, smoothSpeed);
        transform.position = smoothedPosition;

        transform.LookAt(target);
    }
}
