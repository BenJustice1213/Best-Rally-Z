using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // The character that the camera will follow
    public Vector3 offset = new Vector3(0, 10, -10); // Offset position of the camera relative to the character

    void LateUpdate()
    {
        // Follow the target's position while maintaining the offset
        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * 5f);

        // Optionally, you can adjust the camera's rotation to look at the target
        transform.LookAt(target.position);
    }
}