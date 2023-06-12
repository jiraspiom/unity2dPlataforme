using UnityEngine;

public class Detection : MonoBehaviour
{
    [Header("Layer Chao")]
    [SerializeField] private LayerMask layerGround;
    [SerializeField] private float groundRadius = 0.2f;
    [SerializeField] private Transform groundPoint;

    [Header("Layer Parede")]
    //[Range(0, 10)]
    [SerializeField] private LayerMask layerWall;
    [SerializeField] private float wallRadius = 0.2f;
    [SerializeField] private Transform wallPoint;

    [Header("Variaveis publica")]
    public Collider2D collideGround;
    public Collider2D collideWall;
    public bool IsWall;

    // Update is called once per frame
    void Update()
    {
        HandleGround();
        HaldleWall();
    }

    private void HandleGround()
    {
        collideGround = Physics2D.OverlapCircle(groundPoint.position, groundRadius, layerGround);
    }

    private void HaldleWall()
    {
        collideWall = Physics2D.OverlapCircle(wallPoint.position, wallRadius, layerWall);
        if (collideWall != null)
        {
            IsWall = true;
        }
        else { IsWall = false; }
    }

    private void OnDrawGizmos()
    {
        if (groundPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundPoint.position, groundRadius);
        }
        if (wallPoint != null)
        {
            Gizmos.color = Color.yellow;
            if(collideWall != null)
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(wallPoint.position, wallRadius);
        }
    }
}
