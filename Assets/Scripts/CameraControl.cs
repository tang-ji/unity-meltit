using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private Camera myMainCamera;
    private Vector3 touchStart;

    public bool fixedCamera;

    void Start()
    {
        myMainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1) {
            touchStart = myMainCamera.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y));
        }
        if (Input.touchCount == 3 && !fixedCamera) {
            Vector3 direction = touchStart - myMainCamera.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y));
            myMainCamera.transform.position += direction;
        }
    }
}
