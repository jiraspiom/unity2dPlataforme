using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] ManangerInput managerInput;
    [SerializeField] Detection detection;
    [SerializeField] CoyoteTime coyoteTime;

    [Header("Componentes")]
    [SerializeField] new Rigidbody2D rigidbody2D;

    [Header("Physics")]
    [SerializeField] float speed = 8f;
    [SerializeField] float jumpForce = 16f;
    [SerializeField] float fallgravity = 5f;
    [SerializeField] float jumpGravity = 16f;
    [SerializeField] float factorMultiplier;
    [SerializeField] Vector2 jumpForceWall;


    [Header("Variables")]
    [SerializeField] float timerStop = 0.3f;
    [SerializeField] bool isFacingRight = true;
    [SerializeField] int direction;


    [Header("variables private")]
    [SerializeField] JumpSimple playerJumpSimple;
    [SerializeField] JumpBetter playerJumpBetter;
    [SerializeField] JumpWall playerJumpWall;



    // Start is called before the first frame update
    void Start()
    {
        managerInput.OnButtonEvent += ManagerInput_OnbuttonEvent;
        playerJumpSimple = new JumpSimple(rigidbody2D, jumpForce);
        playerJumpBetter = new JumpBetter(rigidbody2D, fallgravity, jumpGravity, rigidbody2D.gravityScale);
        playerJumpWall = new JumpWall(rigidbody2D, factorMultiplier, jumpForceWall, transform, timerStop);
    }

    private void ManagerInput_OnbuttonEvent()
    {
        if (detection.collideGround != null && !playerJumpWall.IsGrabbing || coyoteTime.CanCoyoteTime)
        {
            playerJumpSimple.Jump();
        }
        else if (playerJumpWall.IsGrabbing)
        {
            playerJumpWall.Direction = direction;
            playerJumpWall.Jump();
            StartCoroutine(playerJumpWall.StopMove());
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(managerInput.GetInputX()); //retorna -1, 0, 1
        coyoteTime.Coyote(detection.collideGround != null);
        CheckDirection();

    }

    private void FixedUpdate()
    {
        if (playerJumpWall.CanMove)
        {
            Move();
        }

        if (!playerJumpWall.IsGrabbing)
        {
            playerJumpBetter.IsDown = managerInput.IsDown();
            playerJumpBetter.Jump();
        }

        playerJumpWall.HandleGrab(detection.IsWall, detection.collideGround != null, isFacingRight, managerInput.GetInputX());
        playerJumpWall.StopOnWall();
    }

    private void Move()
    {
        rigidbody2D.velocity = new Vector2(managerInput.GetInputX() * speed, rigidbody2D.velocity.y);
    }

    private void CheckDirection()
    {
        // se olhando para direita e pressionar o botao para esquerda
        if (isFacingRight && managerInput.GetInputX() < 0f)
        {
            Flip();
        }
        // se olhando para esquerda e pressionar o botao para direita
        else if (!isFacingRight && managerInput.GetInputX() > 0f)
        {
            Flip();
        }
    }

    private void Flip()
    {
        direction *= -1;
        isFacingRight = !isFacingRight;
        transform.Rotate(0, 180, 0);
    }

}
