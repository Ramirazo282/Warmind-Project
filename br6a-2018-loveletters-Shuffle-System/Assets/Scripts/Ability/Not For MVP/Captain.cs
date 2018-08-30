using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Captain : Card {

    public override void Ability(Player Target)
    {
        gamemanager.PassTurn();
    }
}
