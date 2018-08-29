using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BountyHunter : Card {

    public override void Ability(Player target, Player myself)
    {
        if (target.infoOnHand[0].powerLevel < myself.infoOnHand[0].powerLevel)
        {
            target.isDead = true;
        }
        else if (target.infoOnHand[0].powerLevel > myself.infoOnHand[0].powerLevel)
        {
            myself.isDead = true;
        }


        //Finalizar el turno
        gamemanager.PassTurn();
    }
}
