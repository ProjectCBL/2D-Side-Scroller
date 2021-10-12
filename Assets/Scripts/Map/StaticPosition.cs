using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticPosition : MonoBehaviour
{

    public MovePlatform movingScript;
    public bool isPlayerTouchingPlatform = false;

    private GameObject player;
    [SerializeField] private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerTouchingPlatform) KeepPlayerOnPlatform();
    }

    private void KeepPlayerOnPlatform()
    {
        player.transform.position = Vector2.MoveTowards(
            player.transform.position,
            GetTopOfSprite(),
            movingScript.platformSpeed * Time.deltaTime);
    }

    private Vector2 GetTopOfSprite()
    {
        Vector2 top = new Vector2(
            transform.position.x,
            spriteRenderer.size.y / 2);
        return top;
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
