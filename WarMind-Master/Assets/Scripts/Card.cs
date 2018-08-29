using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject {

    //Scriptable Object Var's
	public int powerLevel;
    public int parameterType;

	public string Name;
	public string description;

  
    public Sprite artwork;

    //Comunication
    public Gamemanager gamemanager;

    void Start()
    {
        gamemanager = FindObjectOfType<Gamemanager>();
    }

    public virtual void Ability(Player target)
    {
    }

    public virtual void Ability(Player target, Player myself)
    {
    }

    public virtual void Ability(Player target, Card selectedCard)
    {
    }
    public virtual void Ability()
    {

    }
    public virtual void Ability(Player target, Player myself, CardNotepad[] Notepad)
    {

    }



}

