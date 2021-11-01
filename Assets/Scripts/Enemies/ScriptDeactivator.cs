using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptDeactivator : MonoBehaviour
{
    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);       
    }

}
