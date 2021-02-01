using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Cette classe gère l'affichage de l'interface et les évènements (clic sur un bouton par exemple)
/// </summary>
public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("References in scene")]
    public GameObject loadSaveSlot1Panel;
    public GameObject loadSaveSlot2Panel;
    public Button stepByStepButton;
    public Button playButton;
    public Button stopButton;
    public Slider playSpeedSlider;
    public Text playSpeedText;

    private const string slot1FileName = "savefile1";
    private const string slot2FileName = "savefile2";

    #region Interface Events

    public void ResetToEmptyBoard()
    {
        BoardManager.instance.ResetBoardEmpty();
    }

    public void ResetToRandomBoard()
    {
        BoardManager.instance.ResetBoardRandom(0.3f);
    }

    public void ShowSlot1Panel()
    {
        loadSaveSlot1Panel.SetActive(true);
        loadSaveSlot2Panel.SetActive(false);
    }
    public void ShowSlot2Panel()
    {
        loadSaveSlot1Panel.SetActive(false);
        loadSaveSlot2Panel.SetActive(true);
    }

    public void SaveSlot1()
    {
        BoardManager.instance.SaveBoard(slot1FileName);
    }
    public void LoadSlot1()
    {
        BoardManager.instance.LoadBoard(slot1FileName);
    }

    public void SaveSlot2()
    {
        BoardManager.instance.SaveBoard(slot2FileName);
    }
    public void LoadSlot2()
    {
        BoardManager.instance.LoadBoard(slot2FileName);
    }

    public void SimulateOneStep()
    {
        StopSimulation();
        BoardManager.instance.ComputeNextGeneration();
    }

    public void PlaySimulation()
    {
        BoardManager.instance.PlaySimulation();
        playButton.interactable = false;
        stopButton.interactable = true;
    }
    public void StopSimulation()
    {
        BoardManager.instance.StopSimulation();
        playButton.interactable = true;
        stopButton.interactable = false;
    }

    public void SimulationSpeedChanged()
    {
        playSpeedText.text = "x " + playSpeedSlider.value;
        BoardManager.instance.ChangeSimulationSpeed(playSpeedSlider.value);
    }

    #endregion
    
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        SimulationSpeedChanged();
        ShowSlot1Panel();
    }

    private void Update()
    {
        // EXERCICE : Quand j'appuie sur la touche Echap, je veux que l'application se ferme
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        // FIN EXERCICE
    }
}
