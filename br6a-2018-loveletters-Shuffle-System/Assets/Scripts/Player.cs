using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public Transform[] cardsWaypoints;

    // public GameObject firstCard, secondCard;
    public List<Card> infoOnHand;
    public List<GameObject> cardsOnHand;

    protected Gamemanager gamemanager;
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
        //STUFF
    }
    public void ThrowCard(GameObject goCard, int i)
    {
        if (goCard != null)
        {
            Debug.Log("You have thrown " + goCard.name);
            gamemanager.cardHistory.Add(goCard);
            RemoveCard(i);
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
    private void Movement(GameObject goCard)
    {
      float step = movementSpeed * Time.deltaTime;
      goCard.transform.position = Vector3.MoveTowards(transform.position, cardsWaypoints[2].transform.position, step);
      if (goCard.transform.position == cardsWaypoints[2].transform.position)
           movecard = false;
    }

    public virtual void Turn()
    {

    }

}
