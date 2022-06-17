using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCard1 : EventCard
{
    public override void Option1()
    {
        receiver.ModifyTreasury(30);
        base.Option1();
    }
}
