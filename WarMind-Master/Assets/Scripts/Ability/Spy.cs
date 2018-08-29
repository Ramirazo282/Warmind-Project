using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spy : Card
{
    public override void Ability(Player target, Player myself)
    {
        Debug.Log(target.infoOnHand[0].name);
        Debug.Log(myself.infoOnHand[0].name);


        //Finalizar el turno
        gamemanager.PassTurn();
    }
}
