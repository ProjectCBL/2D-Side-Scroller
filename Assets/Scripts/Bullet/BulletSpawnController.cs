using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawnController : MonoBehaviour
{

    public float defaultSpawnY = -0.16f;
    public float elevatedSpawnY = -0.06f;

    private Animator anim;

    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponentInParent<Animator>(); 
    }

    // Update is called once per frame
    void Update()
    {
        MoveBulletSpawnLocation();
    }

    private void MoveBulletSpawnLocation()
    {
        if (anim.GetBool("Running")) SetNewYLocationTo(elevatedSpawnY);
        else SetNewYLocationTo(defaultSpawnY);
    }

    private void SetNewYLocationTo(float yCoordinate)
    {
        gameObject.transform.localPosition = new Vector3(
            gameObject.transform.localPosition.x,
            yCoordinate,
            gameObject.transform.localPosition.z
        );
    }

}
