using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuSeleccionPersonaje : MonoBehaviour
{
    private int index;
    [SerializeField] private Image imagenPersonaje;
    [SerializeField] private TextMeshProUGUI nombrePersonaje;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.instance;
        index = PlayerPrefs.GetInt("PersonajeSeleccionado");

        if(index > gameManager.personajes.Count - 1){
            index = 0;
        }

        CambiarPantalla();
    }

    private void CambiarPantalla(){
        PlayerPrefs.SetInt("PersonajeSeleccionado", index);
        imagenPersonaje.sprite = gameManager.personajes[index].imagen;
        nombrePersonaje.text = gameManager.personajes[index].nombre;
    }

    public void BotonSiguiente(){
        if(index == gameManager.personajes.Count - 1){
            index = 0;
        }
        else{
            index += 1;
        }
        CambiarPantalla();
    }

    public void BotonAnterior(){
        if(index == 0){
            index = gameManager.personajes.Count - 1;
        }
        else{
            index -= 1;
        }
        CambiarPantalla();
    }

    public void BotonSeleccionar(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
