using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Default Parameters
    
    private Player p;
    private Animator Anim = null;
    public float speed = 5.0f;
    public float airSpeed = 1.0f;
    private float fireRate = 0.25f; // slowdown
    private float canFire = 0.03f; //elapsed time
    private float canJump = -4f;
    public Rigidbody2D rb;
    public float jumpAmount = 5.0f;
    public float fallAmount = -5.0f;
    private BoxCollider2D boxCollider2d;
    public float frictionAmount = 3.0f;
    public Vector2 boxSize;
    public float castDistance;
    private float horizontalInput;

    //ShurikenFire Direction
    //public Transform shurikenPoint;

    //Player Direction
    private bool faceRight = true;


    //GroundCheck
    [SerializeField] private LayerMask groundMask;

    //Lives
    [SerializeField] public int lives = 3;


    //Powerups
    [SerializeField] public bool canTripleShot = false;
    [SerializeField] public bool canSpeedBoost = false;

    //Objects
    [SerializeField] private GameObject ShurikenPrefab = null;
    [SerializeField] private GameObject TripleShotPrefab = null;

    //private UIManager UI = null; //can hold link to Canvas
    //private SpawnManager SM = null;
    //private GameManager GM = null;

    //Sounds
    [SerializeField] private AudioClip AttackClip = null;
    [SerializeField] private AudioClip DeathClip = null;

    // Start is called before the first frame update
    void Start()
    {
        
        boxCollider2d = transform.GetComponent<BoxCollider2D>();

        //transform.position = new Vector3(-6,-4,0);
        /*Anim = GetComponent<Animator>();
        
        
        UI = GameObject.Find("Canvas").GetComponent<UIManager>();
        //link to UIManager code
        lives = 3;
        UI.UpdateLives(lives); //Starts wgame with 3 lives

        SM = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

        if (SM != null)
        {
            SM.StartSpawnRoutines();
        }

        GM = GameObject.Find("GameManager").GetComponent<GameManager>();*/
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        Move();

        //Bounds();

        //Shoot();

        isGrounded();

        SpeedController();

    }

    /*void FixedUpdate()
    {
        //Move();

        //isGrounded();

        
    }*/

    void Move()
    {
        

        if (horizontalInput > 0 && !faceRight)
        {
            Direction();
            //rb.AddForce(Vector2.right * speed, ForceMode2D.Force);
        }
        else if (horizontalInput < 0 && faceRight)
        {
            Direction();
            //rb.AddForce(Vector2.left * speed, ForceMode2D.Force);
        }
        
        //changes movement direction based on direction faced
        if (isGrounded())
        {
            rb.AddForce(Vector3.right * speed * horizontalInput, ForceMode2D.Force);
            //boxCollider2d.sharedMaterial.friction = frictionAmount;
        }
            
            //transform.Translate(Vector3.right * speed * horizontalInput * Time.deltaTime);
        else if (!isGrounded())
        {
            rb.AddForce(Vector3.right * airSpeed * horizontalInput, ForceMode2D.Force);
            //boxCollider2d.sharedMaterial.friction = 0.0f;
        }    
            
            //rb.AddForce(Vector3.left * speed * horizontalInput, ForceMode2D.Force);
            //transform.Translate(Vector3.left * speed * horizontalInput * Time.deltaTime);
        
        //Checks if can jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            rb.AddForce(Vector2.up * jumpAmount, ForceMode2D.Impulse);
        }
        if (!Input.GetKey(KeyCode.Space) && !isGrounded())
        {
            rb.AddForce(Vector2.up * jumpAmount * fallAmount, ForceMode2D.Force);
        }
            
    }

    private void SpeedController()
    {
        // limiting speed on slope
        /*if (OnSlope() && !leaveSlope)
        {
            if (rb.velocity.magnitude > speed)
                rb.velocity = rb.velocity.normalized * speed;
        }*/

        //limits speed on the ground or air
        Vector2 maxSpeed = new Vector2(rb.velocity.x, 0);

        // limits velocity if necessary
        if (maxSpeed.magnitude > speed)
        {
            Vector2 limitedSpeed = maxSpeed.normalized * speed;
            rb.velocity = new Vector2(limitedSpeed.x, rb.velocity.y);
        }
        

        /*if (maxYSpeed != 0 && rb.velocity.y > maxYSpeed && state == MovementState.air)
        {
            rb.velocity = new Vector3(rb.velocity.x, -maxYSpeed, rb.velocity.z);
            //if (isGrounded)
                //rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        }*/

        // reset y velocity
        //if (rb.velocity.y < 0f && isGrounded)
           //rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
    }

    public bool isGrounded()
    {
        
        
          
        if (Physics2D.BoxCast(transform.position, boxCollider2d.bounds.size, 0f, Vector2.down, castDistance, groundMask))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, boxSize);
    }


    /*void Bounds()
    {
        //upper bound
        if (transform.position.y > 10)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }

        //left bound
        if (transform.position.x < -8f)
        {
            transform.position = new Vector3(-8f, transform.position.y, 0);
        }
        //right bound
        else if (transform.position.x > 8f)
        {
            transform.position = new Vector3(8f, transform.position.y, 0);
        }

    }*/

    /*private void Shoot()
    {
        //Press F OR mouse 0 to fire
        if(Input.GetKeyDown("f") || Input.GetMouseButtonDown(0))
        {
            if (Time.time > canFire)
            {
                AudioSource.PlayClipAtPoint(AttackClip, Camera.main.transform.position);

                if (canTripleShot == false)
                {
                    // clone laser at player's position rotated as normal
                    Instantiate(ShurikenPrefab, shurikenPoint.position, shurikenPoint.rotation);
                    
                    canFire = Time.time + fireRate;
                }
                else
                {
                    // clone laser at player's position rotated as normal
                    Instantiate(TripleShotPrefab, shurikenPoint.position, shurikenPoint.rotation);

                    canFire = Time.time + fireRate;
                }
            }
        }
        
    }*/

    private void Direction()
    {
        faceRight = !faceRight;

        transform.Rotate(0f, 180f, 0f);
    }

    /*public IEnumerator TripleShotPowerDown()
    {
        yield return new WaitForSeconds(5.0f);
        //wait while program runs for 5 seconds            
        canTripleShot = false; //turn off triple shot
    }

    public void TripleShotPowerUp()
    {
        canTripleShot = true;
        
        StartCoroutine(TripleShotPowerDown());
           //run method separate from main program
    }

    public IEnumerator SpeedBoostPowerDown()
    {
        yield return new WaitForSeconds(5.0f);
        //wait while program runs for 5 seconds            
        canSpeedBoost = false; //turn off triple shot
        speed = 5.0f;
    }

    public void SpeedBoostPowerUp()
    {
        canSpeedBoost = true;
        speed = 10.0f;
        
        StartCoroutine(SpeedBoostPowerDown());
           //run method separate from main program
    }*/

    /*public void LifeUp()
    {
        if (lives < 3)
        {
            lives++;
            UI.UpdateLives(lives);
        }    
    }

    public void Damage()
    {
        //Anim.SetTrigger("Hurt");
        lives--; //takes 1 life away
        //UI.UpdateLives(lives); //actual parameter
        Debug.Log("" + lives);

        if (lives < 1)
        {
            //End Game
            GM.gameOver = true;

            UI.ShowTitleScreen();

            //animation
            //Instantiate(explosion, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(DeathClip, Camera.main.transform.position);

            //remove
            Destroy(this.gameObject);

            
        }

        
    }*/
}