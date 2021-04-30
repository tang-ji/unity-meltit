using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Ground : MonoBehaviour
{
    private Explodable _explodable;

    private AudioClip glassbreak, glasshit1, glasshit2;
    private AudioSource audiosource;
    private GameObject soundplayer;
    private float lowPitchRange = .75F;
    private float highPitchRange = 1.5F;
    private float velToVol = .1F;

    void Awake () {
        glassbreak = Resources.Load<AudioClip>("glassBreak3");
    }

    void Start()
    {
        _explodable = GetComponent<Explodable>();
    }

    void playSound(AudioClip audioclip, Collision2D collision)
    {
        soundplayer = GameObject.Instantiate(Resources.Load<GameObject>("SoundPlayer"));
        audiosource = soundplayer.GetComponent<AudioSource>(); 
        soundplayer.tag = "Clone";
        audiosource.pitch = Random.Range(lowPitchRange, highPitchRange);
        float hitVol = collision.relativeVelocity.magnitude * velToVol;
        audiosource.PlayOneShot(audioclip, hitVol);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        float v = collision.relativeVelocity.magnitude, m = GetComponent<Rigidbody2D>().mass, M;
        try{M = collision.gameObject.GetComponent<Rigidbody2D>().mass;} catch(MissingComponentException e) {M = 1000;}
        
        if (M * v / (M + m)  > 2)
        {
            playSound(glassbreak, collision);
            GameObject.Find("Menu").AddComponent<BoxCollider2D>();
            GameObject.Find("Menu").AddComponent<Rigidbody2D>();
            GameObject.Find("Ball").AddComponent<Rigidbody2D>();
            GameObject.Find("Beam").AddComponent<Rigidbody2D>();
            GameObject.Find("Box").AddComponent<Rigidbody2D>();
            Explodable ep = GameObject.Find("Menu").AddComponent<Explodable>();
            GameObject.Find("Bubble.Support").SetActive(false);
            ep.shatterType = Explodable.ShatterType.Voronoi;
            ep.extraPoints = 7;
            ep.subshatterSteps = 1;
            ep.allowRuntimeFragmentation = true;
            ep.explode();
            _explodable.explode();
        }
        
    }
}
