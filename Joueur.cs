using System;
using System.Collections.Generic;
using System.Text;

namespace TP1
{
    //Classe permettant de créer des personnes avec un nom et un prénom
    public class Personne
    {

        //variable d'instance
        protected string varNom;
        protected string varPrenom;

        //Surdefinition de constructeur 
        public Personne()
        {

        }
        public Personne(string nom, string pre)
        {
            varNom = nom;
            varPrenom = pre;
        }

        //Accesseurs
        public string nom
        {
            get => varNom;
        }

        public string prenom
        {
            get => varPrenom;
        }
    }

    //La classe joueur hérite de la classe personne avec une liste de cartes (sa main en jeu)
    public class Joueur : Personne
    {// var d'instance 
        private Liste malist;

        //surdefinition consructeur 
        public Joueur()
        {
        }
        public Joueur(string nom, string prenom, Liste t) : base(nom, prenom)
        {
            malist = t;
        }

        //Accesseur et mutateur
        public Liste maListe
        {
            get => malist;
            set => malist = value;
        }
    }
}
