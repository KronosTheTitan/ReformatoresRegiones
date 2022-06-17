using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private NetworkManager networkManager;

    [SerializeField] private Canvas multiplayerMenu;
    [SerializeField] private Canvas mainMenu;

    public void StartSinglePlayer()
    {
        networkManager.maxConnections = 1;
        networkManager.StartHost();
        //SceneManager.LoadScene("MainBoard");
        Debug.Log("Loading Complete");
    }

    public void OpenMultiplayerMenu()
    {
        multiplayerMenu.gameObject.SetActive(true);
        mainMenu.gameObject.SetActive(false);
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
