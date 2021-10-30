using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceOff : MonoBehaviour
{

    public GameObject headCheck;
    public LayerMask whatIsPlayer;
    public float overlapCapsuleX = 0.8f;
    public float overlapCapsuleY = 0.25f;
    public bool didPlayerTouchHeadCheck = false;

    private Rigidbody2D playerRb;
    [Range(0, 80.0f)] [SerializeField] private float bounceStrength = 40.0f;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GameObject.Find("Player").GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    public void Run()
    {
        didPlayerTouchHeadCheck = DidPlayerJumpOntoHead();

        if (didPlayerTouchHeadCheck)
        {
            BounceUp();
            KillSelf();
        }
    }

    private bool DidPlayerJumpOntoHead()
    {
        //SpriteRenderer enemySprite = gameObject.GetComponent<SpriteRenderer>();
        Vector2 overlapCapsule = new Vector2(
            overlapCapsuleX,
            overlapCapsuleY);

        return Physics2D.OverlapCapsule(
            headCheck.transform.position,       // Point
            overlapCapsule,                     // Capsule Size
            CapsuleDirection2D.Horizontal,      // Direction of capsule
            0.0f,                               // Angle
            whatIsPlayer);                      // Layer mask

    }

    private void BounceUp()
    {
        // Velocity must be zeroed out before adding force as it produces
        // extreme unexpected results when bouncing up.
        playerRb.velocity = Vector2.zero;
        playerRb.AddForce(Vector2.up * bounceStrength, ForceMode2D.Impulse);
    }

    private void KillSelf()
    {
        Destroy(this.gameObject);
    }

    private void DisableKnockBack()
    {
        Knockback script = GetComponent<Knockback>();
        script.enabled = false;
    }

}
