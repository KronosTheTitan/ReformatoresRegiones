using System.Collections;
using System.Collections.Generic;
using GameManagement;
using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using World;

public class UIManager : NetworkBehaviour
{
    public static UIManager instance;

    public Menu openMenu;
    // Start is called before the first frame update
    private void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        //UpdateTurnUI();
    }

    [ClientRpc]
    public void UpdateUI(Country activeCountry, int activeIndex)
    {
        uint id = GameManager.instance.player.netId;
        if (activeCountry.playerId == id)
        {
            turnCanvas.gameObject.SetActive(true);
            waitCanvas.gameObject.SetActive(false);
            UpdateTurnUI(activeCountry,activeIndex);
            UpdateWaitUI(activeCountry,activeIndex);
        }
        else
        {
            turnCanvas.gameObject.SetActive(true);
            waitCanvas.gameObject.SetActive(true);
            UpdateTurnUI(activeCountry,activeIndex);
            UpdateWaitUI(activeCountry,activeIndex);
        }
    }

    [Header("Turn UI")] 
    [SerializeField] private Canvas turnCanvas;
    [SerializeField] private Image cornerFlag;
    [SerializeField] private TMP_Text countryNameText;
    [SerializeField] private TMP_Text treasuryText;
    [SerializeField] private TMP_Text manpowerText;
    [SerializeField] private Transform eventTargetTransform;

    void UpdateTurnUI(Country activeCountry, int activeIndex)
    {
        cornerFlag.sprite = activeCountry.flag;
        countryNameText.text = activeCountry.countryName;
        
        treasuryText.text = " "+activeCountry.treasury;
        manpowerText.text = " " + activeCountry.manpowerCurrent+ " ("+activeCountry.manpowerCap+")";
        UpdateEventBar(activeCountry);
    }
    
    void UpdateEventBar(Country activeCountry)
    {
        for (int i = 0; i < activeCountry.eventQueue.Count; i++)
        {
            activeCountry.eventQueue[i].gameObject.SetActive(true);
                
            //calculate the position for the selected event in the top bar.
            float x = eventTargetTransform.position.x + (i * 72);
            float y = eventTargetTransform.position.y;
            
            Vector2 pos = activeCountry.eventQueue[i].transform.position;
            pos.x = x;
            pos.y = y;
            
            activeCountry.eventQueue[i].eventBarItem.transform.position = pos;
            activeCountry.eventQueue[i].transform.SetParent(eventTargetTransform);
        }
    }

    [Header("Wait UI")]
    [SerializeField] Canvas waitCanvas;

    [SerializeField] private Image leftFlag;
    [SerializeField] private Image centerFlag;
    [SerializeField] private Image rightFlag;

    void UpdateWaitUI(Country activeCountry, int activeIndex)
    {
        int left = activeIndex - 1 < 0
            ? GameManager.instance.countries.Length - 1
            : activeIndex - 1;
        int right = activeIndex + 1 >= GameManager.instance.countries.Length
            ? 0
            : activeIndex + 1;
        int center = activeIndex;
        leftFlag.sprite = GameManager.instance.countries[left].flag;
        rightFlag.sprite = GameManager.instance.countries[right].flag;
        centerFlag.sprite = GameManager.instance.countries[center].flag;
    }
}
