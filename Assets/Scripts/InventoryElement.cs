using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryElement : MonoBehaviour
{
	private Camera myMainCamera;

    public GameObject component;
	private GameObject element_created;

    private Vector3 _initialPosition, _initialScale;
    Quaternion _initialRotation;

    private Vector3 offset, initialTouched0, initialTouched1, currentTouched0, currentTouched1;
    private bool isSelected;

    void Start()
    {
        myMainCamera = Camera.main; // Camera.main is expensive ; cache it here
        _initialScale = transform.localScale;
        _initialPosition = transform.position;
        _initialRotation = transform.rotation;
    }

    void Update()
    {
        if (Input.touchCount == 2 && isSelected) {
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);

            Vector2 touch0_old = touch0.position - touch0.deltaPosition;
            Vector2 touch1_old = touch1.position - touch1.deltaPosition;

            float magnitude_old = (touch0_old - touch1_old).magnitude;
            float magnitude = (touch0.position - touch1.position).magnitude;
            transform.localScale *= magnitude/magnitude_old;

            float currentAngle = Vector2.SignedAngle(touch0.position - touch1.position, touch0_old - touch1_old);
            transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z - currentAngle);
        }
    }

    void OnMouseDown()
    {
        isSelected = true;
        initialTouched0 = myMainCamera.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y));
        offset = transform.position - initialTouched0;
    }

    void OnMouseDrag() {
        currentTouched0 = myMainCamera.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y));
        currentTouched0.z = initialTouched0.z;
        transform.position = currentTouched0 + offset;
    }


    void OnMouseUp()
    {
        isSelected = false;
        element_created = GameObject.Instantiate(component);
        element_created.transform.position = transform.position;
    	element_created.transform.rotation = transform.rotation;
        element_created.transform.localScale = transform.localScale;
    	Reset();
    }

    public void Reset()
    {
    	transform.position = _initialPosition;
        transform.rotation = _initialRotation;
        transform.localScale = _initialScale;
    }

    public void Destroy()
    {
    	gameObject.SetActive(false);
    }
}