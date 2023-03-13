using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BuddiePlayerController : MonoBehaviour
{
    private BuddieControls buddieControls;
    private InputAction jump;
    private InputAction movement;

    public float jumpForce = 5f;
    public float playerSpeed = 1f;
    private bool isJumping = false;
    private Rigidbody2D myRigidBody;
    private float horizontal;
    public float accelRate;
    public BoxCollider2D rightTrigger;
    public BoxCollider2D leftTrigger;
    public BoxCollider2D groundTrigger;


    private void Awake()
    {
        buddieControls = new BuddieControls();
    }

    void Start()
    {
        myRigidBody = gameObject.GetComponent<Rigidbody2D>();
        

    }

    private void OnEnable()
    {
        jump = buddieControls.GamePlay.Jump;
        jump.performed += Jump;
        jump.Enable();

        movement = buddieControls.GamePlay.Movement; 
        movement.performed += Movement;
        movement.Enable();
    }

    private void OnDisable()
    {
        jump.Disable();
        movement.Disable();

    }
    void Update()
    {
        
        myRigidBody.velocity = new Vector2(Mathf.Clamp(myRigidBody.velocity.x + horizontal * accelRate * Time.deltaTime, -playerSpeed,playerSpeed), myRigidBody.velocity.y);
        


        ;
    }
    
    private void FixedUpdate()
    {
        //myRigidBody.AddForce(new Vector2(horizontal * playerSpeed, 0f), ForceMode2D.Impulse);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            Debug.Log("Player is on the ground");
            isJumping = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            Debug.Log("Player is in the air");
            isJumping = true;
        }
    }
    private void Jump(InputAction.CallbackContext context)
    {
        if (!isJumping)
        {
            myRigidBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            Debug.Log("Jumped");
        }
        
    }

    private void Movement(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
        Debug.Log("Moved");
    }

}
