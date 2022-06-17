using System.Collections.Generic;
using Mirror;
using UnityEngine;

namespace World
{
    public class Country : NetworkBehaviour
    {
        public int treasury => _treasury;

        public string countryName;

        public List<Province> ownedProvinces;

        [SyncVar] private int _treasury;

        [SyncVar] public bool isPlayer;
        [SyncVar] public uint playerId;

        public List<EventCard> eventQueue = new List<EventCard>();

        [ClientRpc]
        public void SyncQueue(List<EventCard> newQueue)
        {
            eventQueue = newQueue;
        }

        [Command(requiresAuthority = false)]
        public void ModifyTreasury(int money)
        {
            //Debug.Log("this is the server");
            _treasury += money;
        }

        public void OnNextTurn()
        {
            manpowerCap = 0;

            foreach (Province province in ownedProvinces)
            {
                province.OnNextTurn();
            }
            
            UpdateManpower();
        }
        
        //Manpower Handling
        [SyncVar] public int manpowerCap;
        [SyncVar] public int manpowerCurrent;
        [SyncVar] public int manpowerUsed;
        [SyncVar] private int _manpowerGraveyard0;
        [SyncVar] int _manpowerGraveyard1;
        [SyncVar] int _manpowerGraveyard2;
        
        public void TakeCasualty()
        {
            manpowerUsed--;
            _manpowerGraveyard0++;
        }
        
        private void UpdateManpower()
        {
            _manpowerGraveyard2 = _manpowerGraveyard1;
            _manpowerGraveyard1 = _manpowerGraveyard0;
            manpowerCurrent = manpowerCap - manpowerUsed - _manpowerGraveyard1 - _manpowerGraveyard2;
        }
        
        //UI info
        public Sprite flag;
    }
}
