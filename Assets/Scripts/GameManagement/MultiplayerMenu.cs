using System.Collections;
using System.Collections.Generic;
using Mirror;
using Mirror.Discovery;
using UnityEngine;

public class MultiplayerMenu : MonoBehaviour
{
    readonly Dictionary<long, ServerResponse> discoveredServers = new Dictionary<long, ServerResponse>();
    
    [SerializeField] private NetworkManager networkManager;

    [SerializeField] private NetworkDiscovery networkDiscovery;

    [SerializeField] private int maxPlayersMp = 9;
    
    [SerializeField] private Canvas multiplayerMenu;
    [SerializeField] private Canvas mainMenu;

    public void HostMultiplayer()
    {
        networkManager.maxConnections = maxPlayersMp;
        networkManager.StartHost();
        networkDiscovery.AdvertiseServer();
        Debug.Log("Loading Complete");
    }

    public void JoinServer()
    {
        void Connect(ServerResponse info)
        {
            networkDiscovery.StopDiscovery();
            networkManager.StartClient(info.uri);
        }
    }

    public void Refresh()
    {
        discoveredServers.Clear();
        networkDiscovery.StartDiscovery();
        Debug.Log("check for Servers");
    }

    public void Back()
    {
        multiplayerMenu.gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(true);
    }
}
