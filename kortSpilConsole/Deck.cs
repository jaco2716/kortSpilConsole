﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kortSpilConsole
{
    class Deck
    {
        private List<Card> cards = new List<Card>();
        public List<Card> cardsRevealed = new List<Card>();
        private UnoGame game;

        public Deck(UnoGame game)
        {
            this.game = game;
            for (int i = 0; i < 10; i++)
            {
                // red cards
                cards.Add(new Card("red", ""+i));
                cards.Add(new Card("red", ""+i));
                //blue cards
                cards.Add(new Card("blue", ""+i));
                cards.Add(new Card("blue", ""+i));
                //green cards
                cards.Add(new Card("green", ""+i));
                cards.Add(new Card("green", ""+i));
                //yellow cards
                cards.Add(new Card("yellow", ""+i));
                cards.Add(new Card("yellow", ""+i));

            }
            //sorte kort
            for (int i = 0; i < 4; i++)
            {
                cards.Add(new Card("black", "+4"));
                cards.Add(new Card("black", "switch color"));
            }
            //reverse og skip og plus 2
            for (int i = 0; i < 2; i++)
            {
                cards.Add(new Card("red", "reverse"));
                cards.Add(new Card("blue", "reverse"));
                cards.Add(new Card("green", "reverse"));
                cards.Add(new Card("yellow", "reverse"));
                cards.Add(new Card("red", "skip"));
                cards.Add(new Card("blue", "skip"));
                cards.Add(new Card("green", "skip"));
                cards.Add(new Card("yellow", "skip"));
                cards.Add(new Card("red", "+2"));
                cards.Add(new Card("blue", "+2"));
                cards.Add(new Card("green", "+2"));
                cards.Add(new Card("yellow", "+2"));
            }

            Shuffle();

            //move first card to revealed cards
            cardsRevealed.Add(Draw());
        }
        
        public Card Draw()
        {
            Card c = cards[0]; //finder øverste kort
            cards.Remove(c); //fjerner kort fra bunken (c = øverste kort)
            return c; //giver kortet til den der kalder metoden
        }

        public Card DebugDraw(string Color, String Value)
        {
            for (int i = 0; i < cards.Count; i++)
            {
                if (cards[i].Color == Color && cards[i].Value == Value)
                {
                    Card c = cards[i];
                    cards.Remove(c);
                    return c;
                }
            }
            return null;
        }

        public void Shuffle()
        {
            // shuffle array
            Random random = new Random();
            cards = cards.OrderBy(x => random.Next()).ToList();
        }

        public bool playCard(Card card)
        {
            if (Peek().Color == card.Color || Peek().Value == card.Value || card.Color == "black")
            {
                game.currentPlayer.Hand.Remove(card);
                cardsRevealed.Add(card);
                
                if (Peek().Value == "skip"){ game.nextPlayer(); }

                else if (Peek().Value == "reverse") { game.players.Reverse(); }
                else if (Peek().Color == "black")
                {
                    Console.WriteLine("Which color would you like");
                    Console.WriteLine("1=red 2=blue 3=green 4=yellow");
                    string colorPick = Console.ReadLine();
                    if (colorPick == "1") {colorPick = "red";}
                    else if (colorPick == "2") { colorPick = "blue"; }
                    else if (colorPick == "3") { colorPick = "green"; }
                    else if (colorPick == "4") { colorPick = "yellow"; }
                    Peek().Color = colorPick;
                }
                
                if (Peek().Value == "+4")
                {
                    game.players[game.players.IndexOf(game.currentPlayer) + 1].DrawCard(4);
                }
                else if (Peek().Value == "+2")
                {
                    game.players[game.players.IndexOf(game.currentPlayer) + 1].DrawCard(2);
                }
                return true;
            }


            else
            {
                game.currentPlayer.DrawCard();
                return false;
            }
        }

        

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < cards.Count; i++)
            {
                sb.Append("[");
                sb.Append(cards[i]);
                sb.Append("], ");
            }

            return sb.ToString();
        }

        public Card Peek()
        {
            
            return cardsRevealed.Last();
        }
    }
}
