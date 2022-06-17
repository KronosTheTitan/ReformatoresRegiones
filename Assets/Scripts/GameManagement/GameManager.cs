using System;
using EventCards;
using Mirror;
using Unity.VisualScripting;
using UnityEngine;
using World;

namespace GameManagement
{
    public class GameManager : NetworkBehaviour
    {
        public static GameManager instance;

        public Province[] provinces;
        public Country[] countries;

        public EventCardManager eventCardManager;

        [SyncVar] public int activeCountryIndex;

        [SyncVar] public Country activeCountry;

        public Player player;

        private void Start()
        {
            instance=this;
        }

        /// <summary>
        /// Generate the game world automatically.
        /// </summary>
        public void GenerateWorld()
        {
            int id = 0;
            foreach (Province province in provinces)
            {
                province.Generate(id);
                id++;
            }
        }
        
        [Command(requiresAuthority = false)]
        public void NextTurn()
        {
            if(activeCountry.eventQueue.Count>0) return;
            activeCountryIndex++;
            if (activeCountryIndex >= countries.Length) activeCountryIndex = 0;
            activeCountry = countries[activeCountryIndex];
            
            //Run the code for the start of a countries turn, things like add province yields and update manpower recovery.
            
            activeCountry.OnNextTurn();
            
            Debug.Log("Hi this is the server calling a new turn");
            eventCardManager.AddRandomEventToQueue(activeCountry);
            ForceUIUpdate();
        }
        [Server]
        public void ForceUIUpdate()
        {
            UIManager.instance.UpdateUI(activeCountry,activeCountryIndex);
        }
    }
}
