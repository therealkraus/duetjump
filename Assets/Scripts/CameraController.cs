using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public bool follow;
    public Transform target;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    Vector3 velocity = Vector3.zero;
    Vector3 desiredPostition;
    Vector3 smoothedPostion;

    private void Update()
    {
        if (follow)
        {         
            desiredPostition = new Vector3(0,target.position.y,0) + offset;
            smoothedPostion = Vector3.SmoothDamp(transform.position, desiredPostition, ref velocity, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPostion;
        }

    }
}
