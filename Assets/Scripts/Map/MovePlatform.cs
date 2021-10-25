using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{

    private enum BehaviourType
    {
        MOVEBACKANDFORTH,
        MOVETOONEANDPAUSE,
        MoveTOTWOANDPAUSE,
        PAUSE
    } 

    [Range(0, 2.5f)] public float platformSpeed = 0.25f;

    private Vector2 targetPoint;
    [SerializeField] private Transform point1;
    [SerializeField] private Transform point2;
    [SerializeField] private float pointDifference = 0.5f;
    [SerializeField] private BehaviourType behaviourSwitch = BehaviourType.MOVEBACKANDFORTH;
 
    // Start is called before the first frame update
    void Start()
    {
        PickBehaviour();
    }

    private IEnumerator MoveBackAndForth()
    {

        targetPoint = point1.localPosition;

        while (true)
        {  
            SwitchDirection();
            MoveToward();
            yield return null;
        }   
    }

    private IEnumerator MoveToOneAndPause()
    {
        targetPoint = point1.localPosition;

        while (true)
        {
            MoveToward();

            if (CheckIfPlatformIsAt(point1))
                break;

            yield return null;
        }
    }

    private IEnumerator MoveToTwoAndPause()
    {
        targetPoint = point2.localPosition;

        while (true)
        {
            MoveToward();

            if (CheckIfPlatformIsAt(point2))
                break;

            yield return null;
        }
    }

    private void MoveToward()
    {
        transform.localPosition = Vector2.MoveTowards(
            transform.localPosition,
            targetPoint,
            platformSpeed * Time.deltaTime);
    }

    private void SwitchDirection()
    {
        if (CheckIfPlatformIsAt(point1))
            targetPoint = point2.transform.localPosition;
        else if (CheckIfPlatformIsAt(point2))
            targetPoint = point1.transform.localPosition;
    }

    private bool CheckIfPlatformIsAt(Transform point)
    {
        return (transform.localPosition == point.localPosition);
    }

    private void ClampPosition()
    {
        float x = Mathf.Clamp(
            transform.localPosition.x, 
            point1.localPosition.x, 
            point2.localPosition.x);
        float y = Mathf.Clamp(
            transform.localPosition.y, 
            point1.localPosition.y, 
            point2.localPosition.y);
        transform.localPosition = new Vector2(x, y);
    }

    private void PickBehaviour()
    {
        switch (behaviourSwitch)
        {
            case BehaviourType.MOVETOONEANDPAUSE:
                StartCoroutine(MoveToOneAndPause());
                break;
            case BehaviourType.MoveTOTWOANDPAUSE:
                StartCoroutine(MoveToTwoAndPause());
                break;
            case BehaviourType.MOVEBACKANDFORTH:
                StartCoroutine(MoveBackAndForth());
                break;
            case BehaviourType.PAUSE:
                break;
            default:
                break;
        }
    }
}
