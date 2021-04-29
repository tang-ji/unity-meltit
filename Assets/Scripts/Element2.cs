using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element2 : MonoBehaviour {
	private Camera myMainCamera;
    private Vector3 offset, initialTouched0, initialTouched1, currentTouched0, currentTouched1;
    private bool isSelected;

    private Explodable _explodable;

    void Start() {
        myMainCamera = Camera.main;
        _explodable = GetComponent<Explodable>();
        Physics2D.IgnoreLayerCollision(5, 6, true);
    }

    void Update(){
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

    void OnMouseDown() {
        isSelected = true;
        // GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        initialTouched0 = myMainCamera.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y));
        offset = transform.position - initialTouched0;
    }

    void OnMouseDrag() {
        currentTouched0 = myMainCamera.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y));
        currentTouched0.z = initialTouched0.z;
        transform.position = currentTouched0 + offset;
    }

    void OnMouseUp() {
        isSelected = false;
    }


    public void Destroy() {
    	gameObject.SetActive(false);
        Destroy(this.gameObject);
    }
}