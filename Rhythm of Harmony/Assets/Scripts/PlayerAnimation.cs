using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    private Animator Anim = null;
    private string currentState;
    private Player P = null;

    private float xAxis;
    private float yAxis;
    private Rigidbody2D rb2d;
    private bool isJumpPressed;
    private int groundMask;
    private bool isAulosPressed;
    private bool isAulosPlaying;

    //Animation States
    const string PLAYER_IDLE = "IdleAnim";
    const string PLAYER_WALK = "WalkingAnim";
    const string PLAYER_DASH = "DashAnim";
    const string PLAYER_JUMP = "JumpAnim";
    const string PLAYER_AULOS = "AulosAnim";

    //Animation Durration
    private float aulosTime;


    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        P = GetComponent<Player>();
        //groundMask = 1 << LayerMask.NameToLayer("Ground");
        //conncet to player's animator component
    }

    void ChangeAnimationState(string newState)
    {
        //stop the same animation from interrupting itself
        if (currentState == newState)
        {
            return;
        }

        //play the animation
        Anim.Play(newState);

        //reassign the current state
        currentState = newState;

    }
    // Update is called once per frame
    void Update()
    {

        //Checking directional inputs
        xAxis = Input.GetAxisRaw("Horizontal");

        //Checking Jump input
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJumpPressed = true;
        }

        //Checking Aulos
        if (Input.GetKeyDown("f"))
        {
                isAulosPressed = true;
        }

    }

    private void FixedUpdate()
    {
        if (P.isGrounded() && !P.isDashing && !isAulosPlaying)
        {
            if (xAxis != 0)
            {
                ChangeAnimationState(PLAYER_WALK);
            }
            else
            {
                ChangeAnimationState(PLAYER_IDLE);
            }
        }

        if (P.isDashing)
        {
            ChangeAnimationState(PLAYER_DASH);
        }

        //-------------------------------------
        //Check player trying to jump
        if (isJumpPressed && P.isGrounded() && !P.isDashing && !isAulosPlaying)
        {
            isJumpPressed = false;
            ChangeAnimationState(PLAYER_JUMP);
        }

        if (!P.isGrounded() && !P.isDashing)
        {
            ChangeAnimationState(PLAYER_JUMP);
        } 
        //Check player trying to attack
        if ((isAulosPressed && P.isGrounded()) && !P.isDashing)
        {
            isAulosPressed = false;
            if (!isAulosPlaying)
            {

                isAulosPlaying = true;
                ChangeAnimationState(PLAYER_AULOS);
                aulosTime = Anim.GetCurrentAnimatorStateInfo(0).length;
                Invoke("AulosComplete", aulosTime); 

            }
        }

    }

    void AulosComplete()
    {
        isAulosPlaying = false;
    }

}