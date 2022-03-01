using System;
using System.Collections.Generic;
using System.Linq;
using TP1.interfaces;
using static TP1.Enume;

namespace TP1
{
    //Classe de gestion des cartes en jeu, initiale et subséquente
    public class Manipulation_liste_Carte
    {
        //VAR de l'instance fait de Delegue, evenement et liste     
        public delegate int choixPremierJoueurEventhandler(Partie partie);
        public delegate void choixJoueurSuivantEventhandler(Partie partie);
        public event choixPremierJoueurEventhandler eventFP;
        public event choixJoueurSuivantEventhandler eventNP;
        static List<Carte> liste_de_depart = new List<Carte>();

        //Méthode pour créer les 52 cartes de jeu et distribuer les 8 de départ aux joueurs. Elle initie le jeu
        public int distribion_Aleatoire_(Partie partie)
        {
            // Generation des 52 cartes 
            foreach (var item1 in Enum.GetValues(typeof(Couleur)))//parcourt chaque valeur de l'enum Couleur
            {
                foreach (var item2 in Enum.GetValues(typeof(Valeur)))//parcourt chaque valeur de l'enum Valeur 
                {
                    liste_de_depart.Add(new Carte((Couleur)item1, (Valeur)item2));
                }
            }

            Random random = new Random();
            int nbreHasard;
            int max = 52;

            //Distribution aleatoirement au joueur 
            foreach (var joueur in partie.listeJoueurs)
            {
                for (int i = 0; i < 8; i++)
                {
                    nbreHasard = random.Next(0, max);

                    if (liste_de_depart[nbreHasard] != null)
                    {
                        joueur.maListe.list_Cartes.Add(liste_de_depart[nbreHasard]);
                        liste_de_depart.Remove(liste_de_depart[nbreHasard]);
                        max -= 1;
                    }

                }
            }
            nbreHasard = random.Next(0, max);

            if (liste_de_depart[nbreHasard] != null)//transfert de carte 
            {
                partie.PileDepot.list_Cartes.Add(liste_de_depart[nbreHasard]);
                liste_de_depart.Remove(liste_de_depart[nbreHasard]);
            }

            foreach (var carte in liste_de_depart)
            {
                partie.PilePioche.list_Cartes.Add(carte);
            }

            liste_de_depart.Clear();


            //Le premier joueur est sélectionné et le jeu peut continuer
            int player = eventFP(partie);

            return player;
        }

        //La méthode qui suit définit la mécanique du principale du jeu après le tour initial
        public void jeuJoueur(Partie partie)
        {
            //variable pour stocker le joueur à jouer
            Joueur joueur = new Joueur();
            joueur = partie.listeJoueurs[partie.ProchainAJouer];

            //Affichage des étapes de jeu
            Console.WriteLine("Nombre de carte sur la pile de pioche :" + partie.PilePioche.list_Cartes.Count);
            Console.WriteLine("Cartes pile depot :");
            partie.PileDepot.AfficheListeCarte();
            Console.WriteLine("----------------------------------------------------\n");
            Console.WriteLine("----------------------------------------------------");

            //Pause au niveau de l'affichage pour suivre le déroulement
            System.Threading.Thread.Sleep(5000);
            Console.WriteLine("\n|| " + joueur.prenom + " " + joueur.nom + "|| A vous de jouer. \n Votre Liste de carte :");
            joueur.maListe.AfficheListeCarte();
            Console.WriteLine("");

            //Création d'une liste pour stocker les options possibles de jeu versus l'arène (dernière carte de la liste dépôt)
            List<Carte> Liste_cartes_Possible = possibliliteDeJeux(partie);

            int choix = 0;

            Console.WriteLine("Liste de cartes valables a jouer:");

            //Affichage des options
            foreach (var cat in Liste_cartes_Possible)
            {
                choix += 1;
                Console.WriteLine("Choix :" + choix + "La valeur =>|" + cat.valeurs + " | \t La couleur =>|" + cat.couleurs + " | \n");
            }

            //Choix de la carte à joueur par défaut ou aléatoirement dans la liste possible
            if (Liste_cartes_Possible.Count == 0)//rammasser 
            {
                ramasse_Carte(partie, joueur);
            }
            else

            {//choix aleatoire carte 
                Random random = new Random();
                int randomNumber;
                int max = Liste_cartes_Possible.Count - 1;
                randomNumber = random.Next(0, max);

                Carte cartJouer = Liste_cartes_Possible[randomNumber];
                partie.PileDepot.list_Cartes.Add(cartJouer);
                joueur.maListe.list_Cartes.Remove(cartJouer);

                Console.WriteLine("\nCarte jouer: " + cartJouer.couleurs + " " +
                cartJouer.valeurs);
            }

            //Affichage des cartes jouées
            Console.WriteLine("Liste des cartes du joueur apres avoir joué : ");
            joueur.maListe.AfficheListeCarte();

            Console.WriteLine("\n***********************************************************************");
            Console.WriteLine("\t\t\t FIN DE TOUR DE :|" +joueur.prenom+" "+ joueur.nom + "|");
            Console.WriteLine("\n***********************************************************************");

            //On détermine si la partie doit continuer
            if (!gererCasSpecial(partie))
            {
                //Il y a un gagnant
                if (joueur.maListe.list_Cartes.Count == 0)
                {
                    Console.WriteLine(joueur.prenom + " " + joueur.nom + " a gagné :)");
                }

                //Il n'y a pas de gagnant, on signale à la partie de continuer
                else
                {
                    eventNP(partie);
                }

            }

            //Cas d'une partie sans gagnant
            else
            {
                Console.WriteLine("\npartie terminée Bye ********");

            }
        }

