using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] protected GameObject menuGameObject;
    
    public virtual void Open()
    {
        menuGameObject.SetActive(true);
        if (UIManager.instance.openMenu != null && UIManager.instance.openMenu != this)
        {
            UIManager.instance.openMenu.Close();
        }

        UIManager.instance.openMenu = this;
    }

    public virtual void Close()
    {
        menuGameObject.SetActive(false);

        UIManager.instance.openMenu = null;
    }
}
