using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace kortSpilConsole
{
    class UnoGame
    {
        public Deck deck;
        public List<Player> players = new List<Player>();
        private List<string> names = new List<string>();
        public Player currentPlayer;
        private bool gameover = false;
        private int amount;
        private string name;


        public UnoGame()
        {
            deck = new Deck(this);
            Console.WriteLine("How many players?");
            amount = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < amount; i++)
            {
                Console.WriteLine("Player "+(i+1)+" Name");
                name = Console.ReadLine();
                names.Add(name);
            }
            for (int i = 0; i < amount; i++)
            {
                players.Add(new Player(names[i], this));
            }

            
            

            currentPlayer = players.First();
            //del kort ud til spiller 1
            players[0].DrawCard(7);
            //players[0].DebugDrawCard("red", "reverse");
            
            //del 7 kort ud til resten af spillerne
            for (int i = 1; i < players.Count; i++)
            {
                players[i].DrawCard(7);
            }

            while (gameover != true)
            {
                // vis vores 'revealed' card
                if (deck.Peek().Color == "black")
                {
                    while (deck.Peek().Color == "black")
                    {
                        deck.cardsRevealed.Add(deck.Draw());
                    }
                }
                
                Console.WriteLine(deck.Peek());

                // print player1 med tostring-metoden (navn: g2, b3, r7....)
                Console.WriteLine(currentPlayer);

                // spørg spiller1 om hvilket kort han vil ligge ned
                Console.WriteLine("Vælg et kort!");
                int i = Convert.ToInt32(Console.ReadLine());

                //TODO prøv at 'spille' det valgte kort til bunken

                deck.playCard(currentPlayer.Hand[i - 1]);

                if (currentPlayer.Hand.Count == 0)
                {
                    Console.WriteLine("The winner is "+ currentPlayer);
                    gameover = true;
                }

                nextPlayer();

            }
        }

        

        public void nextPlayer()
        {
            if (currentPlayer == players.Last())
            {
                currentPlayer = players.First();
            }
            else
            {
                int currentPlayerPosition = players.IndexOf(currentPlayer);
                currentPlayer = players[currentPlayerPosition + 1];
            }
        }
    }
}
