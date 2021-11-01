using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class ThankYouScreenController : MonoBehaviour
{
    private void Awake()
    {
        Screen.SetResolution(800, 500, false);
    }

    public void OnEscape(InputAction.CallbackContext ctx)
    {
        Application.Quit();
    }

    public void OnRestart(InputAction.CallbackContext ctx)
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
