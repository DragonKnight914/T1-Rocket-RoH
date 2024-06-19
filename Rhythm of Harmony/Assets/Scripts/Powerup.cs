
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
    [SerializeField] private AudioMixer Music = null;

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
            //local variable only in this method
            //Script Communication
                //Reach out to what we hit
                //look at component list
                //find script component player and links to it
            Player P = collision.GetComponent<Player>();
            if (P != null) //if script is found
            {
                if (powerupID == 0) //if script is found
                {
                    P.maxJumps = 2; //enables ability
                    AudioSource.PlayClipAtPoint(PowerUpSoundClip, Camera.main.transform.position);
                } 
                else if (powerupID == 1)
                {
                    P.lyreAbility = true;
                    AudioSource.PlayClipAtPoint(PowerUpSoundClip, Camera.main.transform.position);
                }
                else if (powerupID == 2)
                {
                    P.aulosAbility = true;
                    AudioSource.PlayClipAtPoint(PowerUpSoundClip, Camera.main.transform.position);
                }
            }

            Destroy(this.gameObject); //powerup
        }
 
    }
}
