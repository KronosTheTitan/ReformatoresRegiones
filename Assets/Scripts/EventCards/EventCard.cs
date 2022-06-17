using System.Collections;
using System.Collections.Generic;
using GameManagement;
using Mirror;
using UnityEditor.Experimental;
using UnityEngine;
using World;

public class EventCard : NetworkBehaviour
{
    [SyncVar] public Country receiver;

    public Canvas eventBarItem;

    [SerializeField] Canvas menu;

    public virtual void EvaluateAI()
    {
    }

    public void Open()
    {
        menu.gameObject.SetActive(true);
    }

    [ClientRpc]
    public void Close()
    {
        if (!receiver.isPlayer)
        {
            receiver.eventQueue.Remove(this);
            NetworkServer.Destroy(gameObject);
            Debug.Log("test2");
            GameManager.instance.ForceUIUpdate();
        }
        else
        {
            receiver.eventQueue.Remove(this);
            NetworkServer.Destroy(gameObject);
            Debug.Log("test2");
            GameManager.instance.ForceUIUpdate();
        }
    }

    [Command(requiresAuthority = false)]
    public virtual void Option1()
    {
        Close();
    }
    
    [Command(requiresAuthority = false)]
    public virtual void Option2()
    {
    }

    public virtual bool Allowed(Country country)
    {
        return true;
    }

    private void Update()
    {
        if (!menu.gameObject.activeSelf) return;
        if (Input.GetKeyDown(KeyCode.Alpha1)) Option1();
        if (Input.GetKeyDown(KeyCode.Alpha2)) Option2();
    }
}