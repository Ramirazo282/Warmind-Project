using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guardian : Card {

    public override void Ability(Player target, Card selectedCard)
    {
        if (target.infoOnHand[0].powerLevel == selectedCard.powerLevel)
        {
          //target.ThrowCard(target.onPlayerHand[0]);
            target.isDead = true;
        }


        //Finalizar el turno
        gamemanager.PassTurn();
    }


}
