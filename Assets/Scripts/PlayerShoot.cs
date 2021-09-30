using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{

    public GameObject bullet;
    public GameObject bulletSpawn;
    public GameObject bulletContainer;

    [SerializeField] private bool shootPressed = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private GameObject SpawnBullet(){
        return Instantiate(
            bullet, 
            bulletSpawn.transform.position, 
            Quaternion.identity,
            bulletContainer.transform
        );
    }

    public void OnShoot(InputAction.CallbackContext ctx){
        shootPressed = ctx.performed ? true : false;
    }

}
