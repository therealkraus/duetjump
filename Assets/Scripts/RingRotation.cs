using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class that deals with rotation of each ring
public class RingRotation : MonoBehaviour
{

    public float speed;
    [Range(-1, 1)]
    public int direction;

    [Space(10)]
    public bool _Rotate360;
    [Space(10)]
    public bool _PingPong;
    public float degrees;

    float startPos;
    float endPos;

    // Use this for initialization
    void Start()
    {
        startPos = transform.localRotation.eulerAngles.y;
        endPos = transform.localRotation.eulerAngles.y + degrees;
    }

    // Update is called once per frame
    void Update()
    {
        if (_Rotate360)
        {
            RotateOnYAxis(direction);
        }
        if (_PingPong)
        {
            float angle = PingPong(Time.time * speed, startPos, endPos);
            transform.localRotation = Quaternion.AngleAxis(angle, Vector3.up);
        }

    }

    //Rotates around y axis, based on direction that is supplied
    void RotateOnYAxis(float direction)
    {
        transform.Rotate(new Vector3(0, direction, 0) * Time.deltaTime * speed, Space.Self);
    }

    //Rotates between two points that are supplied
    float PingPong(float aValue, float aMin, float aMax)
    {
        return direction * Mathf.PingPong(aValue, aMax - aMin) + aMin;
    }
}
