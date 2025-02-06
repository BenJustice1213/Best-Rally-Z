using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 10, -10); // The Camera's offset position
    public float smoothSpeed = 0.15f;

    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        // Follow the target's position while maintaining the offset
        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);

        
        transform.LookAt(target.position);
    }
}
