using System;
using System.Collections.Generic;
using System.Text;
using TP1.interfaces;
using TP1.interfaces;

namespace TP1
{
    //Classe définissant des listes de Carte et la méthode pour les afficher tel que déclarer dans l'interface InterListe
    public class Liste : InterListe
    {
        protected List<Carte> ensCartes = new List<Carte>();

        public List<Carte> list_Cartes
        {
            get => ensCartes;
            set => ensCartes = value;
        }

        public void AfficheListeCarte()
        {
            foreach (var carte in ensCartes)
            {
                Console.Write("La valeur =>|" + carte.valeurs + " | \t La couleur =>|" + carte.couleurs + " |\n");
            }
        }
    }
}
