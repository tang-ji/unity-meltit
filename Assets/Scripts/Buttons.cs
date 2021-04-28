using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
	public Text physText;
	public bool isPlaying = false;

	public GameObject prefab, instant_prefab;

	Explodable[] _elements;


    void Start()
    {
        Time.timeScale = 0;
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
 	{

 	}

 	public void StartButtonTouch()
 	{
 		if(isPlaying) 
 		{
 			Time.timeScale = 0;
 			physText.text = "Start";
 		}
 		else
 		{
 			Time.timeScale = 1;
 			physText.text = "Stop";
 		}

 		isPlaying = !isPlaying;
 	}

 	public void AddButtonTouch()
 	{
 		instant_prefab = GameObject.Instantiate(prefab);
 		// gameObject.transform.position = position;
    	// gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
 	}


    public void ResetButtonTouch()
 	{
 		_elements = FindObjectsOfType<Explodable>();
 		foreach (var element in _elements)
 		{
 			element.Destroy();
 			
 		}
 	}
}
