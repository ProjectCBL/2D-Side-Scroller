using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    private float xScaleDirection;

    public int damageDealt = 1;
    [Range(0, 80.0f)] public float knockBackStrength = 40.0f;

    private void Update()
    {
        xScaleDirection = transform.localScale.x;    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.tag == "Player")
        {
            GameObject player = collision.gameObject;
            player.GetComponent<PlayerController>().DamagePlayer(damageDealt);
            KnockBackGameObject(player, xScaleDirection);
        }

    }

    private void KnockBackGameObject(GameObject entity, float direction)
    {
        Rigidbody2D entityRb = entity.GetComponent<Rigidbody2D>();
        Vector2 knockBackDirection = new Vector2(1 * direction, 0.5f);

        // Zeroing out the velocity is must as in combination with bounce script can
        // result in the player being knocked back and going sky high.
        entityRb.velocity = Vector2.zero;
        entityRb.AddForce(knockBackDirection * knockBackStrength, ForceMode2D.Impulse);
    }

}
