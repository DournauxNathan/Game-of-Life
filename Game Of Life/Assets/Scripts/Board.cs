using System.Text.RegularExpressions;
using UnityEngine;

namespace JeuDeLaVie.Scripts
{
    /// <summary>
    /// Un objet "Cell", une cellule, n'a que deux statuts possibles : vivant ou mort
    /// </summary>
    [System.Serializable]
    public struct Cell
    {
        public bool alive;
    }

    /// <summary>
    /// Structure de données d'un plateau
    /// Cette classe permet de gérer un plateau de jeu, grille de cellules en deux dimensions
    /// </summary>
    [System.Serializable]
    public class Board
    {
        public int width;
        public int height;

        public Cell[,] cells;

        #region Constructors

        /// <summary>
        /// Constructeur : initialise un tableau de cellules mortes
        /// </summary>
        /// <param name="w">Largeur</param>
        /// <param name="h">Hauteur</param>
        public Board(int w, int h)
        {
            // EXERCICE : écrire le code du constructeur
            // Mettre à jour les attributs de la classe en fonction des paramètres du constructeur
            // Initialiser le tableau à deux dimensions de taille : (width, height)
            // toutes les valeurs du tableau doivent être des cellules mortes

            width = w;
            height = h;

            cells = new Cell[w, h];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    cells[x, y].alive = false;
                }
            }            

            /*Debug.LogWarning("Board(w,h) : CONSTRUCTEUR A IMPLEMENTER");
            throw new System.Exception("Impossible d'instancier le Board, la méthode n'est pas implémentée");*/

