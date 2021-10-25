using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowBeeBehaviour : MonoBehaviour
{

    private enum Direction
    {
        RIGHT,
        LEFT
    }

    private enum BehaviourToggle
    {
        ON,
        OFF
    }

    [SerializeField] private Vector2 target;
    [SerializeField] private Direction flyDirection;
    [SerializeField] private BehaviourToggle switchFlyMode;
    [SerializeField] private SpriteRenderer yellowBeeRenderer;
    [SerializeField] [Range(0, 50.0f)] private float flySpeed = 25.0f;

    // Start is called before the first frame update
    void Awake()
    {
        if (switchFlyMode.Equals(BehaviourToggle.ON)) StartCoroutine(Behave());
    }

    public IEnumerator Behave()
    {

        FlipSpriteBasedOnFlyDirection();
        target = SetTarget();

        while (true)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                target,
                flySpeed * Time.deltaTime);
            yield return null;
        }
    }

    private void FlipSpriteBasedOnFlyDirection()
    {
        if (flyDirection.Equals(Direction.RIGHT))
            FlipSpriteRight();
        else
            FlipSpriteLeft();
    }

    private void FlipSpriteRight()
    {
        Vector3 newScale = transform.localScale;
        newScale.x = 1;
        transform.localScale = newScale;
    }

    private void FlipSpriteLeft()
    {
        Vector3 newScale = transform.localScale;
        newScale.x = -1;
        transform.localScale = newScale;
    }

    private Vector2 SetTarget()
    {
        int targetDirection = (flyDirection.Equals(Direction.RIGHT)) ? 1 : -1;
        float x = Camera.main.pixelWidth * targetDirection;
        return new Vector2(x, transform.position.y);
    }

}
