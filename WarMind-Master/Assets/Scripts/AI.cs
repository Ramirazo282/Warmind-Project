using UnityEngine;
using System.Collections.Generic;
using System.Collections;


public class AI : Player    
{
    public List<int> thrownCards; //int provicional
    public CardNotepad [] PlayersCanHave;
    public CardListing cardListing;
    public List<Card> remainingCards;
    public Player[] playerList;
    public override void Start()
    {
        thrownCards = new List<int>();
        PlayersCanHave = new CardNotepad[3];
        for(int i = 0; i < PlayersCanHave.Length; i++)
        {
            PlayersCanHave[i] = new CardNotepad(playerList[i]);
        }
        base.Start();
    }
    public override void Turn()
    {
        if(infoOnHand[0].powerLevel == 8 || infoOnHand[1].powerLevel == 8)
        {
            //Tirar la que no es la princesa, si es guardia o baron definir target con la funcion, si es principe, rey o clero es el menor conocimiento
            if(infoOnHand[0].powerLevel == 1 || infoOnHand[1].powerLevel == 1)
            {
                Guard();
            }
            else if(infoOnHand[0].powerLevel == 3 || infoOnHand[1].powerLevel == 3)
            {
                BountyHunter();
            }
            else if(infoOnHand[0].powerLevel == 2 || infoOnHand[1].powerLevel == 2 || infoOnHand[0].powerLevel == 5 || infoOnHand[1].powerLevel == 5 || infoOnHand[0].powerLevel == 6 || infoOnHand[1].powerLevel == 6)
            {
                if(infoOnHand[0].powerLevel == 8)
                {
                    ThrowRandomCard(infoOnHand[1]);
                }
                else
                {
                    ThrowRandomCard(infoOnHand[0]);
                }
            }
        }
        else
        {
            //Ver si es la ultima ronda
            if(ItsLastRount())
            {
                //Ver si tengo la carta mas alta
                if(HaveTheHighCard())
                {
                    if(infoOnHand[0].powerLevel == 6 || infoOnHand[1].powerLevel == 6)
                        HavingKingHighCard();
                    else
                    {
                        Card CartaATirar = GetLocalLowCard();
                        //Tirar CartaATirar (Falta checkear tipo de parametros y buscarlos)
                        ThrowRandomCard(CartaATirar);
                    }
                }
                else // Si no tengo la carta mas alta
                {
                    if (infoOnHand[0].powerLevel == 6 || infoOnHand[1].powerLevel == 6)
                    {
                        HavingKingLowCard();
                    }
                    else if (infoOnHand[0].powerLevel == 1 || infoOnHand[1].powerLevel == 1)
                    {
                        HavingGuardLowCard();
                    }
                    else if (infoOnHand[0].powerLevel == 5 || infoOnHand[1].powerLevel == 5)
                    {
                        HavingPrinceLowCard();
                    }
                    else
                    {
                        if(infoOnHand[0].powerLevel < infoOnHand[1].powerLevel)
                        {
                            //Tiro [0] (Parametro Random)
                            ThrowRandomCard(infoOnHand[0]);
                        }
                        else
                        {
                            //Tiro [1] (Parametro Random)
                            ThrowRandomCard(infoOnHand[1]);
                        }
                    }
                }
            }
            else
            {
                //Fijarse si esta spotteado
                if(GotSpottedByHowMany() > 1)
                {
                    //Descartate negro (Tirar la carta [0])
                    ThrowRandomCard(infoOnHand[0]);
                }
                else if(GotSpottedByHowMany() == 1)
                {
                    CardNotepad SpottingGuy = GotSpottedBy();
                    if(CanKill(SpottingGuy))
                    {
                        Kill(SpottingGuy);
                    }
                    else
                    {
                        //Descartate negro (Tirar la carta [0])
                        ThrowRandomCard(infoOnHand[0]);
                    }
                }
                else
                { 
                    //Tengo fichado a alguien? Si es asi matalo
                    bool HasSomeoneSpotted = false;
                    for(int i = 0; i < PlayersCanHave.Length; i++)
                    {
                        if(CanKill(PlayersCanHave[i]))
                        {
                            Kill(PlayersCanHave[i]);
                            HasSomeoneSpotted = true;
                            break;
                        }
                    }
                    if(!HasSomeoneSpotted)
                    {
                        //Puedo Spottear a alguien?             
                        if(infoOnHand[0].powerLevel == 2)
                        {
                            //Tirar Clero [0]
                            Player Target = LeastKnowledge();
                            infoOnHand[0].Ability(Target, this);
                        }
                        else if(infoOnHand [1].powerLevel == 2)
                        {
                            //Tirar Clero [1]
                            Player Target = LeastKnowledge();
                            infoOnHand[1].Ability(Target, this);
                        }
                    }
                    else
                    {
                        if(infoOnHand[0].powerLevel == 8 || infoOnHand[1].powerLevel == 8)
                        {
                            //Tirar la que no es la princesa, si es guardia o baron definir target con la funcion, si es principe, rey o clero es el menor conocimiento
                            if (infoOnHand[0].powerLevel == 1 || infoOnHand[1].powerLevel == 1)
                            {
                                Guard();
                            }
                            else if (infoOnHand[0].powerLevel == 3 || infoOnHand[1].powerLevel == 3)
                            {
                                BountyHunter();
                            }
                            else if (infoOnHand[0].powerLevel == 2 || infoOnHand[1].powerLevel == 2 || infoOnHand[0].powerLevel == 5 || infoOnHand[1].powerLevel == 5 || infoOnHand[0].powerLevel == 6 || infoOnHand[1].powerLevel == 6)
                            {
                                if (infoOnHand[0].powerLevel == 8)
                                {
                                    ThrowRandomCard(infoOnHand[1]);
                                }
                                else
                                {
                                    ThrowRandomCard(infoOnHand[0]);
                                }
                            }
                        }
                        else
                        {  
                            bool FinalizoTurno = false;
                            //Ver si combiene matar con Baron
                            if(infoOnHand[0].powerLevel == 3 || infoOnHand[1].powerLevel == 3)
                            {
                                if(MayKillBountyHunter())
                                {
                                    BountyHunter();
                                    FinalizoTurno = true;
                                }
                            }
                            if(!FinalizoTurno)
                            {
                                if(infoOnHand[0].powerLevel == 4 || infoOnHand[1].powerLevel == 4)
                                {
                                    //Tirar el 4
                                    if(infoOnHand[0].powerLevel == 4)
                                    {
                                        infoOnHand[0].Ability(this);
                                    }
                                    else
                                    {
                                        infoOnHand[1].Ability(this);
                                    }
                                    FinalizoTurno = true;
                                }
                                else if(infoOnHand[0].powerLevel == 5 || infoOnHand[1].powerLevel == 5)
                                {
                                    if(infoOnHand[0].powerLevel == 5 && infoOnHand[1].powerLevel == 5)
                                    {
                                        //Tirar [1] y elegir al target con menos conocimiento (Mas posibles cartas)
                                        Player Target = LeastKnowledge();
                                        infoOnHand[1].Ability(Target);
                                        
                                    }
                                    else if(infoOnHand[0].powerLevel == 6 || infoOnHand[1].powerLevel == 6)
                                    {
                                        //Tirar el pricipe y autodescartarse
                                        if(infoOnHand[0].powerLevel == 5)
                                        {
                                            infoOnHand[0].Ability(this);
                                        }
                                        else
                                        {
                                            infoOnHand[1].Ability(this);
                                        }
                                    }
                                    else if(infoOnHand[0].powerLevel == 7 || infoOnHand[1].powerLevel == 7)
                                    {
                                        //Tirar 7
                                        if(infoOnHand[0].powerLevel == 7)
                                        {
                                            infoOnHand[0].Ability();
                                        }
                                        else
                                        {
                                            infoOnHand[1].Ability();
                                        }
                                    }
                                    else if(infoOnHand[0].powerLevel == 8 || infoOnHand[1].powerLevel == 8)
                                    {
                                        //Tirar 5 y elegir al target con menos conocimiento (Mas posibles cartas)
                                        Player Target = LeastKnowledge();
                                        if(infoOnHand[0].powerLevel == 5)
                                        {
                                            infoOnHand[0].Ability(Target);
                                        }
                                        else
                                        {
                                            infoOnHand[1].Ability(Target);
                                        }
                                    }
                                }
                                else if(infoOnHand[0].powerLevel == 6 || infoOnHand[1].powerLevel == 6)
                                {
                                    if(infoOnHand[0].powerLevel == 7 || infoOnHand[1].powerLevel == 7)
                                    {
                                        //Tirar 7
                                        if(infoOnHand[0].powerLevel == 7)
                                        {
                                            infoOnHand[0].Ability();
                                        }
                                        else
                                        {
                                            infoOnHand[1].Ability();
                                        }
                                    }
                                    else if(infoOnHand[0].powerLevel == 8 || infoOnHand[1].powerLevel == 8)
                                    {
                                        //Tirar 6
                                        if(infoOnHand[0].powerLevel == 6)
                                        {
                                            ThrowRandomCard(infoOnHand[0]);
                                        }
                                        else
                                        {
                                            ThrowRandomCard(infoOnHand[1]);
                                        }
                                        
                                    }
                                }
                                else if(infoOnHand[0].powerLevel == 7 || infoOnHand[1].powerLevel == 7)
                                {
                                    //Random para ver si tirar 7 o la otra
                                    int randomindex = Random.Range(0, 1);
                                    ThrowRandomCard(infoOnHand[randomindex]);
                                }
                                else
                                {
                                    Guard();
                                }
                        }
                    }
                }
            }
        }
        }
    }
    Card GetLocalLowCard()
    {
        if(infoOnHand[0].powerLevel < infoOnHand[1].powerLevel)
            return infoOnHand[0];
        else
            return infoOnHand[1];
    }
    void Kill(CardNotepad Target)
    {
        if(infoOnHand[0].powerLevel == 1 || infoOnHand[1].powerLevel == 1)
        {
            if(Target.posibleCards == 1 && !Target.cardNumber[0])
            {
                for(int i = 1; i < Target.cardNumber.Length; i++)
                {
                    if(Target.cardNumber[i])
                    {
                        //Matar a Target con parametro indice i
                        if(infoOnHand[0].powerLevel == 1)
                        {
                            infoOnHand[0].Ability(Target.Player, Target.typeCards[i]);
                        }
                        else if(infoOnHand[1].powerLevel == 1)
                        {
                            infoOnHand[1].Ability(Target.Player, Target.typeCards[i]);
                        }
                    }
                }
            }
        }
        else if(infoOnHand[0].powerLevel == 3 || infoOnHand[1].powerLevel == 3)
        {
            //Sacar cual es la carta que no es el baron
            int MyIndex = 0;
            if(infoOnHand[0].powerLevel == 3 && infoOnHand[1].powerLevel == 3)
            {
                MyIndex = 1;
            }
            else
            {
                for(int i = 0; i < infoOnHand.Count; i++)
                {
                    if(infoOnHand[i].powerLevel != 2) //BountyHunter Index
                    {
                        MyIndex = i;
                    }
                }
            }
            bool Cant = false;
            for(int i = infoOnHand[MyIndex].powerLevel - 1; i < Target.cardNumber.Length; i++)
            {
                if(Target.cardNumber[i])
                    Cant = true;
            }
            if(!Cant)
            {
                //Tirar Baron y comparar con Target
                infoOnHand[MyIndex].Ability(Target.Player, this);
            }
        }
        else if(infoOnHand[0].powerLevel == 5 || infoOnHand[1].powerLevel == 5)
        {
            if(Target.posibleCards == 1 && Target.cardNumber[7])
            {
                //Descartar a Target
                if(infoOnHand[0].powerLevel == 5)
                {
                    infoOnHand[0].Ability(Target.Player);
                }
                else
                {
                    infoOnHand[1].Ability(Target.Player);
                }
            }
        }
    }
    bool CanKill(CardNotepad Target)
    {
        if(infoOnHand[0].powerLevel == 1 || infoOnHand[1].powerLevel == 1)
        {
            if(Target.posibleCards == 1 && !Target.cardNumber[0])
            {
                return true;
            }
        }
        else if(infoOnHand[0].powerLevel == 3 || infoOnHand[1].powerLevel == 3)
        {
            //Sacar cual es la carta que no es el baron
            int MyIndex = 0;
            if(infoOnHand[0].powerLevel == 3 && infoOnHand[1].powerLevel == 3)
            {
                MyIndex = 1;
            }
            else
            {
                for(int i = 0; i < infoOnHand.Count; i++)
                {
                    if(infoOnHand[i].powerLevel != 2) //BountyHunter Index
                    {
                        MyIndex = i;
                    }
                }
            }
            bool Cant = false;
            for(int i = infoOnHand[MyIndex].powerLevel - 1; i < Target.cardNumber.Length; i++)
            {
                if(Target.cardNumber[i])
                    Cant = true;
            }
            if(!Cant)
            {
                return true;
            }
        }
        else if(infoOnHand[0].powerLevel == 5 || infoOnHand[1].powerLevel == 5)
        {
            if(Target.posibleCards == 1 && Target.cardNumber[7])
            {
                return true;
            }
        }
        return false;
    }
    void CanKillWithGuard(CardNotepad Target) //Falta hacer el ataque con el guardia
    {
        if(infoOnHand[0].powerLevel == 1 || infoOnHand[1].powerLevel == 1) //Si una de las 2 cartas es un guardia
        {
            if(Target.posibleCards == 1 && !Target.cardNumber[0]) // Si tiene una sola carta posible y esa carta no es un guardia
            {
                for(int i = 1; i < Target.cardNumber.Length; i++)
                {
                    if(Target.cardNumber[i]) // Esta carta es la unica posible
                    {
                        //Atacar con el guardia
                        if(infoOnHand[0].powerLevel == 1)
                        {
                            infoOnHand[0].Ability(Target.Player, Target.typeCards[i]);
                        }
                        else if(infoOnHand[1].powerLevel == 0)
                        {
                            infoOnHand[1].Ability(Target.Player, Target.typeCards[i]);
                        }
                    }
                }
            }
        }
    }
    void CanKillWithBountyHunter(CardNotepad Target)
    {
        int TargetCardIndex = 0;
        int MyIndex = 0;
        if (infoOnHand[0].powerLevel == 3 || infoOnHand[1].powerLevel == 3)
        {
            if(Target.posibleCards == 1)
            {
                for(int i = 0; i < Target.cardNumber.Length; i++)
                {
                    if(Target.cardNumber[i])
                    {
                        TargetCardIndex = i;
                    }
                }
                for(int i = 0; i < infoOnHand.Count; i++)
                {
                    if(infoOnHand[i].powerLevel != 2) //BountyHunter Index
                    {
                        MyIndex = i;
                    }
                }

                if(infoOnHand[MyIndex].powerLevel > TargetCardIndex + 1)
                {
                    infoOnHand[MyIndex].Ability(Target.Player, this);
                }
            }
        }
    }
    void CanKillWithPrince(CardNotepad Target)
    {
        if(infoOnHand[0].powerLevel == 5 || infoOnHand[1].powerLevel == 5)
        {
            int PrincessIndex = Target.cardNumber.Length - 1;
            if(Target.posibleCards == 1 && Target.cardNumber[PrincessIndex])
            {
                if(infoOnHand[0].powerLevel == 5)
                {
                    infoOnHand[0].Ability(Target.Player);
                }
                else if(infoOnHand[1].powerLevel == 5)
                {
                    infoOnHand[1].Ability(Target.Player);
                }
            }
        }
    }
    bool ItsLastRount()
    {   
        int playersRemaining = 0;
        for(int i = 0; i < PlayersCanHave.Length; i++)
        {
            if(!PlayersCanHave[i].isDead)
                playersRemaining++;
        }
        int cardsNextTurn = cardListing.cardList.Count;
        // Si voy a tener a volver a jugar
        if(cardsNextTurn <= 0)
            return true;
        else
            return false;
    }
    /*Funcion que se llama cada vez que se tira una 
    carta para eliminarla de la lista de cartas restantes */
    void RefreshThrownCards(Card thrownCard)
    {
        remainingCards.Remove(thrownCard);
        bool CartaAgotada = true;
        for(int i = 0; i < remainingCards.Count; i++)
        {
            if(remainingCards[i].powerLevel == thrownCard.powerLevel)
                CartaAgotada = false;
        }
        if(CartaAgotada)
        {
            for(int i = 0; i < PlayersCanHave.Length; i++)
            {
                PlayersCanHave[i].cardNumber[thrownCard.powerLevel - 1] = false;
            }
        }
    }
    int GotSpottedByHowMany()
    {
        int howMany = 0;
        for(int i = 0; i < PlayersCanHave.Length; i++)
        {
            if(PlayersCanHave[i].spotted)
                howMany++;
        }
        return howMany;
    }
    CardNotepad GotSpottedBy() //Devuelve el jugador que te spotteo
    {
        for(int i = 0; i < PlayersCanHave.Length; i++)
        {
            if(PlayersCanHave[i].spotted)
                return PlayersCanHave[i];
        }
        return null;
    }

