using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelection : MonoBehaviour
{

    Gamemanager gamemanager;
    ColorChange colorChange;
    Player player;

    public bool hasBeenSelected;

    void Start()
    {
        colorChange = FindObjectOfType<ColorChange>();
        gamemanager = FindObjectOfType<Gamemanager>();
        player = FindObjectOfType<Player>();
    }
    void OnMouseDown()
    {
        if (transform.name == "Player's 1 Cube")
        {
            Debug.Log("P1 HasBeenSelected");
            GetSelected(0);
            hasBeenSelected = true;
        }
        if (transform.name == "Player's 2 Cube")
        {
            Debug.Log("P2 HasBeenSelected");
            GetSelected(1);
            hasBeenSelected = true;
        }
        if (transform.name == "Player's 3 Cube")
        {
            Debug.Log("P3 HasBeenSelected");
            GetSelected(2);
            hasBeenSelected = true;
        }
        if (transform.name == "Player's 4 Cube")
        {
            Debug.Log("P4 HasBeenSelected");
            GetSelected(3);
            hasBeenSelected = true;
        }
    }
    public Player GetSelected(int i)
    {
        Debug.Log(gamemanager.playersInGame[i]);
        return gamemanager.playersInGame[i];
    }
    

}
