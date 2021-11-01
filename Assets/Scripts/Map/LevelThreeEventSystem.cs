using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelThreeEventSystem : MonoBehaviour
{
    private enum stage
    {
        STAGEONE=1,
        STAGETWO=2,
        STAGETHREE=3,
        STAGEFOUR=4,
        DONE=5

    }

    public Camera cam;
    public GameObject blueBee;
    public GameObject yellowBee;
    public GameObject caterpillar;
    public LayerMask whatIsAPlayer;

    [SerializeField] private stage currentStage = stage.STAGEONE;
    [SerializeField] private int currentEventNumber = 0;
    [SerializeField] private int eventLimit = 2;

    [SerializeField] private GameObject platform;
    [SerializeField] private GameObject arenaTrigger;
    [SerializeField] private Vector2 arenaTriggerCollider;
    [SerializeField] private bool playerTriggerEventAlready = false;

    [SerializeField] private GameObject[] blueArenaSpawns = new GameObject[] { };
    [SerializeField] private GameObject[] yellowArenaSpawns = new GameObject[] { };
    [SerializeField] private GameObject[] caterpillarArenaSpawns = new GameObject[] { };

    [SerializeField] private GameObject[] anchorPoints = new GameObject[] { };

    private void FixedUpdate()
    {
        if (DidPlayerTriggerArenaCollider() && !playerTriggerEventAlready)
        {
            playerTriggerEventAlready = true;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (playerTriggerEventAlready) ExecuteArenaEvent();
    }

    private void ExecuteArenaEvent()
    {
        if (!currentStage.Equals(stage.DONE))PlaceAnchorsPointsOnArena();
        GrabStageEvent();
    }

    private void ReleaseYellowBees()
    {
        if (AreThereNoEnemiesLeft(yellowArenaSpawns) && currentEventNumber < eventLimit)
        {
            for (int i = 0; i < yellowArenaSpawns.Length; i++)
            {
                SpawnYellowBeeAt(yellowArenaSpawns[i], (i > 1));
            }

            currentEventNumber += 1;
        }

        if (AreThereNoEnemiesLeft(yellowArenaSpawns) && currentEventNumber == eventLimit)
        {
            currentStage = stage.STAGETWO;
            currentEventNumber = 0;
        }
    }

    private void ReleaseCaterpillars()
    {
        if (AreThereNoEnemiesLeft(caterpillarArenaSpawns) && currentEventNumber < eventLimit)
        {
            SpawnCaterpillarAt(caterpillarArenaSpawns[0]);
            SpawnCaterpillarAt(caterpillarArenaSpawns[1]);
            currentEventNumber += 1;
        }

        if (AreThereNoEnemiesLeft(caterpillarArenaSpawns) && currentEventNumber == eventLimit)
        {
            currentStage = stage.STAGETHREE;
            currentEventNumber = 0;
        }
    }

    private void ReleaseBlueBees()
    {
        if (AreThereNoEnemiesLeft(blueArenaSpawns) && currentEventNumber < eventLimit)
        {
            SpawnBlueBeeAt(blueArenaSpawns[0]);
            SpawnBlueBeeAt(blueArenaSpawns[1]);
            currentEventNumber += 1;
        }

        if (AreThereNoEnemiesLeft(blueArenaSpawns) && currentEventNumber == eventLimit)
        {
            currentStage = stage.STAGEFOUR;
            currentEventNumber = 0;
        }
    }

    private void ActivateEndOfEvent()
    {
        ResetAnchorPoints();
        ReactivatePlatform();
        currentStage = stage.DONE;
    }

    private void ReactivatePlatform()
    {
        MovePlatform platformScript = platform.GetComponent<MovePlatform>();
        StartCoroutine(platformScript.MoveBackAndForth());
    }

    private void GrabStageEvent()
    {
        switch (currentStage)
        {
            case stage.STAGEONE:
                ReleaseYellowBees();
                break;
            case stage.STAGETWO:
                ReleaseCaterpillars();
                break;
            case stage.STAGETHREE:
                ReleaseBlueBees();
                break;
            case stage.STAGEFOUR:
                ActivateEndOfEvent();
                break;
            default:
                break;
        }
    }

    #region Camera Control

    private void PlaceAnchorsPointsOnArena()
    {
        CameraController camScript = cam.GetComponent<CameraController>();
        camScript.anchorPoint1 = anchorPoints[1];
        camScript.anchorPoint2 = anchorPoints[2];
    }

    private void ResetAnchorPoints()
    {
        Debug.Log("Something");
        CameraController camScript = cam.GetComponent<CameraController>();
        camScript.anchorPoint1 = anchorPoints[0];
        camScript.anchorPoint2 = anchorPoints[3];
    }

    #endregion Camera Control

    #region Collision Trigger
    private bool DidPlayerTriggerArenaCollider()
    {
        return Physics2D.OverlapBox(
            arenaTrigger.transform.position,
            arenaTriggerCollider,
            0.0f,
            whatIsAPlayer);
    }

    #endregion Collision Trigger

    #region Spawners
    private void SpawnCaterpillarAt(GameObject spawnLocation)
    {
        Instantiate(caterpillar, spawnLocation.transform);
    }

    private void SpawnBlueBeeAt(GameObject spawnLocation)
    {
        Instantiate(blueBee, spawnLocation.transform);
    }

    private void SpawnYellowBeeAt(GameObject spawnLocation, bool flyRight=false)
    {
        YellowBeeBehaviour.Direction direction;
        direction = (flyRight) ? YellowBeeBehaviour.Direction.LEFT : YellowBeeBehaviour.Direction.RIGHT;

        GameObject bee = Instantiate(yellowBee, spawnLocation.transform);
        YellowBeeBehaviour beeScript = bee.GetComponent<YellowBeeBehaviour>();

        bee.AddComponent<ScriptDeactivator>();

        StopCoroutine(beeScript.Behave());
        beeScript.flyDirection = direction;
        StartCoroutine(beeScript.Behave());
    }

    #endregion Spawners

    private bool AreThereNoEnemiesLeft(GameObject[] spawns)
    {
        int count = 0;

        foreach (GameObject loc in spawns)
        {
            count += loc.transform.childCount;
        }

        return (count > 0) ? false : true;
    }

}
