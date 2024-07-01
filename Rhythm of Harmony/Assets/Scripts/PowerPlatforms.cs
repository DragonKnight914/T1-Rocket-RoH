using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPlatforms : MonoBehaviour
{
    public bool isActive;
    private BoxCollider2D bc2d;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        bc2d = GetComponent<BoxCollider2D>();
        anim = gameObject.transform.GetChild(0).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive && !bc2d.enabled)
        {
            bc2d.enabled = true;
            anim.SetTrigger("Activate");
        }
        else if (!isActive && bc2d.enabled)
        {
            bc2d.enabled = false;
            anim.SetTrigger("Deactivate");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            
            Player P = collision.GetComponent<Player>();
            if (P != null) //if script is found
            {

            }

            Destroy(this.gameObject); //powerup
        }
 
    }
}
