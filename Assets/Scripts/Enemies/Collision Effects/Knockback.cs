using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    private float xScaleDirection;

    public int damageDealt = 1;
    public GameObject hitboxCheck;
    public LayerMask whatIsPlayer;
    public float overlapCapsuleX = 0.8f;
    public float overlapCapsuleY = 0.25f;
    [Range(0, 80.0f)] public float knockBackStrength = 40.0f;

    private void Update()
    {
        xScaleDirection = transform.localScale.x;
    }

    public void Run()
    {
        if (DidPlayerTouchKnockBackHitbox())
        {
            GameObject player = GameObject.Find("Player");
            PlayerController pController = player.GetComponent<PlayerController>();
            if (!pController.isPlayerInvincible)
            {
                pController.DamagePlayer(damageDealt);
                KnockBackGameObject(player);
                pController.StartCoroutine(pController.ActivateInvincibiltyFrames());
            }
        }
    }

    private bool DidPlayerTouchKnockBackHitbox()
    {
        Vector2 overlapCapsule = new Vector2(
            overlapCapsuleX,
            overlapCapsuleY);

        return Physics2D.OverlapCapsule(
            hitboxCheck.transform.position,       // Point
            overlapCapsule,                     // Capsule Size
            CapsuleDirection2D.Horizontal,      // Direction of capsule
            0.0f,                               // Angle
            whatIsPlayer);                      // Layer mask
    }

    private void KnockBackGameObject(GameObject entity)
    {
        Rigidbody2D entityRb = entity.GetComponent<Rigidbody2D>();
        Vector2 knockBackDirection = new Vector2(GetDirection(entity), 0.5f);

        // Zeroing out the velocity is must as in combination with bounce script can
        // result in the player being knocked back and going sky high.
        entityRb.velocity = Vector2.zero;
        entityRb.AddForce(knockBackDirection * knockBackStrength, ForceMode2D.Impulse);
    }

    private float GetDirection(GameObject entity)
    {
        float entityFacingDirection = entity.transform.localScale.x;

        if (entityFacingDirection == transform.localScale.x)
            return entityFacingDirection * -1;
        else if (entityFacingDirection != transform.localScale.x)
            return transform.localScale.x;
        else
            return transform.localScale.x;
    }

}
