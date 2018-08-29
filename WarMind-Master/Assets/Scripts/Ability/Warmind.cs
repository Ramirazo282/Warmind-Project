using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warmind : Card
{
	public override void Ability (Player target)
    {
        target.isDead = true;


        //Finalizar el turno
        gamemanager.PassTurn();
    }
}
