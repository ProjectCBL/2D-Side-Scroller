using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{

    public Animator anim;
    public GameObject bullet;
    public GameObject muzzle;
    public GameObject bulletSpawn;
    public GameObject bulletContainer;
    public PlayerMovement movementScript;
    public PlayerController playerController;
    [Range(0, 80.0f)] public float bulletSpeed = 40.0f;

    private Muzzle muzzleScript;
    [SerializeField] private bool shootPressed = false;

    // Called once the game object is enabled
    void Awake()
    {
        muzzleScript = muzzle.GetComponent<Muzzle>();
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
            anim.SetBool("IsShooting", false);
            //StopAllCoroutines();
            
            StartCoroutine(muzzleScript.PlayMuzzleAnimation());

            GameObject bullet = SpawnBullet();
            OrientBulletDirection(bullet);

            bullet.GetComponent<BulletController>().bulletSpeed = bulletSpeed;
            bullet.GetComponent<BulletController>().PropellBullet();

            anim.SetBool("IsShooting", true);
            StartCoroutine(ResetShootingAnimation());

            playerController.currentAmmoCount -= 1;
            shootPressed = !shootPressed;
        }
    }

    /* ==================== Animation ==================== */

    private IEnumerator ResetShootingAnimation()
    {
        for (int i = 0; i < 5; i++) yield return null;
        anim.SetBool("IsShooting", false);
    }

    /* ==================== Bullet Operations ==================== */

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
