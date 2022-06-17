using System;
using System.Collections;
using System.Collections.Generic;
using GameManagement;
using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using World;

public class CountrySelector : NetworkBehaviour
{
    private Country _selectedCountry;
    private int _countryID;
    
    [SerializeField] private Canvas selectionScreen;

    [SerializeField] private Image[] selectionFrames;

    [SerializeField] private Image selectedFlag;
    [SerializeField] private TMP_Text selectedName; 
    [SerializeField] private TMP_Text selectedDescription; 

    //TextAreaAttribute(int minLines, int maxLines);
    
    [SerializeField,@TextAreaAttribute(15,20)] private string[] countryInfo;
    
    public void PickCountry(int i)
    {
        if(GameManager.instance.countries[i].isPlayer) return;

        Country previous = null;
        
        if (_selectedCountry != null)
        {
            selectionFrames[_countryID].gameObject.SetActive(false);
            previous = _selectedCountry;
        }

        _selectedCountry = GameManager.instance.countries[i];
        _countryID = i;
        
        selectionFrames[i].gameObject.SetActive(true);

        selectedFlag.sprite = _selectedCountry.flag;
        selectedName.text = _selectedCountry.countryName;
        selectedDescription.text = countryInfo[i];
        
        CmdPickCountry(i,GameManager.instance.player.netId,previous);
    }
    
    [Command(requiresAuthority = false)]
    public void CmdPickCountry(int i, uint id, Country previous)
    {
        GameManager.instance.countries[i].playerId = id;

        if (previous != null)
        {
            previous.isPlayer = false;
        }
        
        _selectedCountry.isPlayer = true;
    }
    [Command(requiresAuthority = false)]
    public void StartGame()
    {
        CloseMenu();
    }
    [ClientRpc]
    public void CloseMenu()
    {
        selectionScreen.gameObject.SetActive(false);
    }
}
