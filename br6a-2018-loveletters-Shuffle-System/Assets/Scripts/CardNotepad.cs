using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class CardNotepad : MonoBehaviour
{
    public bool [] cardNumber;
    public Card[] typeCards;
    public int posibleCards;
    public int [] cardNumberOrigen;
    public Card BaronKill;
    public bool isDead; // Ver si ya murio
    public Player Player;
    public bool spotted;

    public CardNotepad (Player player)
    {
        //Si tiene x tipo de carta
        cardNumber = new bool[8];
        //Retorno de x tipo de carta
        typeCards = new Card[8];
        typeCards[0] = new Guardian();
        typeCards[1] = new Spy();
        typeCards[2] = new BountyHunter();
        typeCards[3] = new Shield();
        typeCards[4] = new Captain();
        typeCards[5] = new Admiral();
        typeCards[6] = new Adviser();
        typeCards[7] = new Warmind();

        //Tiene que ver con el spotteo y un index creeeeo
        cardNumberOrigen = new int[8];

        Player = player;
    }
    public void RefreshList(Card ThrownCard) //Funcion que se llama cada vez que alguien tira una carta
    {
        for(int i = 0; i < cardNumber.Length; i++)
        {
            switch (cardNumberOrigen[i])
            {
                case 1:
                    if(ThrownCard.powerLevel  - 1 == i)
                    {
                        cardNumber[i] = false;
                        cardNumberOrigen[i] = 1;
                    }
                    else
                    {
                        cardNumber[i] = true;
                        cardNumberOrigen[i] = 0;
                    }
                    break;
                case 2:
                    int CartaVista = 0;
                    for(int j = 0; j < cardNumber.Length; j++)
                    {
                        if(cardNumber[j])
                            CartaVista = j + 1;
                    }
                    if(ThrownCard.powerLevel != CartaVista)
                    {
                        cardNumber[i] = false;
                        cardNumberOrigen[i] = 2;
                    }
                    else
                    {
                        cardNumber[i] = true;
                        cardNumberOrigen[i] = 0;
                    }
                    break;
                case 3:
                    if(ThrownCard.powerLevel > BaronKill.powerLevel)
                    {
                        cardNumber[i] = true;
                        cardNumberOrigen[i] = 0;
                    }
                    else
                    {
                        cardNumber[i] = false;
                        cardNumberOrigen[i] = 3;
                    }
                    break;
                case 5:
                    if(ThrownCard.powerLevel == 5)
                    {
                        cardNumber[6] = false;
                        cardNumberOrigen[6] = 5;
                    }
                    else
                    {
                        cardNumber[6] = true;
                        cardNumberOrigen[6] = 0;
                    }
                    break;
                case 7:
                    if(ThrownCard.powerLevel > 4)
                    {
                        cardNumber[i] = false;
                        cardNumberOrigen[i] = 7;
                    }
                    else
                    {
                        cardNumber[i] = true;
                        cardNumberOrigen[i] = 0;
                    }
                    break;
            }
        }
    }
    public int ProbabilityHighestCard(Card HighestCard)
    {
            if(!cardNumber[HighestCard.powerLevel - 1]) //Si no puede tener esa carta
            {
                return 0;
            }
            else
            {
                //La probabilidad de tener esa carta
                int Probabilidad = (int) (1/posibleCards) * 100;
                return Probabilidad; 
            }
    }
    public int ProbabilityBountyHunterKilling(Card MyCard, List<Card> RemainingCards)
    {
        int HigherCards = 0;
        for(int i = MyCard.powerLevel - 1; i < cardNumber.Length; i++)
        {
            for(int j = 0; j < RemainingCards.Count; j++)
            {
                if(cardNumber[i] && RemainingCards[j].powerLevel == i - 1)
                {
                   HigherCards++;
                }
            }
        }
        if(HigherCards == 0)
        {
            return 100;
        }
        else
        {
            int CartasPosiblesATener = 0;
            for(int i = 0; i < cardNumber.Length; i++)
            {
                for(int j = 0; j < RemainingCards.Count; j++)
                {
                    if(cardNumber[i] && RemainingCards[j].powerLevel == i - 1)
                    {
                        CartasPosiblesATener++;
                    }
                }
            }
            int Probabilidad = (int) (HigherCards/CartasPosiblesATener) * 100;
            return Probabilidad;
        }       
    }

    public int OnePosibleCard()
    {
        int i = Random.Range(1,cardNumber.Length);
        while(!cardNumber[i])
        {
            i = Random.Range(1,cardNumber.Length);
        }
        return i;
    }
}