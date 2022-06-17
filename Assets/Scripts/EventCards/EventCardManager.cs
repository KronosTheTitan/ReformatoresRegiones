using System.Collections.Generic;
using GameManagement;
using Mirror;
using UnityEngine;
using World;

namespace EventCards
{
    public class EventCardManager : NetworkBehaviour
    {
        [SerializeField] private List<EventCard> events;
        
        [Server]
        public void AddRandomEventToQueue(Country country)
        {
            List<EventCard> potentialEvents = new List<EventCard>();
            foreach (EventCard eventCard in events)
                if (eventCard.Allowed(country))
                    potentialEvents.Add(eventCard);
            EventCard selected = potentialEvents[Random.Range(0, potentialEvents.Count)];
            GameObject newGameObject = Instantiate(selected.gameObject);
            selected = newGameObject.GetComponent<EventCard>();
            selected.receiver = country;
            NetworkServer.Spawn(newGameObject);
            country.eventQueue.Add(selected);
            country.SyncQueue(country.eventQueue);
            GameManager.instance.ForceUIUpdate();
        }
    }
}