    void ThrowOldCard()
    {
        if(MustThrowCondesa())
        {
            if(infoOnHand[0].powerLevel == 7)
            {
                infoOnHand[0].Ability();
            }
            else if(infoOnHand[1].powerLevel == 7)
            {
                infoOnHand[1].Ability();
            }
        }
        else
        {
            ThrowRandomCard(infoOnHand[0]);
        }
    }
    bool MustThrowCondesa()
    {
        if(infoOnHand[0].powerLevel == 7 || infoOnHand[1].powerLevel == 7)
        {
            if(infoOnHand[0].powerLevel == 5 || infoOnHand[1].powerLevel == 5 || infoOnHand[0].powerLevel == 6 || infoOnHand[1].powerLevel == 6)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
    bool HaveTheHighCard()
    {
        //Sacar el index de mi carta alta
        int MyIndex;
        if(infoOnHand[1].powerLevel > infoOnHand[0].powerLevel)
            MyIndex = 1;
        else
            MyIndex = 0;
        
        //Ver cual es el numero de cartas superiores no tiradas
        int HigherCards = 0;
        for(int i = 0; i < remainingCards.Count; i++)
        {
            if(remainingCards[i].powerLevel > infoOnHand[MyIndex].powerLevel) // Mismo value lo deja pasar
                HigherCards++;
        }
        if(HigherCards == 0)
            return true;
        else
            return false;
    }
    Player SearchHighestCard()
    {
        Card[] HighCardFromEachPlayer = new Card [PlayersCanHave.Length];
        int[] posibleCards = new int [PlayersCanHave.Length];
        //Conseguir la carta mas elevada de cada jugador y cuantas cartas puede tener, mientras menos pueda tener mayor chance de tener esa carta elevada
        for (int i = 0; i < PlayersCanHave.Length; i++)
        {
            for(int j = PlayersCanHave[i].cardNumber.Length; j >= 0; j--)
            {
                if (PlayersCanHave[i].cardNumber[j])
                {
                    HighCardFromEachPlayer[i] = PlayersCanHave[i].typeCards[j];
                    posibleCards[i] = PlayersCanHave[i].posibleCards;
                    break;
                }
            }
            //Ver quien tiene la carta mas alta
            int BiggestCardIndex = 0;
            for(int j = 1; j < HighCardFromEachPlayer.Length; j++)
            {
                if(HighCardFromEachPlayer[BiggestCardIndex].powerLevel < HighCardFromEachPlayer[j].powerLevel)
                {
                    BiggestCardIndex = j;
                }
                else if(HighCardFromEachPlayer[BiggestCardIndex] == HighCardFromEachPlayer[j] && posibleCards[BiggestCardIndex] > posibleCards[j])
                {
                    BiggestCardIndex = j;
                }
            }
            return PlayersCanHave[BiggestCardIndex].Player;
        }
        return null;
    }
    Card GetHighestCardRemaining()
    {
        Card HighestCard = remainingCards[0];
        for(int i = 0; i < remainingCards.Count; i++)
        {
            if(remainingCards[i].powerLevel > HighestCard.powerLevel)
                HighestCard = remainingCards[i];
        }
        return HighestCard;
    }


    void HavingKingHighCard()
    {                 
        //Todo el lio de si tiene el rey
        if(infoOnHand[0].powerLevel == 6)
        {                       
            if(infoOnHand[1].powerLevel < infoOnHand[0].powerLevel)
            {
                ThrowRandomCard(infoOnHand[1]);
            }
            else if(infoOnHand[1].powerLevel == 7)
            {
                ThrowRandomCard(infoOnHand[1]);
            }
            else //Princesa
            {
                //Rip (Hace que la tire a alguien random o Suicidio)
                ThrowRandomCard(infoOnHand[0]);
            }
        }
        else
        {
            if(infoOnHand[0].powerLevel < infoOnHand[1].powerLevel)
            {
                ThrowRandomCard(infoOnHand[0]);
            }
            else if(infoOnHand[0].powerLevel == 7)
            {
                ThrowRandomCard(infoOnHand[0]);
            }
            else //Princesa
            {
                //Rip (Hace que la tire a alguien random o Suicidio)
                ThrowRandomCard(infoOnHand[1]);
            }
        }
    }
    void HavingKingLowCard()
    {
        Player Target = SearchHighestCard();
        if(infoOnHand[0].powerLevel == 6)
        {
            infoOnHand[0].Ability(Target);
        }
        else
        {
            infoOnHand[1].Ability(Target);
        }
    }
    void HavingPrinceLowCard()
    {
        Card HighestCard = GetHighestCardRemaining();
        int [] Probabilities = new int[PlayersCanHave.Length];
        for(int i = 0; i < PlayersCanHave.Length; i++)
        {
            Probabilities[i] = PlayersCanHave[i].ProbabilityHighestCard(HighestCard);
        }
        //Sacar la maxima
        int IndexMax = 0;
        for(int i = 1; i < Probabilities.Length; i++)
        {
            if(Probabilities[i] > Probabilities[IndexMax])
                IndexMax = i;
        }

        if(Probabilities[IndexMax] == 0)
        {
            //Autodescartarse
            if(infoOnHand[0].powerLevel == 5)
            {
                infoOnHand[0].Ability(this); //BORRAR LA CARTA ANTES DE TIRAR LA HABILIDAD
            }
            else if(infoOnHand[1].powerLevel == 5)
            {
                infoOnHand[1].Ability(this);
            }
        }
        else
        {
            if(Probabilities[IndexMax] >= 50)
            {
                //Descartar al jugador IndexMax
                if (infoOnHand[0].powerLevel == 5)
                {
                    infoOnHand[0].Ability(PlayersCanHave[IndexMax].Player);
                }
                else if (infoOnHand[1].powerLevel == 5)
                {
                    infoOnHand[1].Ability(PlayersCanHave[IndexMax].Player);
                }
            }
            else
            {
                //Autodescartarse
                if (infoOnHand[0].powerLevel == 5)
                {
                    infoOnHand[0].Ability(this); //BORRAR LA CARTA ANTES DE TIRAR LA HABILIDAD
                }
                else if (infoOnHand[0].powerLevel == 5)
                {
                    infoOnHand[1].Ability(this);
                }
            }
        }
    }
    void HavingGuardLowCard()
    {
        Card HighestCard = GetHighestCardRemaining();
        int [] Probabilities = new int[PlayersCanHave.Length];
        for(int i = 0; i < PlayersCanHave.Length; i++)
        {
            Probabilities[i] = PlayersCanHave[i].ProbabilityHighestCard(HighestCard);
        }
        //Sacar la maxima
        int IndexMax = 0;
        for(int i = 1; i < Probabilities.Length; i++)
        {
            if(Probabilities[i] > Probabilities[IndexMax])
                IndexMax = i;
        }

        //Acusar a IndexMax de tener Highest Card
        if(infoOnHand[0].powerLevel == 1)
        {
            infoOnHand[0].Ability(PlayersCanHave[IndexMax].Player, HighestCard);
        }
        else if(infoOnHand[1].powerLevel == 1)
        {
            infoOnHand[1].Ability(PlayersCanHave[IndexMax].Player, HighestCard);
        }
    }


    void Guard()
    {
        int Target = 0;
        //Fijarse que el target tenga una carta que no sea el guardia para acusar
        for(int i = 0; i < PlayersCanHave.Length; i++)
        {
            if(PlayersCanHave[i].cardNumber[1] || PlayersCanHave[i].cardNumber[2] || PlayersCanHave[i].cardNumber[3] || PlayersCanHave[i].cardNumber[4] || PlayersCanHave[i].cardNumber[5] || PlayersCanHave[i].cardNumber[6] || PlayersCanHave[i].cardNumber[7])
            {
                Target = i;
                break;
            }
        }
        //Convertir a Target en el que menos posibles cartas tenga
        for(int i = 0; i < PlayersCanHave.Length; i++)
        {
            if(PlayersCanHave[i].posibleCards < PlayersCanHave[Target].posibleCards)
            {
                if(PlayersCanHave[i].cardNumber[1] || PlayersCanHave[i].cardNumber[2] || PlayersCanHave[i].cardNumber[3] || PlayersCanHave[i].cardNumber[4] || PlayersCanHave[i].cardNumber[5] || PlayersCanHave[i].cardNumber[6] || PlayersCanHave[i].cardNumber[7])
                {
                    Target = i;
                }
            }
        }
       
        int TargetCard = Random.Range(1,7);
        while (!PlayersCanHave[Target].cardNumber[TargetCard])
        {
            TargetCard = Random.Range(1, 7);
        }
        //Tirar guardia a Target y decirle que tiene TargetCard
        if(infoOnHand[0].powerLevel == 1)
        {
            infoOnHand[0].Ability(PlayersCanHave[Target].Player, PlayersCanHave[Target].typeCards[TargetCard]);
        }
        else if(infoOnHand[1].powerLevel == 1)
        {
            infoOnHand[1].Ability(PlayersCanHave[Target].Player, PlayersCanHave[Target].typeCards[TargetCard]);
        }
    }
    bool MayKillBountyHunter()
    {
        //Sacar cual es la carta que no es el baron
        int MyIndex = 0;
        if(infoOnHand[0].powerLevel == 3 && infoOnHand[1].powerLevel == 3)
        {
            MyIndex = 1;
        }
        else
        {
            for(int i = 0; i < infoOnHand.Count; i++)
            {
                if(infoOnHand[i].powerLevel != 2) //BountyHunter Index
                {
                    MyIndex = i;
                }
            }
        }
        int [] Probabilidades = new int[PlayersCanHave.Length]; 
        for(int i = 0; i < PlayersCanHave.Length; i++)
        {
            Probabilidades[i] = PlayersCanHave[i].ProbabilityBountyHunterKilling(infoOnHand[MyIndex], remainingCards);
        }
        int MaxIndex = 0;
        for(int i = 0; i < Probabilidades.Length; i++)
        {
            if(Probabilidades[i] > Probabilidades[MaxIndex])
                MaxIndex = i;
        }
        if(Probabilidades[MaxIndex] >= 50)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    void BountyHunter()
    {
        //Sacar cual es la carta que no es el baron
        int MyIndex = 0;
        if(infoOnHand[0].powerLevel == 3 && infoOnHand[1].powerLevel == 3)
        {
            MyIndex = 1;
        }
        else
        {
            for(int i = 0; i < infoOnHand.Count; i++)
            {
                if(infoOnHand[i].powerLevel != 2) //BountyHunter Index
                {
                    MyIndex = i;
                }
            }
        }
        int [] Probabilidades = new int[PlayersCanHave.Length]; 
        for(int i = 0; i < PlayersCanHave.Length; i++)
        {
            Probabilidades[i] = PlayersCanHave[i].ProbabilityBountyHunterKilling(infoOnHand[MyIndex], remainingCards);
        }
        int MaxIndex = 0;
        for(int i = 0; i < Probabilidades.Length; i++)
        {
            if(Probabilidades[i] > Probabilidades[MaxIndex])
                MaxIndex = i;
        }
        //Comparar con PlayersCanHave[MaxIndex]
        infoOnHand[MyIndex].Ability(PlayersCanHave[MaxIndex].Player, this);
    }

    //Funciones de info de cartas que tiran otros
    void GuardThrown(CardNotepad AccusedPlayer, Card AccusedCard)
    {
        int Accused = PlayerIndex(AccusedPlayer);
        PlayersCanHave[Accused].cardNumber[AccusedCard.powerLevel - 1] = false;
    }
    void BaronThrown(CardNotepad RemainingPlayerNP, Card DeadCard)
    {
        int RemainingPlayer = PlayerIndex(RemainingPlayerNP);

        for(int i = 0; i < DeadCard.powerLevel; i++)
        {
            PlayersCanHave[RemainingPlayer].cardNumber[DeadCard.powerLevel - 1] = false;
        }
    }
    void PrinceThrown(CardNotepad TargetNP)
    {
        int Target = PlayerIndex(TargetNP);
        PlayersCanHave[Target].cardNumber[6] = false;
    }
    void KingThrown(CardNotepad TargetNP)
    {
        int Target = PlayerIndex(TargetNP);
        PlayersCanHave[Target].cardNumber[6] = false;
    }
    void CondesaThrown(CardNotepad TargetNP)
    {
        int Target = PlayerIndex(TargetNP);
        for(int i = 0; i < PlayersCanHave[Target].cardNumber.Length; i++)
        {
            if(i != 4 || i != 5 || i != 7)
            {
                PlayersCanHave[Target].cardNumber[i] = false;
            }
        }
    }

    int PlayerIndex(CardNotepad Target)
    {
        int Accused = 0;
        for(int i = 0; i < PlayersCanHave.Length; i++)
        {
            if(PlayersCanHave[i].Player == Target.Player)
                Accused = i;
        }
        return Accused;
    }


    void ThrowRandomCard(Card C)
    {
        int playerIndex = 0;
        int cardIndex = 0;
        switch(C.parameterType)
        {  
            case 0:         //Target
                playerIndex = Random.Range(0,PlayersCanHave.Length);
                while (PlayersCanHave[playerIndex].isDead)
                    playerIndex = Random.Range(0, PlayersCanHave.Length);
                C.Ability(PlayersCanHave[playerIndex].Player);
                break;
            case 1:         //Target & Myself
                playerIndex = Random.Range(0,PlayersCanHave.Length);
                while (PlayersCanHave[playerIndex].isDead)
                    playerIndex = Random.Range(0, PlayersCanHave.Length);
                C.Ability(PlayersCanHave[playerIndex].Player, this);
                break;
            case 2:         //Target & selectedCard
                playerIndex = Random.Range(0,PlayersCanHave.Length);
                while (PlayersCanHave[playerIndex].isDead)
                    playerIndex = Random.Range(0, PlayersCanHave.Length);


                // EVITAR LOOP SI PUEDE TENER UNICAMENTE UN GUARDIA //
                bool wontLoop = false;
                for(int i = 1; i < PlayersCanHave[playerIndex].cardNumber.Length; i++)
                {
                    if(PlayersCanHave[playerIndex].cardNumber[i])
                        wontLoop = true;
                }
                // LISTO //
                if(wontLoop)
                {
                    cardIndex = Random.Range(1, PlayersCanHave[playerIndex].cardNumber.Length);
                    while (!PlayersCanHave[playerIndex].cardNumber[cardIndex])
                        cardIndex = Random.Range(1, PlayersCanHave[playerIndex].cardNumber.Length);
                }
                else
                {
                    cardIndex = Random.Range(1, PlayersCanHave[playerIndex].cardNumber.Length);
                }
                C.Ability(PlayersCanHave[playerIndex].Player, PlayersCanHave[playerIndex].typeCards[cardIndex]);
                break;
            case 3:         //Target & Myself & Notepad
                playerIndex = Random.Range(0, PlayersCanHave.Length);
                while (PlayersCanHave[playerIndex].isDead)
                    playerIndex = Random.Range(0, PlayersCanHave.Length);
                C.Ability(PlayersCanHave[playerIndex].Player, this, PlayersCanHave);
                break;
        }
    }
    Player LeastKnowledge()
    {
        int MinPlayer = 0;
        for(int i = 0; i < PlayersCanHave.Length; i++)
        {
            if(PlayersCanHave[i].posibleCards < MinPlayer)
            {
                MinPlayer = i;
            }
        }
        return PlayersCanHave[MinPlayer].Player;
    }



}