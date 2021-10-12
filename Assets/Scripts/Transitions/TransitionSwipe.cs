using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionSwipe : MonoBehaviour
{

    public string swipeDirection;
    public float swipeSpeed = 20f;
    public float middleOfScreen = 17.0f;

    private Dictionary<string, bool> positionCheck = new Dictionary<string, bool>();
    private Dictionary<string, Vector2> centerPositions = new Dictionary<string, Vector2>();

    private void Awake()
    {
        SetChecks();
        SetCenterOfScreen();
    }

    /*Uncomment to test script
     * private void Start()
    {
        StartCoroutine(Swipe());
    }*/

    public IEnumerator Swipe()
    {
        while (true){
            if (positionCheck[swipeDirection]) break;
            MoveTowardsCenterOfScreen();
            yield return null;
        }
    }

    private void MoveTowardsCenterOfScreen()
    {
        transform.position = Vector2.MoveTowards(
            transform.position,
            centerPositions[swipeDirection],
            swipeSpeed * Time.deltaTime);
    }

    private void SetChecks()
    {
        positionCheck.Add("Right", (transform.position.x >= middleOfScreen));
        positionCheck.Add("Left", (transform.position.x <= middleOfScreen));
        positionCheck.Add("Down", (transform.position.y <= middleOfScreen));
        positionCheck.Add("Up", (transform.position.y >= middleOfScreen));
    }

    private void SetCenterOfScreen()
    {
        centerPositions.Add("Right", new Vector2(middleOfScreen, transform.position.y));
        centerPositions.Add("Left", new Vector2(middleOfScreen, transform.position.y));
        centerPositions.Add("Down", new Vector2(transform.position.x, middleOfScreen));
        centerPositions.Add("Up", new Vector2(transform.position.x, middleOfScreen));
    }

}
