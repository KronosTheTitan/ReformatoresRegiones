using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using World;
using GameManagement;

public class Army : NetworkBehaviour
{
    [Header("Gameplay")]
    
    public Country owner;
    public Province location;

    public int infantry;
    public int cavalry;
    public int artillery;

    private void Update()
    {
        transform.position = location.armyPosition.position;
        UpdateBanner();
        RotateBanner();
    }

    [Header("UI")] [SerializeField] private ArmyMenu armyMenu;

    [SerializeField] private Canvas banner;

    [SerializeField] private TMP_Text armySizeText;

    [SerializeField] private Image bannerFlag;

    public void UpdateBanner()
    {
        armySizeText.text = (infantry+cavalry+artillery).ToString()+"k";
        bannerFlag.sprite = owner.flag;
    }

    void RotateBanner()
    {
        if (Vector3.Distance(banner.transform.position, Camera.main.transform.position) > 750 || Math.Abs(CameraController.instance.transform.position.y - 120f) < 5  )
        {
            banner.gameObject.SetActive(false);
        }
        else
        {
            banner.gameObject.SetActive(true); banner.gameObject.SetActive(true);
            float x = Camera.main.transform.rotation.x-banner.transform.rotation.x;
            banner.transform.Rotate(x,0,0);
        }
    }
    
    public void BannerClick()
    {
        if(GameManager.instance.player.netId != GameManager.instance.activeCountry.playerId) return;
        if(GameManager.instance.activeCountry != owner) return;
        armyMenu.Open();
    }
}
