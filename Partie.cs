using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace TP1
{
    //Classe définissant les structures et les mécanismes d'une partie
    public class Partie
    {
        //var istance
        private List<Joueur> listeJoueurses = new List<Joueur>();
        private Pioche pilePioche = new Pioche();
        private Depot pileDepot = new Depot();

        private Manipulation_liste_Carte gesCarte = new Manipulation_liste_Carte();
        private Gestion_Tour_Joueur gestTour = new Gestion_Tour_Joueur();

        private int prochainAJouer;

        //Accesseurs et mutateurs
        public List<Joueur> listeJoueurs
        {
            get => listeJoueurses;
            set => listeJoueurses = value;
        }

        public Pioche PilePioche
        {
            get => pilePioche;
            set => pilePioche = value;
        }

        public Depot PileDepot
        {
            get => pileDepot;
            set => pileDepot = value;
        }

        public int ProchainAJouer
        {
            get => prochainAJouer;
            set => prochainAJouer = value;
        }

        //Méthode pour créer les joueurs et les ajouter à la liste de joueur
        public void chargerJoueurs(int nombreDeJoueur)
        {
            string nom, prenom;
            for (int i = 1; i <= nombreDeJoueur; i++)
            {
                //Liste de cartes
                Liste mainJ = new Liste();

                //Identification des joueurs
                do
                {
                    Console.WriteLine("Entrer le nom du joueur " + i);
                    nom = Console.ReadLine();
                }
                while (string.IsNullOrEmpty(nom));

                do
                {
                    Console.WriteLine("Entrer le prenom du joueur " + i);
                    prenom = Console.ReadLine();
                }
                while (string.IsNullOrEmpty(prenom));

                Joueur jouer = new Joueur(nom, prenom, mainJ);
                this.listeJoueurses.Add(jouer);
            }
        }

        //Méthode pour démarrer la partie
        public void demarrer()
        {
            //Création des event handler pour suivre le joueur actif
            Manipulation_liste_Carte.choixPremierJoueurEventhandler choixFp = new Manipulation_liste_Carte.choixPremierJoueurEventhandler(this.gestTour.choisirPremierJoueur);
            Manipulation_liste_Carte.choixJoueurSuivantEventhandler choixNp = new Manipulation_liste_Carte.choixJoueurSuivantEventhandler(this.gestTour.selectionJoueurSuivant);
            gesCarte.eventFP += choixFp;
            gesCarte.eventNP += choixNp;

            //Création du délégué
            Gestion_Tour_Joueur.peutCommencerCallback starGame = new Gestion_Tour_Joueur.peutCommencerCallback(this.gesCarte.jeuJoueur);
            gestTour.eventPJ += starGame;

            prochainAJouer = gesCarte.distribion_Aleatoire_(this);
        }
    }
}