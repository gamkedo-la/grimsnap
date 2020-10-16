using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform followThis;
    Vector3 camOffset;

    // Start is called before the first frame update
    void Start()
    {
        camOffset = followThis.position - transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = followThis.position - camOffset;
    }
}
