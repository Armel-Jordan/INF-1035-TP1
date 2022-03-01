using System;
using System.Collections.Generic;
using System.Text;
using TP1.interfaces;

namespace TP1
{
    //La classe dépot permet de créer le paquet dépot dans le jeu
    //Elle hérite de la classe liste et de l'interface InterDepot
    public class Depot : Liste, InterDepot
    {
        //Constructeur
        public Depot()
        {
        }

        public Depot(List<Carte> cartes)
        {
            ensCartes.Clear();
            this.ensCartes = cartes;
        }

        //Méthode pour ajouter des cartes à un ensemble de cartes donné
        public void DeposerCarte(Carte carte)
        {
            ensCartes.Add(carte);
        }

    }

    //La classe dépot permet de créer le paquet pioche dans le jeu
    //Elle hérite de la classe liste et de l'interface InterPioche
    public class Pioche : Liste, InterPioche
    {
        public Pioche()
        {
        }

        public Pioche(List<Carte> cartes)
        {
            ensCartes.Clear();
            this.ensCartes = cartes;
        }

        //Méthode pour ramasser les cartes d'un joueur donné.
        public void rammasserCarte(Joueur joueur)
        {
            int lastCarte = ensCartes.Count;
            lastCarte -= lastCarte;
            joueur.maListe.list_Cartes.Add(ensCartes[lastCarte]);
        }

    }
}
