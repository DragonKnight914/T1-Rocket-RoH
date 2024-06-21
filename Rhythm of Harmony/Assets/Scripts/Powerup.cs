
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Powerup : MonoBehaviour
{

    [SerializeField] private int powerupID = 0;
        //0 = Jump
        //1 = Dash
        //2 = Interact
    [SerializeField] private AudioClip PowerUpSoundClip = null;
    [SerializeField] private AudioClip MusicClip = null;
    [SerializeField] private AudioMixer music = null;
    [SerializeField] private AudioSource SoundEffects;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            
            Player P = collision.GetComponent<Player>();
            if (P != null) //if script is found
            {
                if (powerupID == 0) //if script is found
                {
                    P.maxJumps = 2; //enables ability
                    music.SetFloat("Defi", Mathf.Lerp(-80f, 0, Time.deltaTime));
                    
                } 
                else if (powerupID == 1)
                {
                    P.lyreAbility = true;
                    music.SetFloat("Lyre", Mathf.Lerp(-80f, 0, Time.deltaTime));
                }
                else if (powerupID == 2)
                {
                    P.aulosAbility = true;
                    music.SetFloat("Aulos", Mathf.Lerp(-80f, 0, Time.deltaTime));
                }
                SoundEffects.PlayOneShot(PowerUpSoundClip, 0.1f);
                SoundEffects.PlayOneShot(MusicClip, 0.1f);
            }

            Destroy(this.gameObject); //powerup
        }
 
    }
}
