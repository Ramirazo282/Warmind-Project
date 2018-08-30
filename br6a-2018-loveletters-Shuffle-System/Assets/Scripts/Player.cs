using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [Header("Card Location")]
    public Transform cardEndWaypoint, start1Location, start2Location;
    
    [Header("Card Lists")]
    public List<Card> infoOnHand;
    public List<GameObject> cardsOnHand;

    Gamemanager gamemanager;
    CardDisplay display;
    PlayerSelection playerSelection;
    ColorChange colorChange;

    public int firstPlayer;
    public int selectedCard;

    public bool hasAdviser;
    public bool hasSecondCard = false;
    public bool canBeSelected = false;
    public bool isDead = false;
    public bool isProtected = false;
    public bool isAI;
    bool movecard = false;

    public bool isPlayersTurn = false;


    //Card Movement
    public float movementSpeed = 1.0f;

    public virtual void Start()
    {
        gamemanager = FindObjectOfType<Gamemanager>();
        display = FindObjectOfType<CardDisplay>();
        playerSelection = FindObjectOfType<PlayerSelection>();
        colorChange = FindObjectOfType<ColorChange>();

       // colorChange.Start();
    }
    void Update()
    {
        if (cardsOnHand.Count >= 2)
            hasSecondCard = true;
        else
            hasSecondCard = false;
        if (isPlayersTurn)
        {
           
            if (Input.GetKeyDown(KeyCode.Z) && !isAI)
            {
                ThrowCard(cardsOnHand[0], 0);
                if (movecard)
                    Movement(cardsOnHand[0]);
            }
            if (Input.GetKeyDown(KeyCode.X) && !isAI)
            {
                ThrowCard(cardsOnHand[1], 1);
                if (movecard)
                    Movement(cardsOnHand[1]);

            }
        }
    }

    public void SpawnCard()
    {
        
    }
    public void ThrowCard(GameObject goCard, int i)
    {
        if (goCard != null)
        {
            Debug.Log("You have thrown " + goCard.name);

            GameObject card = Instantiate(goCard, cardEndWaypoint);

            gamemanager.cardHistory.Add(goCard);
            RemoveCard(i);

            isPlayersTurn = false;
            gamemanager.PassTurn();

            /*
            canBeSelected = true;
            if (playerSelection.hasBeenSelected)
            {
                canBeSelected = false;
                gamemanager.PassTurn();
            }
            */

        }

    }
    private void RemoveCard (int c)
    {
        cardsOnHand.RemoveAt(c);
        infoOnHand.RemoveAt(c);
    }
    //Future Imolement
    private void Movement(GameObject goCard)
    {
      float step = movementSpeed * Time.deltaTime;
      goCard.transform.position = Vector3.MoveTowards(transform.position, start1Location.transform.position, step);
      if (goCard.transform.position == start2Location.transform.position)
           movecard = false;
    }

    public virtual void Turn()
    {

    }

}
