using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    public Transform Camera;
    public Vector3 targetPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        Camera = GameObject.Find("CameraHolder").transform;
        targetPosition = new Vector3(transform.position.x, -Camera.position.y, -Camera.position.z);
        
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        targetPosition = new Vector3(transform.position.x, -Camera.position.y, -Camera.position.z);
        transform.LookAt(targetPosition);
    }
}

