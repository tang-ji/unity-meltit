using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Bubble : MonoBehaviour
{
    public GameObject startButton;

    private bool isWin;
    private AudioClip Keflavik;
    private AudioSource audiosource;
    private GameObject soundplayer;

    void Awake () {
        Keflavik = Resources.Load<AudioClip>("Keflavik");
    }

    void playSound(AudioClip audioclip) {
        soundplayer = GameObject.Instantiate(Resources.Load<GameObject>("SoundPlayer"));
        audiosource = soundplayer.GetComponent<AudioSource>(); 
        audiosource.PlayOneShot(audioclip);
    }

    void Update() {
        if (transform.position.y < -15 && !isWin) {
            startButton.SetActive(false);
            Physics2D.gravity = new Vector3(0, +0.1f, 0);
            isWin = true;
            playSound(Keflavik);
        }
    }
}
