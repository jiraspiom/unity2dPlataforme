
using UnityEngine;

public class JumpBetter : JumpPlayer
{
    private float fallgravity;
    private float jumpGravity;
    private float normalGravity;
    private bool isDown;

    public bool IsDown { get => isDown; set => isDown = value; }
    
    public JumpBetter(Rigidbody2D _rb2D, float _fallgravity, float _jumpGravity, float _normalGravity)
    {
        rb2D = _rb2D;
        fallgravity = _fallgravity;
        jumpGravity = _jumpGravity;
        normalGravity = _normalGravity;
    }
    public override void Jump()
    {
        if (rb2D.velocity.y < 0)
        {
            rb2D.gravityScale = fallgravity;

        }
        else if (rb2D.velocity.y > 0 && !IsDown)
        {
            rb2D.gravityScale = jumpGravity;
        }
        else
        {
            rb2D.gravityScale = normalGravity;
        }
    }
}
