using System;
using GameManagement;
using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace World
{
    public class Province : NetworkBehaviour
    {
        [Header("Gameplay")]
        [SerializeField,SyncVar] private Country country;

        public int developmentLevel = 1;
    
        [SerializeField] private string provinceName;

        public int provinceID
        {
            get
            {
                return _provinceID;
            }
        }

        [SerializeField,SyncVar] private int _provinceID;

        /// <summary>
        /// Method used to set up the province automatically so I don't have to do so by hand.
        /// </summary>
        public void Generate(int id)
        {
            _provinceID = id;
            banner.worldCamera = Camera.main;
            
            if (provinceName == null)
            {
                provinceName = "Province" + provinceID;
                gameObject.name = provinceName;
            }
            else
            {
                gameObject.name = provinceID + " " +provinceName;
            }
            
            UpdateBanner();
        }

        private void Update()
        {
            UpdateBanner();
            RotateBanner();
        }

        public void OnNextTurn()
        {
            int goldYield = developmentLevel * 5;
            int manPower = developmentLevel;
            country.manpowerCap += manPower;
            country.ModifyTreasury(goldYield);
        }

        [Command(requiresAuthority = false)]
        public virtual void TransferOwnership(Country newCountry)
        {
            country = newCountry;
        }

        [Command(requiresAuthority = false)]
        public void IncreaseLevel()
        {
            developmentLevel++;
        }
        //here starts the section dedicated to handling the UI for this province.
        [Header("Province UI")] 
        
        public Transform armyPosition;
        
        [SerializeField] private TMP_Text provinceNameText;

        [SerializeField] private Image bannerFlag;

        [SerializeField] private Canvas banner;

        public void UpdateBanner()
        {
            provinceNameText.text = provinceName;
            bannerFlag.sprite = country.flag;
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

        [SerializeField] protected ProvinceMenu provinceMenu;

        public void BannerClick()
        {
            if(GameManager.instance.player.netId != GameManager.instance.activeCountry.playerId) return;
            provinceMenu.Open();
        }
    }
}
