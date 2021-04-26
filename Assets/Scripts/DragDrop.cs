using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop : MonoBehaviour {

    private Vector3 offset, initialTouched0, currentTouched0;
    private Camera myMainCamera; 

    void Start()
    {
        myMainCamera = Camera.main; // Camera.main is expensive ; cache it here
    }

    void OnMouseDown()
    {
        initialTouched0 = myMainCamera.ScreenToWorldPoint(Input.mousePosition);
        offset = transform.position - initialTouched0;
    }

    void OnMouseDrag()
    {   
        currentTouched0 = myMainCamera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = currentTouched0 + offset;
    }
}
