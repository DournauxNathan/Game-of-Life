using JeuDeLaVie.Scripts;
using System.Collections;
using UnityEngine;

/// <summary>
/// Gère l'état et l'affichage du plateau
/// </summary>
public class BoardManager : MonoBehaviour
{
    public static BoardManager instance;

    [Header("Prefab - cellule")]
    public GameObject cellPrefab;

    // Modèle de données, stocke l'état du plateau
    private Board board;

    // Tableau à deux dimensions contenant les GameObjects "Cellule" dans la scène
    private GameObject[,] allCells;

    // Le plateau "Board"
    private GameObject boardGameObject;

    // Booléen indiquant si la simulation tourne (lecture) ou non
    private bool simulationIsPlaying;
    // Valeur en secondes entre deux générations, quand la simulation tourne
    private float simulationDelayBetweenGenerations;

    public void DestroyCells()
    {
        allCells = null;

        // Cette boucle parcourt tous les "children" du plateau
        // chaque child correspond à une cellule du plateau
        // Cette boucle détruit chaque cellule une à une, jusqu'à ce que le plateau soit vide
        foreach (Transform cell in boardGameObject.transform)
        {
            Destroy(cell.gameObject);
        }
    }

    /// <summary>
    /// Instancie toutes les cellules du plateau
    /// Chaque cellule est un prefab "cellPrefab" de taille 1
    /// Les cellules sont placées de gauche à droite (coordonnée X, vector Right)
    /// Les cellules sont placées de bas en haut (coordonnée Y, vector Up)
    /// </summary>
    public void GenerateCells()
    {
        DestroyCells();

        allCells = new GameObject[board.width, board.height];

        Vector3 cellSize = Vector3.one;
        Vector3 boardCenterPosition = boardGameObject.transform.position;
        Vector3 boardBottomLeftPosition = boardCenterPosition - ((cellSize.x * board.width) / 2) * Vector3.right - ((cellSize.y * board.height) / 2) * Vector3.up;
        Vector3 currentPosition = boardBottomLeftPosition;
        for (int x = 0; x < board.width; x++)
        {
            for (int y = 0; y < board.height; y++)
            {
                Vector3 position = boardBottomLeftPosition + x * cellSize.x * Vector3.right + y * cellSize.y * Vector3.up;
                allCells[x, y] = Instantiate(cellPrefab, position, cellPrefab.transform.rotation, this.transform);
                allCells[x, y].GetComponent<CellBehaviour>().SetupCoordinates(x, y);
            }
        }
    }

    public void DisplayBoard()
    {
        // On teste si le plateau a été instancié et que la scène correspond bien au modèle de données
        if (board == null || allCells == null || allCells.GetLength(0) != board.width || allCells.GetLength(1) != board.height)
        {
            // Si ce n'est pas le cas, on réinitialise le plateau dans la scène
            GenerateCells();
        }

        // EXERCICE : Parcourir le plateau et mettre à jour chaque cellule (GameObject)
        // Indice : Utiliser la méthode Getcell(x,y) sur l'objet board pour récupérer la valeur
        // Indice 2 : Utiliser les méthodes SetAlive() et SetDead() sur la cellule

        for (int x = 0; x < board.width; x++)
        {
            for (int y = 0; y < board.height; y++)
            {
                if (board.GetCell(x, y))
                {
                    allCells[x, y].GetComponent<CellBehaviour>().SetAlive();
                }
                else
                {
                    allCells[x, y].GetComponent<CellBehaviour>().SetDead();
                }
            }
        }

        //Debug.LogWarning("DisplayBoard() : METHODE A IMPLEMENTER");

        // FIN EXERCICE
    }

    public void ResetBoardEmpty()
    {
        board = new Board(110, 70);
        DisplayBoard();
    }

    public void ResetBoardRandom(float aliveCellsRatio)
    {
        board = new Board(110, 70, aliveCellsRatio);
        DisplayBoard();
    }

    public void ComputeNextGeneration()
    {
        board.NextGeneration();
        DisplayBoard();
    }

    public void PlaySimulation()
    {
        simulationIsPlaying = true;
    }
    public void StopSimulation()
    {
        simulationIsPlaying = false;
    }
    public void ChangeSimulationSpeed(float speed)
    {
        simulationDelayBetweenGenerations = 1 / speed;
    }

    public void SetCellStatus(int x, int y, bool alive)
    {
        board.SetCell(x, y, alive);
    }

    public void SaveBoard(string filename)
    {
        try
        {
            string filePath = Application.streamingAssetsPath + "/" + filename + ".json";
            string jsonContent = board.ToJSON();
            System.IO.File.WriteAllText(filePath, jsonContent);
            Debug.Log("Fichier " + filename + " sauvegardé avec succès");
        }
        catch (System.Exception ex)
        {
            Debug.LogWarning("Impossible de sauvegarder le fichier : " + filename + ". Erreur : " + ex.Message);
        }
    }

    public void LoadBoard(string filename)
    {
        try
        {
            string filePath = Application.streamingAssetsPath + "/" + filename + ".json";
            string jsonContent = System.IO.File.ReadAllText(filePath);
            board = Board.FromJSON(jsonContent);
            Debug.Log("Fichier " + filename + " chargé avec succès");
        }
        catch (System.Exception ex)
        {
            Debug.LogWarning("Impossible de charger le fichier : " + filename + ". Erreur : " + ex.Message);
        }
        DisplayBoard();
    }

    private void Awake()
    {
        instance = this;
        simulationIsPlaying = false;
    }

    /// <summary>
    /// Coroutine calculant une génération à intervalles réguliers, quand la simulation est en "lecture"
    /// </summary>
    /// <returns></returns>
    private IEnumerator ComputeSimulation()
    {
        while (true)
        {
            yield return new WaitForSeconds(simulationDelayBetweenGenerations);
            if (simulationIsPlaying)
            {
                try
                {
                    ComputeNextGeneration();
                }
                catch (System.Exception ex)
                {
                    Debug.LogError("Coroutine ; Impossible de calculer la génération suivante ; Erreur : " + ex.Message);
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Le Board Manager (ce script) est sur le plateau
        // Donc on peut accéder à l'objet "Board" en écrivant this.gameObject
        boardGameObject = this.gameObject;

        try
        {
            // Au démarrage du jeu, on remplit le plateau aléatoirement
            ResetBoardRandom(0.3f);
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Erreur à l'initialisation : " + ex.Message);
        }
        
        // On lance la coroutine, qui ne s'arrêtera qu'à la fin du jeu
        StartCoroutine(ComputeSimulation());
    }
}
