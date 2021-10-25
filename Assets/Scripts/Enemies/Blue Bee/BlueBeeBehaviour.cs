using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBeeBehaviour : MonoBehaviour
{

    private enum BehaviourToggle
    {
        ON,
        OFF
    }

    public LayerMask whatIsPlayer;

    private GameObject player;
    private bool isAggroed = false;
    [SerializeField] BehaviourToggle behaviourSwitch;
    [SerializeField] [Range(0, 50.0f)] float flySpeed = 25.0f;
    [SerializeField] [Range(0, 50.0f)] float aggroRadius = 25.0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        if (behaviourSwitch.Equals(BehaviourToggle.ON)) StartCoroutine(Behave());
    }

    public IEnumerator Behave()
    {
        while (true)
        {
            if (!isAggroed) CheckIfPlayerInAggroRange();
            if (isAggroed) MoveTowardPlayer();
            yield return null;
        }
    }

    private void CheckIfPlayerInAggroRange()
    {
        bool isPlayerInRange = Physics2D.OverlapCircle(
            transform.position,
            aggroRadius,
            whatIsPlayer);

        if (isPlayerInRange) isAggroed = true;
    }

    private void MoveTowardPlayer()
    {
        transform.position = Vector2.MoveTowards(
            transform.position,
            player.transform.position,
            flySpeed * Time.deltaTime);
    }

}
