using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{

    [Range(0, 0.5f)] public float platformSpeed = 0.25f;

    private Vector2 targetPoint;
    [SerializeField] private Vector2 upperPoint;
    [SerializeField] private Vector2 lowerPoint;
    [SerializeField] private float pointDifference = 0.5f;
 
    // Start is called before the first frame update
    void Start()
    {
        SetTargetPoints();
        targetPoint = upperPoint;
    }

    // Update is called once per frame
    void Update()
    {
        SwithDirection();
        MoveToward();
    }

    private void MoveToward()
    {
        transform.position = Vector2.MoveTowards(
            transform.position,
            targetPoint,
            platformSpeed * Time.deltaTime);
    }

    private void SwithDirection()
    {
        if (transform.position.y >= upperPoint.y)
            targetPoint = lowerPoint;
        else if (transform.position.y <= lowerPoint.y)
            targetPoint = upperPoint;
    }

    private void SetTargetPoints()
    {
        float y = transform.position.y;
        upperPoint = new Vector2(transform.position.x, y + pointDifference);
        lowerPoint = new Vector2(transform.position.x, y - pointDifference);
    }
}
