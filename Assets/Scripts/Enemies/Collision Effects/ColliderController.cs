using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderController : MonoBehaviour
{

    [SerializeField] Knockback knockback;
    [SerializeField] BounceOff bounceOff;

    private void FixedUpdate()
    {
        bounceOff.Run();
        if (!bounceOff.didPlayerTouchHeadCheck)
            knockback.Run();
    }

}
