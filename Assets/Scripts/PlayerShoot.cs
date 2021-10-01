using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{

    public GameObject bullet;
    public GameObject muzzleFlare;
    public GameObject bulletSpawn;
    public GameObject bulletContainer;
    public PlayerMovement movementScript;

    [SerializeField] private bool shootPressed = false;

    // Called once the game object is enabled
    void Awake()
    {
        bulletContainer = GameObject.Find("Bullet Container");
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
    }

    /* ==================== Shooting Bullets ==================== */

    private void Shoot()
    {
        if (shootPressed)
        {
            OrientBulletDirection(SpawnBullet());
            shootPressed = !shootPressed;
        }
    }

    private void OrientBulletDirection(GameObject bullet)
    {
        Vector3 newLocalScale = bullet.transform.localScale;
        newLocalScale.x *= (movementScript.playerFacingRight) ? 1 : -1;
        bullet.transform.localScale = newLocalScale;
    }

    private GameObject SpawnBullet(){
        return Instantiate(
            bullet, 
            bulletSpawn.transform.position, 
            Quaternion.identity,
            bulletContainer.transform
        );
    }

    /* ==================== Shoot Button Input ==================== */

    public void OnShoot(InputAction.CallbackContext ctx){
        shootPressed = ctx.performed ? true : false;
    }

}
