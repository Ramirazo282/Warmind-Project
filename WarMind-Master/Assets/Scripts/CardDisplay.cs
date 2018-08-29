using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour {

    //Selected Card
    public Card selectedCard;

    //Comunication Scripts
   // public Gamemanager gamemanager;

	// Card Description Var's
	public Text nameText;
	public Text descriptionText;
	public Text powerLevelText;

	public Image artwork;

    int totalCards;

    void Update()
    {
        ShowCards();
    }


    private void ShowCards()
    {
       
       //Card Tranformation

       nameText.text = selectedCard.name;
       descriptionText.text = selectedCard.description;

       powerLevelText.text = selectedCard.powerLevel.ToString();

       //artwork.sprite = selectedCard.artwork;
    }

    

}
