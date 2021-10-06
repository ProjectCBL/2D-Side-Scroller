using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaterpillarBehaviour : MonoBehaviour
{

    public Animator anim;
    public Rigidbody2D rb;
    public GameObject player;
    public GameObject headCheck;
    public LayerMask whatIsAPlayer;
    public bool isTravelingRight = true;
    public float currentAgroTimer = 0.0f;
    [Range(0, 5.0f)] public float agroTimer = 1.5f; 
    [Range(0, 5.0f)] public float aggroRange = 2.5f;
    [Range(0, 10.0f)] public float pathingRange = 5.0f;
    [Range(0, 5.0f)] public float crawlSpeed = 0.5f;
    [Range(0, 5.0f)] public float agroCrawlSpeed = 0.5f;

    [SerializeField] private float targetPoint;
    [SerializeField] private float pathingPoint1;
    [SerializeField] private float pathingPoint2;
    [SerializeField] private bool isAggroed = false;

    private void Awake()
    {
        SetWalkingPoints();
        player = GameObject.Find("Player");
        targetPoint = (Random.Range(0, 9) <= 4) ? pathingPoint1 : pathingPoint2;
    }

    private void Update()
    {
        HandleAggroEvent();
        Crawl((isAggroed) ? agroCrawlSpeed : crawlSpeed);
        if (!isAggroed) SwapPathingPresetPoints();
        FlipSpriteTowardsTargetPoint();
    }

    /* ======================== Path Controls ======================== */

    private void Crawl(float speed)
    {

        Vector2 newTargetPoint = new Vector2(targetPoint, transform.position.y);

        transform.position = Vector2.MoveTowards(
            transform.position,
            newTargetPoint,
            speed * Time.deltaTime);

        anim.SetBool("Move", true);

    }

    private void HandleAggroEvent()
    {
        TestForAggro();
        ActivateAggro();
    }

    private void TestForAggro()
    {

        currentAgroTimer += (isAggroed) ? Time.deltaTime : 0.0f;

        bool playerIsInAggroRange = Physics2D.OverlapCircle(
            transform.position,
            aggroRange,
            whatIsAPlayer);

        if (currentAgroTimer >= agroTimer && !playerIsInAggroRange)
        {
            isAggroed = false;
        }
        else if (playerIsInAggroRange)
        {
            currentAgroTimer = 0.0f;
            isAggroed = true;
        }
    }

    private void ActivateAggro()
    {
        if (isAggroed) MarkPlayerAsPathingPoint();
    }

    /* ======================== Sprite / Animation ======================== */

    private void FlipSpriteTowardsTargetPoint()
    {
        Vector3 newScale = transform.localScale;

        // Flip sprite through the x axis
        newScale.x = (targetPoint >= transform.position.x) ? 1 : -1;
        transform.localScale = newScale;
        isTravelingRight = (transform.localScale.x > 0) ? true : false;

        // This line is extremely important.  Without resetting the velocity,
        // the entity will slow to a crawl or get stuck after flipping.
        rb.velocity = Vector2.zero;
    }

    private IEnumerator WaitForCrawlAnimationToEnd()
    {
        yield return null;
    }

    private IEnumerator WaitForFastCrawlAnimationToEnd()
    {
        yield return null;
    }

    /* ======================== Position Pathing Modification ======================== */

    private void SwapPathingPresetPoints()
    {
        if (transform.position.x >= pathingPoint2) targetPoint = pathingPoint1;
        else if (transform.position.x <= pathingPoint1) targetPoint = pathingPoint2;
    }

    private void MarkPlayerAsPathingPoint()
    {
        targetPoint = player.transform.position.x;
    }

    /* ======================== Set Up Functions ======================== */

    private void SetWalkingPoints()
    {
        //Enemy will walk between these points
        pathingPoint1 = transform.position.x - pathingRange / 2;
        pathingPoint2 = transform.position.x + pathingRange / 2;
    }

}
