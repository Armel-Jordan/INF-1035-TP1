/* Le programme qui suit est l'implémentation du jeu de carte de pèche. Il s'agit du TP1 pour le cours INF-1035 à l'UQTR
  * Automne 2020
  * Les auteurs sont:
  * Jonathan Kanyinda Muamba (KANJ88060000)
  * Jordan Armel Kuibia (KUIA73040101)
  * Maxime Déry (DERM12028401)
  * Date : 08/10/2020
  */

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic.CompilerServices;
using static TP1.Enume;
namespace TP1
{
    class Program
    {
        static void Main(string[] args)
        {
            Partie partie = new Partie();
            int nbJoueurs=0;
            bool boucle = true;

            Console.WriteLine("Bienvenue au jeu de la Pèche");

            //Le jeu peut être joué de 2 à 4 joueurs
            //Mandat = 3 joueurs par défaut
            do
            {
                Console.WriteLine("Veuillez entrer le nombre de joueurs (de 2 à 4).");
                var entree = Console.ReadLine();

                //Gestion de l'exception
                if (!int.TryParse(entree, out int number))
                    Console.WriteLine("Entrée invalide");

                else
                {
                    boucle = false;
                    nbJoueurs = Convert.ToInt32(entree);
                }
            }
            while (nbJoueurs < 2 || nbJoueurs > 4 || boucle);

            //Méthode appelé pour créer les joueurs selon le nombre sélectionné
            partie.chargerJoueurs(nbJoueurs);

            //Méthode qui démarre la partie
            partie.demarrer();
            Console.ReadLine();
        }
    }
}