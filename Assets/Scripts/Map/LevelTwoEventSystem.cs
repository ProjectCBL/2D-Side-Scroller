using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTwoEventSystem : MonoBehaviour
{

    public GameObject blueBee;
    public GameObject yellowBee;
    public LayerMask whatIsAPlayer;

    private float timerTracker = 0.0f;
    [SerializeField] private float timeInterval = 1.0f;
    [SerializeField] private bool stepEncounterCheck = true;

    [SerializeField] private GameObject stepTrigger;
    [SerializeField] private GameObject platformTrigger;
    [SerializeField] private GameObject beginningTrigger;
    [SerializeField] private GameObject[] stepSpawns = new GameObject[] { };
    [SerializeField] private GameObject[] beginningSpawn = new GameObject[] { };
    [SerializeField] private GameObject[] platformSpawns = new GameObject[] { };

    [SerializeField] 
    private Vector2 beginningCollider, stepCollider, platformCollider;

    private Dictionary<string, bool> events = new Dictionary<string, bool>()
    {
        {"Beginning", false},
        {"Step", false},
        {"Platform", false}
    };

    // Start is called before the first frame update
    private void Start()
    {
        timerTracker = timeInterval;
    }

    private void FixedUpdate()
    {
        LookForCollisions();
        ExexcuteEvents();
    }

    private void Update()
    {
        timerTracker += Time.deltaTime;
    }

    private void LookForCollisions()
    {
        List<string> keys = new List<string>(events.Keys);
        foreach (string key in keys)
        {
            events[key] = GrabCollisionType(key);
        }
    }

    private void ExexcuteEvents()
    {
        foreach (KeyValuePair<string, bool> item in events)
        {
            if (item.Value) GrabEvents(item.Key);
        }
    }

    /*=========================== Collision Detectors ===========================*/

    private bool DidPlayerTriggerBeginningCollider()
    {
        return Physics2D.OverlapBox(
            beginningTrigger.transform.position,
            beginningCollider,
            0.0f,
            whatIsAPlayer);
    }

    private bool DidPlayerTriggerStepCollider()
    {
        return Physics2D.OverlapBox(
            stepTrigger.transform.position,
            stepCollider,
            0.0f,
            whatIsAPlayer);
    }

    private bool DidPlayerTriggerPlatformCollider()
    {
        return Physics2D.OverlapBox(
            platformTrigger.transform.position,
            platformCollider,
            0.0f,
            whatIsAPlayer);
    }

    /*=========================== Event Callers ===========================*/

    private void ExecuteBeginningEvent()
    {
        if(timerTracker >= timeInterval)
        {
            SpawnYellowBeesAt(beginningSpawn);
            timerTracker = 0.0f;
        }
    }

    private void ExecuteStepEvent()
    {
        if (stepEncounterCheck)
        {
            SpawnYellowBeesAt(stepSpawns, true);
            stepEncounterCheck = !stepEncounterCheck;
        }
    }
    private void ExecutePlatformEvent()
    {
        if (timerTracker >= timeInterval && !DidEnemyLimitGetReached())
        {
            int randomIndex = Random.Range(0, platformSpawns.Length-1);
            SpawnBlueBeesAt(platformSpawns[randomIndex]);
        }
    }

    /*=========================== Spawners ===========================*/

    private void SpawnYellowBeesAt(GameObject[] spawnLocation, bool flyRight=false)
    {

        YellowBeeBehaviour.Direction direction;

        if (flyRight)
            direction = YellowBeeBehaviour.Direction.RIGHT;
        else
            direction = YellowBeeBehaviour.Direction.LEFT;

        foreach (GameObject spawn in spawnLocation)
        {
            GameObject bee = Instantiate(yellowBee, spawn.transform);
            YellowBeeBehaviour beeScript = bee.GetComponent<YellowBeeBehaviour>();

            //Correct fly direction if needed
            StopCoroutine(beeScript.Behave());
            beeScript.flyDirection = direction;
            StartCoroutine(beeScript.Behave());
        }
    }

    private void SpawnBlueBeesAt(GameObject spawnLocation)
    {
        Instantiate(blueBee, spawnLocation.transform);
    }


    /*=========================== Gettters ===========================*/

    private void GrabEvents(string eventName)
    {
        switch (eventName)
        {
            case "Beginning":
                ExecuteBeginningEvent();
                break;
            case "Step":
                ExecuteStepEvent();
                break;
            case "Platform":
                ExecutePlatformEvent();
                break;
            default:
                break;
        }
    }

    private bool GrabCollisionType(string collisionName)
    {
        switch (collisionName)
        {
            case "Beginning":
                return DidPlayerTriggerBeginningCollider();
                break;
            case "Step":
                return DidPlayerTriggerStepCollider();
                break;
            case "Platform":
                return DidPlayerTriggerPlatformCollider();
                break;
            default:
                return false;
                break;
        }
    }

    /*=========================== Bool Function ===========================*/

    private bool DidEnemyLimitGetReached()
    {
        int count = 0;

        foreach(GameObject spawnPoint in platformSpawns)
        {
            count += spawnPoint.transform.childCount;
        }

        return (count >= 2) ? true : false;
    }

}
