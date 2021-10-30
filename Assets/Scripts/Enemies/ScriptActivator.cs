using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptActivator : MonoBehaviour
{

    private void OnBecameVisible()
    {
        Enemy script = GetBehaviourScript();

        Debug.Log("True");

        if (script.behaviourSwitch.Equals(Enemy.BehaviourToggle.OFF))
        {
            StartCoroutine(script.Behave());
            script.behaviourSwitch = Enemy.BehaviourToggle.ON;
        } 
    }

    private Enemy GetBehaviourScript()
    {
        if(this.GetComponent<BlueBeeBehaviour>() != null)
        {
            return this.GetComponent<BlueBeeBehaviour>();
        }
        else if(this.GetComponent<CaterpillarBehaviour>() != null)
        {
            return this.GetComponent<CaterpillarBehaviour>();
        }
        else if(this.GetComponent<YellowBeeBehaviour>() != null)
        {
            return this.GetComponent<YellowBeeBehaviour>();
        }
        else
        {
            return null;
        }
    }

}
