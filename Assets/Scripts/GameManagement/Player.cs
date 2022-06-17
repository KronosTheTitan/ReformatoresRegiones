using System;
using System.Collections;
using System.Collections.Generic;
using GameManagement;
using Mirror;
using UnityEngine;
using UnityEngine.Networking.Types;

public class Player : NetworkBehaviour
{
    private void Start()
    {
        if(isLocalPlayer)
            GameManager.instance.player = this;
    }
}
