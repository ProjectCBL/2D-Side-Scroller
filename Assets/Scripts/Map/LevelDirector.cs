using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelDirector : MonoBehaviour
{

    public GameObject player;
    public GameObject startTransition;
    public GameObject endTransition;

    [SerializeField] private int nextScene = 1;

    private void Awake()
    {
        DisablePlayerScripts();
        StartTransitionAnimation(startTransition);
    }

    private void Update()
    {
        TransitionSwipe startTransScript = startTransition.GetComponent<TransitionSwipe>();
        TransitionSwipe endTransScript = endTransition.GetComponent<TransitionSwipe>();

        if (startTransScript.animationIsDone && !endTransScript.animationTriggered)
        {
            EnablePlayerScripts();
        }

        if (endTransScript.animationIsDone)
        {
            LoadNextScene();
        }

    }

    private void DisablePlayerScripts()
    {
        PlayerController controller = player.GetComponent<PlayerController>();
        PlayerShoot shoot = player.GetComponent<PlayerShoot>();
        PlayerMovement movement = player.GetComponent<PlayerMovement>();
        controller.enabled = false;
        shoot.enabled = false;
        movement.enabled = false;
    }

    private void EnablePlayerScripts()
    {
        PlayerController controller = player.GetComponent<PlayerController>();
        PlayerShoot shoot = player.GetComponent<PlayerShoot>();
        PlayerMovement movement = player.GetComponent<PlayerMovement>();
        controller.enabled = true;
        shoot.enabled = true;
        movement.enabled = true;
    }

    private void StartTransitionAnimation(GameObject transition)
    {
        TransitionSwipe transScript = transition.GetComponent<TransitionSwipe>();
        StartCoroutine(transScript.Swipe());
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(nextScene, LoadSceneMode.Single);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            DisablePlayerScripts();
            StartTransitionAnimation(endTransition);
            //LoadNextScene();
        }
    }

}