            // FIN EXERCICE
        }

        /// <summary>
        /// Initialise un tableau de cellules aléatoirement
        /// </summary>
        /// <param name="w">Largeur</param>
        /// <param name="h">Hauteur</param>
        /// <param name="aliveRatio">Ratio de cellules vivantes sur le plateau</param>
        public Board(int w, int h, float aliveRatio)
        {
            // EXERCICE : écrire le code du constructeur
            // Mettre à jour les attributs de la classe en fonction des paramètres du constructeur
            // Initialiser le tableau à deux dimensions de taille : (width, height)
            // Le nombre de cellules vivantes doit, plus ou moins, correspondre au ratio donné en paramètre
            // Le ratio est une valeur flottante (nombre à virgule) entre 0 et 1 :
            // - pour un ratio = 0, toutes les cellules du plateau sont mortes
            // - pour un ratio = 1, toutes les cellules du plateau sont vivantes
            // Utiliser une valeur aléatoire pour déterminer l'état de chaque cellule

            width = w;
            height = h;

            cells = new Cell[w, h];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    float ratio = Random.Range(0f, 1f);

                    if (ratio > aliveRatio)
                    {
                        cells[x, y].alive = false;
                    }
                    else
                    {
                        cells[x, y].alive = true;
                    }
                }
            }


            /*Debug.LogWarning("Board(w,h,ratio) : CONSTRUCTEUR A IMPLEMENTER");
            throw new System.Exception("Impossible d'instancier le Board, la méthode n'est pas implémentée");*/
            // FIN EXERCICE
        }

        #endregion

        #region Access Cells

        /// <summary>
        /// Renvoie le statut de la cellule se trouvant aux coordonnées [x,y]
        /// </summary>
        /// <param name="x">Coordonnée</param>
        /// <param name="y">Coordonnée</param>
        /// <returns>true si la cellule est vivante, false si elle est morte</returns>
        public bool GetCell(int x, int y)
        {
            // EXERCICE : Ecrire la ligne de code qui renvoie le statut de la cellule
            // Ajoutez une vérification : x et y ne doivent pas sortir des limites du tableau !

            if (x <= width && y <= height)
            {
                return cells[x, y].alive;
            }

            //Debug.LogWarning("GetCell(x,y) : METHODE A IMPLEMENTER");
            return false;

            // FIN EXERCICE
        }
        /// <summary>
        /// Modifie le statut de la cellule se trouvant aux coordonnées [x,y]
        /// </summary>
        /// <param name="x">Coordonnée</param>
        /// <param name="y">Coordonnée</param>
        /// <param name="alive">Nouveau statut</param>
        public void SetCell(int x, int y, bool alive)
        {
            // EXERCICE : Ecrire la ligne de code qui modifie le statut de la cellule
            // Ajoutez une vérification : x et y ne doivent pas sortir des limites du tableau !

            //Debug.LogWarning("SetCell(x,y,alive) : METHODE A IMPLEMENTER");

            if (x <= width && y <= height)
            {
                if (cells[x, y].alive)
                {                   
                    alive = false;
                }
            }

            // FIN EXERCICE
        }

        #endregion

        #region Simulation

        /// <summary>
        /// Cette méthode renvoie le nombre de cellules voisines vivantes d'une cellule données
        /// </summary>
        /// <param name="x">Coordonnée</param>
        /// <param name="y">Coordonnée</param>
        /// <returns>Un nombre entre 0 et 8. 0 si toutes les cellules voisines sont mortes, 8 si elles sont toutes vivantes.</returns>
        private int GetNeighborsCountForCell(int x, int y)
        {
            int neighborsCount = 0;

            // EXERCICE : Ecrire le contenu de cette méthode
            // Cette méthode doit renvoyer une valeur correspondant au nombre de cellules voisines vivantes
            // autour des coordonnées (x,y) données en paramètre
            // Indice : Il y a 8 cellules voisines en général, un peu moins sur les bords du plateau

                //Haut de la cellule actuel
                if (y + 1 < height)
                {
                    if (cells[x, y + 1].alive)
                    {
                        neighborsCount++;
                    }
                }

                //Droite de la cellule actuel
                if (x + 1 < width)
                {
                    if (cells[x + 1, y].alive)
                    {
                        neighborsCount++;
                    }
                }

                //Bas de la cellule actuel
                if (y - 1 >= 0)
                {
                    if (cells[x, y - 1].alive)
                    {
                        neighborsCount++;
                    }
                }

                //Gauche de la cellule actuelle
                if (x - 1 >= 0)
                {
                    if (cells[x - 1, y].alive)
                    {
                        neighborsCount++;
                    }
                }

                //Haut + Droite de la cellule actuelle
                if (x + 1 < width && y + 1 < height)
                {
                    if (cells[x + 1, y + 1].alive)
                    {
                        neighborsCount++;
                    }
                }
                    
                //Haut + Gauche de la cellule actuelle
                if (x - 1 >= 0 && y + 1 < height)
                {
                    if (cells[x - 1, y + 1].alive)
                    {
                        neighborsCount++;
                    }
                }
                
                //Bas + Droite de la cellule actuelle
                if (x + 1 < width && y - 1 >= 0)
                {
                    if (cells[x + 1, y - 1].alive)
                    {
                        neighborsCount++;
                    }
                }
                
                //Bas + Gauche de la cellule actuelle
                if (x - 1 >= 0 && y - 1 >= 0)
                {
                    if (cells[x - 1, y - 1].alive)
                    {
                        neighborsCount++;
                    }
                }


            return neighborsCount;

            //Debug.LogWarning("GetNeighborsCountForCell(x,y) : METHODE A IMPLEMENTER");

            // FIN EXERCICE


        }

        /// <summary>
        /// Cette méthode modifie le tableau de cellules selon les règles du jeu de la vie de John Conway.
        /// https://fr.wikipedia.org/wiki/Jeu_de_la_vie
        /// </summary>
        public void NextGeneration()
        {
            // EXERCICE : Ecrire le contenu de cette méthode
            // Cette méthode modifie le tableau en utilisant les règles du jeu de la vie :
            // - si une cellule est entourée de 3 cellules vivantes exactement, elle devient vivante
            // - si une cellule est entourée de 2 cellules vivantes exactement, elle ne change pas d'état
            // - dans tous les autres cas, elle devient morte
            // Indice : vous devez utiliser un tableau intermédiaire et le parcourir cellule par cellule
            // Indice 2 : utilisez la méthode GetNeighborsCountForCell(x,y)

            Cell[,] newGen = new Cell[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    newGen[x, y].alive = GetCell(x, y);

                    if (cells[x,y].alive)
                    {
                        if (GetNeighborsCountForCell(x, y) != 3 && GetNeighborsCountForCell(x, y) != 2)
                        {
                            newGen[x, y].alive = false;
                        }
                    }
                    else
                    {
                        if (GetNeighborsCountForCell(x, y) == 3)
                        {
                            newGen[x, y].alive = true;
                        }
                    }

                }
            }

            cells = newGen;

            //Debug.LogWarning("NextGeneration() : METHODE A IMPLEMENTER");

            // FIN EXERCICE
        }

        #endregion

        #region Save/Load

        /// <summary>
        /// Cette méthode retourne une chaine de caractères au format JSON qui décrit le plateau
        /// Unity ne sérialise pas correctement les tableaux à deux dimensions, c'est pour ça que j'ai dû sérialiser à la main
        /// </summary>
        /// <returns>Une chaine de caractères au format JSON</returns>
        public string ToJSON()
        {
            string jsonContent = "{\"width\":" + width + ",\"height\":" + height + ",";

            jsonContent += "\"cells\":[";
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    jsonContent += GetCell(x, y) ? "1" : "0";
                }
            }
            jsonContent += "]}";

            return jsonContent;
        }

        /// <summary>
        /// Cette méthode crée un tableau à partir d'une chaine de caractères au format JSON
        /// Unity ne sérialise pas correctement les tableaux à deux dimensions, c'est pour ça que j'ai dû faire ça à la main
        /// </summary>
        /// <param name="json">Une chaine de caractères au format JSON</param>
        /// <returns>Un objet Board</returns>
        public static Board FromJSON(string json)
        {
            char[] delimiterChars = { ' ', ':', ',', '{', '}', '[', ']', '\"' };
            string[] words = json.Split(delimiterChars, System.StringSplitOptions.RemoveEmptyEntries);

            // On cherche l'entrée "width:___" dans la chaine de caractères, puis on parse sa valeur pour obtenir un entier
            Match matchWidth = Regex.Match(json, "\"width\":[0-9]*");
            int widthValue = int.Parse(matchWidth.Value.Substring(8));

            // On cherche l'entrée "height:___" dans la chaine de caractères, puis on parse sa valeur pour obtenir un entier
            Match matchHeight = Regex.Match(json, "\"height\":[0-9]*");
            int heightValue = int.Parse(matchHeight.Value.Substring(9));

            Board board = new Board(widthValue, heightValue);

            // On cherche l'entrée "cells:___" dans la chaine de caractères
            Match matchCells = Regex.Match(json, "\"cells\":" + Regex.Escape("[") + "[01]*" + Regex.Escape("]"));
            string allCellsValues = matchCells.Value.Substring(9, widthValue * heightValue);

            int x = 0;
            int y = 0;
            foreach (char value in allCellsValues)
            {
                // Pour chaque caractère, on met à jour l'état de la cellule correspondante
                bool alive = value.Equals('1');
                board.SetCell(x, y, alive);
                x++;
                if (x >= widthValue)
                {
                    x = 0;
                    y++;
                }
            }

            return board;
        }

        #endregion
    }

}
