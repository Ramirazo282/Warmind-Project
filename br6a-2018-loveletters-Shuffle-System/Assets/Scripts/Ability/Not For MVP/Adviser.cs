using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adviser : Card
{
    public override void Ability()
    {
        gamemanager.PassTurn();
    }
}
