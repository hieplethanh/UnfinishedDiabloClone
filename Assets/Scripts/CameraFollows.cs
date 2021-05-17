using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollows : MonoBehaviour
{
    public Transform target;
    public Vector3 cameraOffset;
    public float smoothFactor = 0.125f;
    public bool lookAtTarget = true;

    void Start()
    {
        cameraOffset = transform.position - target.transform.position;
    }

    void LateUpdate()
    {
        Vector3 newPosition = target.transform.position + cameraOffset;
        transform.position = Vector3.Slerp(transform.position, newPosition, smoothFactor);

        if (lookAtTarget)
        {
            transform.LookAt(target);
        }
    }
}
