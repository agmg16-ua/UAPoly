using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChecklistManager : MonoBehaviour
{
    public Toggle[] toggles; // Array que contiene los toggles de la checklist

    public Sprite imagenNormal;
    public Sprite imagenSeleccionada;

    private void Start()
    {
        // Agrega un listener a cada toggle para que llame al método ToggleValueChanged cuando se cambie su estado
        foreach (Toggle toggle in toggles)
        {
            toggle.image.sprite = imagenNormal;
            toggle.isOn = false;
            toggle.onValueChanged.AddListener(delegate { ToggleValueChanged(toggle); });
        }
    }

    private void ToggleValueChanged(Toggle changedToggle)
    {
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
        SceneManager.LoadScene("Assets/Scenes/SeleccionPersonaje");
    }
}

