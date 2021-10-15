using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InGameUIController: MonoBehaviour
{

    public GameObject healthContainer;
    public GameObject ammoContainer;

    private int health = 0;
    private int ammoCount = 0;
    private GameObject player;
    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlayerInfo();

        if (healthContainer.transform.childCount != health) MaintainHealthMeter();
        if (ammoContainer.activeSelf) MaintainAmmoCountMeter();

        ShowAmmoCount();
    }

    private void ShowAmmoCount()
    {
        if (player.GetComponent<PlayerShoot>().enabled) 
            ammoContainer.SetActive(true);
        else 
            ammoContainer.SetActive(false);
    }

    private void MaintainHealthMeter()
    {
        for (int i = 0; i < 3; i++)
        {
            healthContainer
                .transform
                .GetChild(i)
                .gameObject
                .SetActive((i + 1 <= health));
        }
    }

    private void MaintainAmmoCountMeter()
    {
        TMP_Text text = ammoContainer.transform.GetChild(1).GetComponent<TMP_Text>();
        text.text = ammoCount.ToString();
    }

    private void UpdatePlayerInfo()
    {
        health = playerController.playerHealth;
        ammoCount = playerController.currentAmmoCount;
    }

}
