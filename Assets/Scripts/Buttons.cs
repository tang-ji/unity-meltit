using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
	public Text physText;
	public bool isPlaying = false;

	public GameObject prefab, instant_prefab;

	GameObject[] _elements;


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

 	public void RestartButtonTouch()
 	{
		Physics2D.gravity = new Vector3(0, -9.81F, 0);
		SceneManager.LoadScene("level_1");
 		// Application.LoadLevel("level_1");
 	}

    public void ResetButtonTouch()
 	{
 		_elements = GameObject.FindGameObjectsWithTag("Clone");
 		foreach (var element in _elements)
 		{
 			Destroy(element);
 			
 		}
 	}

}
