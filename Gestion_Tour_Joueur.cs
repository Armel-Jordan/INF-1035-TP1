using System;
using System.Collections.Generic;
using TP1.interfaces;

namespace TP1
{
    //Classe de gestion pour choisir le joueur actif
    public class Gestion_Tour_Joueur
    {
        public delegate void peutCommencerCallback(Partie partie);
        public event peutCommencerCallback eventPJ;

        //Sélection du premier joueur
        public int choisirPremierJoueur(Partie partie)
        {
            int nombreDejoueur = partie.listeJoueurs.Count;

            //Idée de choisir de façon aléatoire
            Random random = new Random();
            int randomNumber;
            randomNumber = random.Next(0, nombreDejoueur);
            partie.ProchainAJouer = randomNumber;

            //On signale que la partie peut continuer
            eventPJ(partie);

            return randomNumber;
        }

        //Sélection du joueur suivant. Il prend comme paramètre une partie avec sa liste de joueurs.
        public void selectionJoueurSuivant(Partie partie)
        {
            //Si on arrive au bout de la liste de joueurs, on recommence au début
            if (partie.ProchainAJouer + 1 == partie.listeJoueurs.Count)
            {
                partie.ProchainAJouer = 0;
            }

            //On passe au joueur suivant
            else
            {
                partie.ProchainAJouer += 1;
            }

            //On signale à la partie qu'elle peut continuer
            eventPJ(partie);
        }
    }
}