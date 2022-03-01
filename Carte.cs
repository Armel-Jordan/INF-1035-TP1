using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.VisualBasic.CompilerServices;
using static TP1.Enume;

namespace TP1
{
    //La classe carte pour créer les cartes dans le jeu
    public class Carte
    {
        // var d'instance 
        private Couleur couleur;
        private Valeur valeur;
        private String nom;

        //constructeur 
        public Carte(Couleur c, Valeur v)
        {
            couleur = c;
            valeur = v;
            nom = "" + c + v;
        }
        public Carte()
        {
        }

        //Redéfinition
        public String toString()
        {
            return "" + couleur + "\t" + valeur;
        }

        //Accesseur mutateur 
        public Valeur valeurs

        {
            get => valeur;
            set => valeur = value;
        }

        public Couleur couleurs
        {
            get => couleur;
            set => couleur = value;
        }
    }
}
