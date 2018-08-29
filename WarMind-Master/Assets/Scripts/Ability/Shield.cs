using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Card
{
    public override void Ability(Player target)
    {
        target.isProtected = true;


        //Finalizar el turno
        gamemanager.PassTurn();
    }
}
