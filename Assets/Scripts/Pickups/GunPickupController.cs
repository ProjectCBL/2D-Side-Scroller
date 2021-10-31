using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickupController : MonoBehaviour
{

    private Vector2 upperPoint;
    private Vector2 lowerPoint;
    private bool isPlayerTouching = false;

    // Start is called before the first frame update
    void Start()
    {
        SetPoints();
        StartCoroutine(BobUpAndDown());
    }

    /* ====================== Animation ====================== */

    private IEnumerator BobUpAndDown()
    {
        float step = 0.15f;
        Vector2 targetPoint = upperPoint;

        while (true)
        {
            targetPoint = SwitchPoint(targetPoint);
            MoveTowards(targetPoint, step);
            yield return null;
        }
    }

    private void MoveTowards(Vector2 targetPoint, float step)
    {
        transform.position = Vector2.MoveTowards(
            transform.position,
            targetPoint,
            step * Time.deltaTime);
    }

    private Vector2 SwitchPoint(Vector2 currentPoint)
    {
        if (transform.position.y >= upperPoint.y)
            return lowerPoint;
        else if (transform.position.y <= lowerPoint.y)
            return upperPoint;
        else
            return currentPoint;
    }

    private void SetPoints()
    {
        float heightDifference = 0.05f;
        Vector2 currentPos = transform.position;
        upperPoint = new Vector2(currentPos.x, currentPos.y + heightDifference);
        lowerPoint = new Vector2(currentPos.x, currentPos.y - heightDifference);
    }

    /* ====================== Collision ====================== */

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.tag == "Player" && isPlayerTouching == false)
        {
            GameObject player = collision.gameObject;
            player.GetComponent<PlayerController>().UpdateAnimationAndSprite();
            Destroy(this.gameObject);
            isPlayerTouching = true;
        }

    }

}
