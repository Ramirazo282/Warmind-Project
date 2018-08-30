using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Gamemanager : MonoBehaviour {

    public GameObject[] playerArray;
    public Player[] playersInGame;

    CardListing cardListing;

    public Text notificationText;

    public List<GameObject> cardHistory;

    public int round;
    public int playerTurnNumber;
    private int turnCouter;
    private float timeForNextCard = .5f;

    // Use this for initialization
    void Start()
    {
        //Finding CODE
        cardListing = FindObjectOfType<CardListing>();
        cardListing.Start();
        cardHistory = new List<GameObject>();

        //Shuffle To ALL Players
        SelectStartingPlayer();
        for (int i = 0; i < playersInGame.Length; i++)
        {
            cardListing.GetCard(i);
        }
    }
    void Update()
    {
        playersInGame[playerTurnNumber].isPlayersTurn = true;
        if (!playersInGame[playerTurnNumber].hasSecondCard)
        {
            cardListing.GetCard(playerTurnNumber);
        }
        CheckDeath();

        if (Input.GetKeyDown(KeyCode.G) && !playersInGame[playerTurnNumber].hasSecondCard)
        {
            
            if(playersInGame[playerTurnNumber] is AI)
            {
                playersInGame[playerTurnNumber].Turn();
            }
        }
    }

    public void PassTurn()
    {
        playerTurnNumber++;
        turnCouter++;

        if (playerTurnNumber >= 3)
            playerTurnNumber = 0;

        if (turnCouter >= 4)
            PassRound();

        if (playersInGame[playerTurnNumber].isDead)
            playerTurnNumber++;
    }
    private void PassRound()
    {
        round++;
        turnCouter = 0;
    }
    public void SelectStartingPlayer()
    {
        playerTurnNumber = Random.Range(0, playersInGame.Length);
        Debug.Log("Es el turno del jugador: " + playerTurnNumber);
    }
    private void CheckDeath()
    {
        if (playersInGame[0].isDead)
        {
            notificationText.text = "YOU LOST";
        }
        else if (playersInGame[1].isDead && playersInGame[2].isDead && playersInGame[3].isDead)
        {
            notificationText.text = "YOU WIN";
        }
    }
}
