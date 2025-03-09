using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPlayer : MonoBehaviour
{
    [Header("Default Parameters")]
    public float speed = 5.0f;
    private float horizontalInput;
    public bool faceRight = true;

    public Rigidbody2D rb;

    public float jumpAmount = 9.0f;
    public float maxFallSpeed;
    public float fallMultiplier = 4.5f;

    [Header("Inputs")]
    public KeyCode jumpKey = KeyCode.Space;


    public int jumpCount;

    [Header("GroundCheck")]
    public Vector2 boxSize;
    public float castDistance;
    [SerializeField] private LayerMask groundMask;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        //Checks if can jump
        if (Input.GetKeyDown(jumpKey))
        {
            if (isGrounded() && jumpCount == 0)
            {
                Jump();
                jumpCount++;

            }

        }
        if (!Input.GetKey(jumpKey) && !isGrounded())
        {
            //rb.AddForce(Vector2.up * jumpAmount * fallAmount, ForceMode2D.Force);
            JumpFall();
        }

        //Resets Jump 
        if (isGrounded() && rb.velocity.y <= 0f)
        {
            jumpCount = 0;
            
        }
    
    }



    // FixedUpdate is called once per in game frame based on the games frame rate
    void FixedUpdate()
    {
        Move();

        isGrounded();
    }




    void Move()
    {
        if (horizontalInput > 0 && !faceRight)
        {
            Direction();
        }
            
        
        else if (horizontalInput < 0 && faceRight)
        {
            Direction();
        }
            

        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
    }

    private void Direction()
    {
        faceRight = !faceRight;

        transform.Rotate(0f, 180f, 0f);
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(Vector2.up * jumpAmount, ForceMode2D.Impulse);
            
    }

    private void JumpFall()
    {
        rb.AddForce(Vector2.up *  -jumpAmount/fallMultiplier, ForceMode2D.Force);
    }




    public bool isGrounded()
    {
          
        if (Physics2D.BoxCast(transform.position - transform.up * castDistance, boxSize, 0f, Vector2.down, 0f, groundMask))
        {
            //Debug.Log("Grounded");
            return true;
        }
        else
        {
            //Debug.Log("Not Grounded");
            return false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, boxSize);
    }

}
