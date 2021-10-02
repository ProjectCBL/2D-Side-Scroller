using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Muzzle : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        TurnOffSpriteRender();
    }

    public IEnumerator PlayMuzzleAnimation()
    {

        // Called before to disable spriterenderer
        // before showing muzzle flare
        TurnOffSpriteRender();

        for(int i = 0; i < 3; i++) yield return null;

        TurnOnSpriteRender();

        for (int i = 0; i < 9; i++) yield return null;

        TurnOffSpriteRender();

    }

    private void TurnOffSpriteRender()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    private void TurnOnSpriteRender()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }

}