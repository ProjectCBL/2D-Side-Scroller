using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RenderOptimizer : MonoBehaviour
{

    /*Purpose of this script is for optimization purposes:  As of writing this
     I haven't tested the strength of game when a level is fully loaded and 
    rendered. However, if this script avoids a headache down the road then I 
    achieved my goal.  That being said, this script is primarily to limit what is 
    being rendered on the screen.  This way I can control the opitmization and 
    load that is being placed on the users' system.  So whatever isn't be picked
    up from the camera will have their renderer component disabled.*/

    public dynamic objectRenderer;

    [SerializeField] private bool isFirstPass = false;
    [SerializeField] private bool willKillAfterFirstPass = false;

    // Start is called before the first frame update
    void Start()
    {
        GetRenderer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnBecameVisible()
    {
        Debug.Log("Hi I'm Here...");
    }

    private void OnBecameInvisible()
    {
        
    }

    private void GetRenderer()
    {
        switch (gameObject.tag)
        {
            case "Enemy":
            case "Player":
            case "Platform":
            case "Pickup":
                objectRenderer = gameObject.GetComponent<SpriteRenderer>();
                break;
            case "Ground":
            case "Decorations":
            case "Background":
                objectRenderer = gameObject.GetComponent<TilemapRenderer>();
                break;
            default:
                objectRenderer = gameObject.GetComponent<SpriteRenderer>();
                break;
        }
    }

}
