using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryElement : MonoBehaviour
{
	private Camera myMainCamera;
    private Plane dragPlane;

    public GameObject component;
	private GameObject element_created;

    private Vector3 _initialPosition, _initialScale;
    Quaternion _initialRotation;

    private Vector3 offset, initialTouched0, initialTouched1, currentTouched0, currentTouched1;
    bool isTouched, isMultiTouched, isSelected, validMultiplier;

    Quaternion initialRotation;
    float initialAngle, currentAngle, initialDistance, currentDistance, intialMultiplier, currentMultiplier;
    Vector3 initialVector, currentVector;
    


    void Start()
    {
        myMainCamera = Camera.main; // Camera.main is expensive ; cache it here
        isMultiTouched = false;
        isSelected = false;
        isTouched = false;
        intialMultiplier = currentMultiplier = 1;
        _initialScale = transform.localScale;
        validMultiplier = false;
        _initialPosition = transform.position;
        _initialRotation = transform.rotation;
    }

    void Update()
    {
        if (!isSelected)
        {
            return;
        }

        if (Input.touchCount < 2)
        {
            intialMultiplier *= currentMultiplier;
            currentMultiplier = 1;
            if (isMultiTouched) {
                initialTouched0 = myMainCamera.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y));
                offset = transform.position - initialTouched0;
                isTouched = true;
            }
            isMultiTouched = false;

            if (Input.touchCount == 1)
            {
                if (!isTouched)
                {
                    initialTouched0 = myMainCamera.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y));
                    offset = transform.position - initialTouched0;
                    isTouched = true;
                }
                else
                {
                    currentTouched0 = myMainCamera.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y));
                    transform.position = currentTouched0 + offset;
                }
            }
        }
        else if (Input.touchCount >= 2)
        {
            if (isMultiTouched)
            {
                currentTouched1 = Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(1).position.x, Input.GetTouch(1).position.y));
                currentVector = currentTouched1 - currentTouched0;
                currentAngle = Vector2.SignedAngle(initialVector, currentVector);
                currentDistance = Vector2.Distance(currentTouched1, currentTouched0);
                transform.rotation = Quaternion.Euler(0, 0, initialAngle + currentAngle);
                currentMultiplier = currentDistance / initialDistance;
                if (currentMultiplier > 1.2 || currentMultiplier < 0.8)
                {
                    validMultiplier = true;
                    
                }
                if (validMultiplier){
                    transform.localScale = _initialScale * intialMultiplier * currentMultiplier;
                }
                else
                {
                    currentMultiplier = 1;
                }

            }
            else 
            {
                isMultiTouched = true;
                initialTouched1 = myMainCamera.ScreenToWorldPoint(new Vector3(Input.GetTouch(1).position.x, Input.GetTouch(1).position.y));
                initialVector = initialTouched1 - initialTouched0;
                initialAngle = transform.rotation.eulerAngles.z;
                initialDistance = Vector2.Distance(initialTouched1, initialTouched0);
            }
        }

    }

    void OnMouseDown()
    {
        isSelected = true;   
    }


    void OnMouseUp()
    {
        isSelected = false;
        isTouched = false;
        validMultiplier = false;
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