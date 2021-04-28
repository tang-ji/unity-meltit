using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragileElement : MonoBehaviour
{
    private Explodable _explodable;

    private AudioClip glassbreak;
    private AudioSource audiosource;
    private GameObject soundplayer;
    private float lowPitchRange = .75F;
    private float highPitchRange = 1.5F;
    private float velToVol = .2F;
    private float velocityClipSplit = 10F;

    void Awake () {
        glassbreak = Resources.Load<AudioClip>("glassBreak1");
        // audiosource = GetComponent<AudioSource>();   
        // if (audiosource == null) audiosource = gameObject.AddComponent<AudioSource>();  
    }

    void Start()
    {
        _explodable = GetComponent<Explodable>();
    }

    void playSound(Collision2D collision)
    {
        soundplayer = GameObject.Instantiate(Resources.Load<GameObject>("SoundPlayer"));
        audiosource = soundplayer.GetComponent<AudioSource>(); 
        soundplayer.tag = "Clone";
        audiosource.pitch = Random.Range(lowPitchRange, highPitchRange);
        float hitVol = collision.relativeVelocity.magnitude * velToVol;
        audiosource.PlayOneShot(glassbreak, hitVol);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        float v = collision.relativeVelocity.magnitude, m = GetComponent<Rigidbody2D>().mass, M;
        try{M = collision.gameObject.GetComponent<Rigidbody2D>().mass;} catch(MissingComponentException e) {M = 1000;}
        
        if (M * v / (M + m)  > 2)
        {
            UnityEditor.EditorApplication.delayCall+=()=>
            {
                playSound(collision);
                _explodable.explode();
            };
        }
        
    }
}
