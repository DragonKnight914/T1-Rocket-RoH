using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPlatforms : MonoBehaviour
{
    public bool isActive;
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

            }

            Destroy(this.gameObject); //powerup
        }
 
    }
}
