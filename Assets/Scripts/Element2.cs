using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Element2 : MonoBehaviour
{
    private Outline outline;
    void OnMouseDown()
    {
        outline = gameObject.AddComponent<Outline>();
        outline.useGraphicAlpha = false;
        outline.effectColor = new Color(1f, 0f, 0f, .8f);
        outline.effectDistance = new Vector2(1, -1);
    }

    void OnMouseUp()
    {
    }



    public void Destroy()
    {
    	gameObject.SetActive(false);
    }
}