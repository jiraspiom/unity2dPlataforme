using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpWall : JumpPlayer
{
    private float factorMultiplier;
    private Vector2 forceWallJump;
    private Transform transform;

    private float timerStop;
    private int direction;
    private bool canMove = true;
    private bool isGrabbing;
    public int Direction { get => direction; set => direction = value; }
    public bool CanMove { get => canMove; set => canMove = value; }
    public bool IsGrabbing { get => isGrabbing; set => isGrabbing = value; }

    public JumpWall(Rigidbody2D _rb2D, float _factorMultiplier, 
        Vector2 _forceWallJump, Transform _transform, float _timerStop)
    {
        rb2D = _rb2D;
        factorMultiplier = _factorMultiplier;
        forceWallJump = _forceWallJump;
        transform = _transform;
        timerStop = _timerStop;
    }


    public override void Jump()
    {
        Vector2 force = new Vector2(factorMultiplier * forceWallJump.x * -direction, factorMultiplier * forceWallJump.y);
        rb2D.velocity = Vector2.zero;
        rb2D.AddForce(force, ForceMode2D.Impulse);
    }

    public void HandleGrab(bool canGrab,bool isGround, bool isFacingRight, float inputX)
    {
        isGrabbing = false;
        if (canGrab && !isGround) {
            if((isFacingRight && inputX > 0) || (!isFacingRight && inputX < 0))
            {
                isGrabbing = true;
            }
        }
    }

    public void StopOnWall()
    {
        if (isGrabbing)
        {
            rb2D.gravityScale = 0f;
            rb2D.velocity = Vector2.zero;
        }
    }

    public IEnumerator StopMove()
    {
        canMove = false;
        transform.localScale = transform.localScale.x == 1 ? new Vector2(-1, 1) : Vector2.one;
        yield return new WaitForSeconds(timerStop);
        transform.localScale = Vector2.one;
        canMove = true;
    }
}
