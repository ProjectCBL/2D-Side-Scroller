using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticPosition : MonoBehaviour
{

    private GameObject player;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.Find("Player");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        player.transform.SetParent(this.gameObject.transform);
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        player.transform.SetParent(null);
    }

}
