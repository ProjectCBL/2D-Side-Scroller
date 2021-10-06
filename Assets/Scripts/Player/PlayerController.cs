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

    [SerializeField] private int playerHealth = 3;
    [SerializeField] public PlayerMovement movementScript;
    [SerializeField] public PlayerShoot shootScript;
    [SerializeField] public RuntimeAnimatorController noGunAnimations;
    [SerializeField] public RuntimeAnimatorController gunAnimations;
    [SerializeField] public Sprite gunDefaultSprite;
    [SerializeField] public Sprite noGunDefaultSprite;
    [SerializeField] public SpriteRenderer spriteRenderer;
    [SerializeField] public PlayerStates currentPlayerState = PlayerStates.NOGUN;

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
        if (currentPlayerState == PlayerStates.WITHGUN) TurnOnShootScript();
        else TurnOffShootScript();
    }

    public void TurnOnShootScript()
    {
        if (!shootScript.enabled) shootScript.enabled = true;
    }

    public void TurnOffShootScript()
    {
        shootScript.enabled = false;
    }

    public void SwitchAnimationAndSprite()
    {
        currentPlayerState = (currentPlayerState == PlayerStates.NOGUN) ? PlayerStates.WITHGUN : PlayerStates.NOGUN;
        SwitchSprite(currentPlayerState);
        SwitchMovementAnimator(currentPlayerState);
    }

    private void SwitchSprite(PlayerStates state)
    {
        spriteRenderer.sprite = (state == PlayerStates.NOGUN) ? noGunDefaultSprite : gunDefaultSprite;
    }

    private void SwitchMovementAnimator(PlayerStates state)
    {
        movementScript.anim.runtimeAnimatorController = (state == PlayerStates.NOGUN) ? noGunAnimations : gunAnimations;
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
