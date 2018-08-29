using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardListing : MonoBehaviour {

    [Header("Deck")]
    public List<GameObject> cardList;
    public List<Card> scriptableInfo;

    [Space]
    public GameObject[] cardArray;
    public Card[] scriptableArray;
   
    [Header("Off Hand Card")]
    public GameObject offHandCard;

    [Header("Gamemanager")]
    Gamemanager gamemanager;
    Player player;


    bool startHappened;
    // Use this for initialization
    public void Start ()
    {
        if(!startHappened)
        {
            cardList = new List<GameObject>(cardArray);
            scriptableInfo = new List<Card>(scriptableArray);

            gamemanager = FindObjectOfType<Gamemanager>();
            player = FindObjectOfType<Player>();

            int fistCard = Random.Range(0, cardList.Count);
            RemoveFirstCard(fistCard);
            startHappened = true;
        }
    }
    public int GetRandom()
    {
        int rand = Random.Range(0, cardList.Count);
        return rand;
    }
    public void GetCard(int playerIndex)
    {
        MoveCard(GetRandom(), playerIndex);
    }

    private void RemoveFirstCard(int selected)
    {
        offHandCard = cardList[selected];
        cardList.RemoveAt(selected);
        scriptableInfo.RemoveAt(selected);
    }

    private void MoveCard(int cardIndex, int playerIndex)
    {
        gamemanager.playersInGame[playerIndex].infoOnHand.Add(scriptableInfo[cardIndex]);

        gamemanager.playersInGame[playerIndex].cardsOnHand.Add(cardList[cardIndex]);
        cardList.RemoveAt(cardIndex);
        scriptableInfo.RemoveAt(cardIndex);

    }


}
