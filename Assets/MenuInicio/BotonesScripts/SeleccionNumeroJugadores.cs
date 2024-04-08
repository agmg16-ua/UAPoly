using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ChecklistManager : MonoBehaviour
{
    public Toggle[] toggles; // Array que contiene los toggles de la checklist

    public Sprite imagenNormal;
    public Sprite imagenSeleccionada;

    public TextMeshProUGUI error;

    private bool seleccionado = false;
    
    private GameManagerSeleccion gameManager;

    private void Start()
    {
        // Agrega un listener a cada toggle para que llame al método ToggleValueChanged cuando se cambie su estado
        foreach (Toggle toggle in toggles)
        {
            toggle.image.sprite = imagenNormal;
            toggle.isOn = false;
            toggle.onValueChanged.AddListener(delegate { ToggleValueChanged(toggle); });
            gameManager = GameManagerSeleccion.instance;
        }
    }

    private void ToggleValueChanged(Toggle changedToggle)
    {
        seleccionado = true;
        error.text = "";
        // Desactiva todos los toggles excepto el que se ha cambiado
        foreach (Toggle toggle in toggles)
        {
            if (toggle != changedToggle)
            {
                toggle.isOn = false;
                toggle.image.sprite = imagenNormal;
            }
        }

        changedToggle.image.sprite = imagenSeleccionada;
    }

    public void Siguiente(){
        foreach (Toggle toggle in toggles)
        {
            if (toggle.isOn)
            {
                if (toggle.name == "two")
                {
                    gameManager.numeroJugadores = 2;
                }
                else if (toggle.name == "three")
                {
                    gameManager.numeroJugadores = 3;
                }
                else if (toggle.name == "four")
                {
                    gameManager.numeroJugadores = 4;
                }
            }
        }

        if (seleccionado)
        {
            SceneManager.LoadScene("Scenes/SeleccionPersonaje");
        }
        else{
            Debug.Log("No se ha seleccionado el numero de jugadores");
            error.text = "No se ha seleccionado el numero de jugadores";
        }
    }
}

