using UnityEngine;

public class CoyoteTime : MonoBehaviour
{
    [Header("Coyote Time")]
    [SerializeField] private float coyoteTime = 0.1f;
    private float coyoteTimerCounter;
    private bool canCoyoteTime;

    public bool CanCoyoteTime { get => canCoyoteTime; set => canCoyoteTime = value; }
    //public bool CanCoyoteTime2 { get; set; }

    public void Coyote(bool isGround)
    {
        if (isGround)
        {
            coyoteTimerCounter = 0f;
            canCoyoteTime = true;
        }
        if (!isGround && canCoyoteTime)
        {
            coyoteTimerCounter += Time.deltaTime;
            if (coyoteTimerCounter > coyoteTime)
            {
                canCoyoteTime = false;
            }
        }
    }

}
