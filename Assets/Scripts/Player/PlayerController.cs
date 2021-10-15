using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum PlayerStates
    {
        NOGUN,
        WITHGUN
    }

    public int playerHealth = 3;
    public bool isPlayerInvincible = false;
    [Range(0, 50)] public int defaultAmmoCount = 20;
    [Range(0, 50)] public int currentAmmoCount = 20;

    [SerializeField] private PlayerMovement movementScript;
    [SerializeField] private PlayerShoot shootScript;
    [SerializeField] private RuntimeAnimatorController noGunAnimations;
    [SerializeField] private RuntimeAnimatorController gunAnimations;
    [SerializeField] private Sprite gunDefaultSprite;
    [SerializeField] private Sprite noGunDefaultSprite;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private PlayerStates currentPlayerState = PlayerStates.NOGUN;

    // Start is called before the first frame update
    void Awake()
    {
        SwitchSprite(currentPlayerState);
        SwitchMovementAnimator(currentPlayerState);
    }

    // Called once per frame
    void Update()
    {
        CheckHealth();
        CheckAmmoCount();
        if (currentPlayerState == PlayerStates.WITHGUN) TurnOnShootScript();
        else TurnOffShootScript();
    }

    /* =========================== Shoot Script =========================== */

    public void TurnOnShootScript()
    {
        if (!shootScript.enabled) shootScript.enabled = true;
    }

    public void TurnOffShootScript()
    {
        shootScript.enabled = false;
    }

    private void CheckAmmoCount()
    {
        if (currentAmmoCount <= 0) SwitchAnimationAndSprite();
    }

    /* =========================== Sprite and Animation =========================== */

    public void SwitchAnimationAndSprite()
    {
        currentPlayerState = (currentPlayerState == PlayerStates.NOGUN) ? PlayerStates.WITHGUN : PlayerStates.NOGUN;
        SwitchSprite(currentPlayerState);
        SwitchMovementAnimator(currentPlayerState);
        currentAmmoCount = defaultAmmoCount;
    }

    private void SwitchSprite(PlayerStates state)
    {
        spriteRenderer.sprite = (state == PlayerStates.NOGUN) ? noGunDefaultSprite : gunDefaultSprite;
    }

    private void SwitchMovementAnimator(PlayerStates state)
    {
        movementScript.anim.runtimeAnimatorController = (state == PlayerStates.NOGUN) ? noGunAnimations : gunAnimations;
    }

    /* =========================== Player Health =========================== */

    public IEnumerator ActivateInvincibiltyFrames()
    {
        isPlayerInvincible = true;
        yield return new WaitForSeconds(1.0f);
        isPlayerInvincible = false;
    }

    private void CheckHealth()
    {
        if (playerHealth <= 0) KillPlayer();
    }

    public void DamagePlayer(int damage)
    {
        playerHealth -= damage;
    }

    private void KillPlayer()
    {
        Destroy(this.gameObject);
    }

}
