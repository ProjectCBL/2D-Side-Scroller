using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum BehaviourToggle
    {
        ON,
        OFF
    }

    public BehaviourToggle behaviourSwitch;
    public virtual IEnumerator Behave() { yield return null; }

}
