using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float speed = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {

        if (Input.GetKey(KeyCode.A))
        {
            transform.localPosition += transform.right * -speed/100;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.localPosition += transform.right * speed / 100;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.localPosition += transform.up * -speed / 100;
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.localPosition += transform.up * speed / 100;
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(0.0f, 0f, 1.5f);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(0.0f, 0f, -1.5f);
        }
    }
}
