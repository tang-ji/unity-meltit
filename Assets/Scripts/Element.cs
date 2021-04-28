using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element : MonoBehaviour
{
	private Camera myMainCamera;
    private Plane dragPlane;

    private Vector3 offset, initialTouched0, initialTouched1, currentTouched0, currentTouched1;
    bool isTouched, isMultiTouched, isSelected, validMultiplier;
    private float lastTouchtime;
    public float doubleTouchDelay;
    private Explodable _explodable;

    Quaternion initialRotation;
    float initialAngle, currentAngle, initialDistance, currentDistance, intialMultiplier, currentMultiplier;
    Vector3 initialVector, currentVector, initial_scale;

    void Start()
    {
        myMainCamera = Camera.main; // Camera.main is expensive ; cache it here
        isMultiTouched = false;
        isSelected = false;
        isTouched = false;
        intialMultiplier = currentMultiplier = 1;
        initial_scale = transform.localScale;
        validMultiplier = false;
        lastTouchtime = -10;
        _explodable = GetComponent<Explodable>();
        Physics2D.IgnoreLayerCollision(5, 6, true);
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
                    transform.localScale = initial_scale * intialMultiplier * currentMultiplier;
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

    bool DoubleTouched()
    {
        if(Time.fixedTime - lastTouchtime < doubleTouchDelay) return true;
        lastTouchtime = Time.fixedTime;
        return false;
    }

    void OnMouseDown()
    {
        GetComponent<Rigidbody2D>().isKinematic = true;
        if(DoubleTouched())
        {
            _explodable.explode();
        } 
        isSelected = true;

    }

    void OnMouseUp()
    {
        isSelected = false;
        GetComponent<Rigidbody2D>().isKinematic = false;
        isTouched = false;
        validMultiplier = false;
    }


    public void Destroy()
    {
    	gameObject.SetActive(false);
        Destroy(this.gameObject);
    }
}