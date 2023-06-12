using UnityEngine;

public class JumpSimple : JumpPlayer
{
    private float jumpForce;

    public JumpSimple(Rigidbody2D _rb2D, float _jumpForce)
    {
        jumpForce = _jumpForce;
        rb2D = _rb2D; // ele do rigidbordy2d protected da heranca da classe JumpPlayer 
    }
    public override void Jump()
    {
        rb2D.velocity = Vector2.up * jumpForce;
    }
}
