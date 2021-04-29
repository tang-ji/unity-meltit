using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Bubble : MonoBehaviour
{
    public GameObject winning;
    void Update()
    {
        if (transform.position.y < -1) {
            winning.SetActive(true);
        }
    }
}
