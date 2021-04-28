using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Explodable))]
public class ExplodeOnDoubleClick : MonoBehaviour {

	private Explodable _explodable;
	private float lastTouchtime;
    public float doubleTouchDelay;

	void Start()
	{
		_explodable = GetComponent<Explodable>();
		lastTouchtime = -10;
	}
	bool DoubleTouched()
    {
        if(Time.fixedTime - lastTouchtime < doubleTouchDelay) return true;
        lastTouchtime = Time.fixedTime;
        return false;
    }
	void OnMouseDown()
	{
		if(DoubleTouched()){
			_explodable.explode();
			// ExplosionForce ef = GameObject.FindObjectOfType<ExplosionForce>();
			// ef.doExplosion(transform.position);
		}
	}
}
