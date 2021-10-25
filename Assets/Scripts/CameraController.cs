using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField]
    private GameObject player, anchorPoint1, anchorPoint2;

    // Update is called once per frame
    void Update()
    {
        AdjustXAxis();
    }

    private void AdjustXAxis()
    {
        float x = Mathf.Clamp(
            player.transform.position.x, 
            anchorPoint1.transform.position.x,
            anchorPoint2.transform.position.x);

        transform.position = new Vector3(x, transform.position.y, -10);
    }

}
