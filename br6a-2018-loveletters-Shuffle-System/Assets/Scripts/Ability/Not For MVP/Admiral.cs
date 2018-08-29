using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Admiral : Card
{
    public override void Ability(Player target, Player myself)
    {
        target.infoOnHand.Add(myself.infoOnHand[0]);
        myself.infoOnHand.RemoveAt(0);
        myself.infoOnHand.Add(target.infoOnHand[0]);
        target.infoOnHand.RemoveAt(0);




        //Finalizar el turno
        gamemanager.PassTurn();
    }
}
