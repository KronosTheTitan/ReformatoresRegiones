using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using World;

public class ProvinceMenu : Menu
{
    [SerializeField] private GameObject provinceMenu;

    [SerializeField] private TMP_Text developmentText;    
    
    [SerializeField] private Province province;

    public override void Open()
    {
        UpdateMenu();
        base.Open();
    }

    private void UpdateMenu()
    {
        developmentText.text = "Province level: " + province.developmentLevel;
    }
}
