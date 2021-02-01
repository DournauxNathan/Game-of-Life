using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Cette classe gère le comportement d'une cellule dans la scène
/// Une cellule est un GameObject qui réagit aux clics de souris
/// - un clic = changer l'état vivant/mort de la cellule
/// </summary>
public class CellBehaviour : MonoBehaviour
{
    [Header("Materials")]
    public Material aliveMaterial;
    public Material deadMaterial;

    [Header("References dans la scene")]
    public Renderer cellRenderer;

    [Header("Coordonnées de la cellule")]
    public int x;
    public int y;

    [Header("Statut de la cellule")]
    public bool alive;
    
    #region public methods

    public void SetupCoordinates(int coordX, int coordY)
    {
        x = coordX;
        y = coordY;
    }

    public void Toggle()
    {
        if (!alive)
        {
            SetAlive();
        }
        else
        {
            SetDead();
        }
    }

    public void SetAlive()
    {
        alive = true;
        BoardManager.instance.SetCellStatus(x, y, alive);
        UpdateDisplay();
    }

    public void SetDead()
    {
        alive = false;
        BoardManager.instance.SetCellStatus(x, y, alive);
        UpdateDisplay();
    }

    #endregion

    private void UpdateDisplay()
    {
        // EXERCICE : Modifier l'apparence de la cellule en fonction de son état vivant/mort
        if (alive)
        {
            cellRenderer.material = aliveMaterial;
        }
        else if (!alive)
        {
            cellRenderer.material = deadMaterial;
        }
        // FIN EXERCICE
    }

    #region Mouse click

    /*private void OnMouseDown()
    {
        Toggle();
    }
*/
    // EXERCICE : Lorsque le curseur de la souris passe au dessus d'une cellule, 
    // et si le bouton gauche de la souris est enfoncé, je veux que l'état de la cellule change
    private void OnMouseOver()
    {        
        if (Input.GetMouseButton(0))
        {
            Toggle();
        }

    }
    // FIN EXERCICE

    #endregion
}
