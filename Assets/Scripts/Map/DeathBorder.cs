using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBorder : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerController pc = collision.
                gameObject.
                GetComponent<PlayerController>();
            pc.playerHealth = 0;

        }
    }
}
