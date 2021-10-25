using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticPosition : MonoBehaviour
{

    public bool isPlayerTouchingPlatform = false;

    private GameObject player;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerTouchingPlatform)
            KeepPlayerOnPlatform();
        else
            TakePlayerOffPlatform();
    }

    private void KeepPlayerOnPlatform()
    {
        player.transform.parent = this.transform;
    }

    private void TakePlayerOffPlatform()
    {
        player.transform.parent = null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isPlayerTouchingPlatform = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isPlayerTouchingPlatform = false;
    }

}
