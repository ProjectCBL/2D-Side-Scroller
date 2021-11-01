using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionSwipe : MonoBehaviour
{
    public Transform endPoint;
    public GameObject transition;
    public float swipeSpeed = 20f;
    public bool animationIsDone = false;
    public bool animationTriggered = false;

    private Vector2 originalPos;
    private Vector2 endPointPos;

    private void Awake()
    {
        originalPos = transition.transform.position;
        endPointPos = endPoint.position;
    }

    /*Uncomment to test script*/
    /*private void Start()
    {
        StartCoroutine(Swipe());
    }*/

    public IEnumerator Swipe()
    {
        animationTriggered = true;

        while (true){
            if (DidCurtainReachPoint()) break;
            MoveTowardsPoint();
            yield return null;
        }

        animationTriggered = false;
        animationIsDone = true;
    }

    private void MoveTowardsPoint()
    {
        transition.transform.position = Vector2.MoveTowards(
            transition.transform.position,
            endPoint.position,
            swipeSpeed * Time.deltaTime);
    }

    private bool DidCurtainReachPoint()
    {
        return (transition.transform.position.x == endPoint.position.x 
            && transition.transform.position.y == endPoint.position.y);
    }
}