        //Méthode pour déterminer les cartes possibles à jouer
        public List<Carte> possibliliteDeJeux(Partie partie)
        {
            Depot p = partie.PileDepot;
            List<Carte> cartesPossibleAjouer = new List<Carte>();
            int lastindex = p.list_Cartes.Count - 1;
            Carte carteAuDessusDeLapile = p.list_Cartes[lastindex];

            Joueur joueur = partie.listeJoueurs[partie.ProchainAJouer];

            //On compare les carte selon leur couleurs et leurs valeur puis on retourne la liste de sélection
            foreach (var cart in partie.listeJoueurs[partie.ProchainAJouer].maListe.list_Cartes)
            {
                if (cart.couleurs.Equals(carteAuDessusDeLapile.couleurs) ||
                    cart.valeurs.Equals(carteAuDessusDeLapile.valeurs))
                {
                    cartesPossibleAjouer.Add(cart);
                }
            }
            return (List<Carte>)cartesPossibleAjouer;
        }

        //Méthode pour qu'un joueur prenne une carte dans le paquet pioche et la mette dans sa main
        public void ramasse_Carte(Partie partie, Joueur joueur)
        {
            //Si le paquet pioche est trop faible, on le recharge depuis le dépôt
            if (partie.PilePioche.list_Cartes.Count < 5)
            {
                rechargerPP(partie);
            }

            int idCartPP = partie.PilePioche.list_Cartes.Count - 1;
            Carte carteAprendre = partie.PilePioche.list_Cartes[idCartPP];

            //On ajoute une carte au joueur
            joueur.maListe.list_Cartes.Add(carteAprendre);

            //On enlève une carte depuis la pioche
            partie.PilePioche.list_Cartes.Remove(carteAprendre);
            Console.WriteLine(joueur.nom + " Ramasse une carte \n");

        }

        //Méthode pour recharger la pioche depuis le dépôt
        public void rechargerPP(Partie partie)
        {
            if (!gererCasSpecial(partie))
            {
                List<Carte> list = new List<Carte>();
                Carte carte = new Carte();
                int valmax = partie.PileDepot.list_Cartes.Count;

                //On laisse la dernière carte du dépôt dans le dépôt puisqu'elle représente l'arène
                carte = partie.PileDepot.list_Cartes[valmax - 1];


                partie.PileDepot.list_Cartes.Remove(carte);

                //On mélange ajoute les cartes restante de dépôt et de la pioche dans une liste temporaire
                foreach (var cart in partie.PileDepot.list_Cartes)
                {
                    list.Add(cart);
                }

                foreach (var cart in partie.PilePioche.list_Cartes)
                {
                    list.Add(cart);
                }

                partie.PilePioche.list_Cartes.Clear();

                //La nouvelle pioche est crée de la liste temporaire, soit le mélange de dépôt et de pioche sauf une carte, l'arène
                partie.PilePioche.list_Cartes = list;

                partie.PileDepot.list_Cartes.Clear();

                //L'arène est recréée depuis la carte retenue
                partie.PileDepot.list_Cartes.Add(carte);
            }
        }

        //Méthode pour gérer le cas où les cartes de la pioche sont distribuées dans les mains des joueurs et le dépôt est quasiment vide. Partie sans gagnant
        public bool gererCasSpecial(Partie partie)
        {
            if (partie.PileDepot.list_Cartes.Count < 5 && partie.PilePioche.list_Cartes.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}