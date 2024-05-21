using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuSeleccionPersonaje : MonoBehaviour
{
    private int index;
    private int jugador;
    public int numberPlayers;
    [SerializeField] private Image imagenPersonaje;
    [SerializeField] private TextMeshProUGUI nombrePersonaje;
    private GameManager gameManager;
    private GameManagerSeleccion gameManagerSeleccion;
    public TextMeshProUGUI pepe;

    private void Start()
    {
        gameManager = GameManager.instance;
        gameManagerSeleccion = GameManagerSeleccion.instance;
        numberPlayers = gameManagerSeleccion.numeroJugadores;
        index = PlayerPrefs.GetInt("PersonajeSeleccionado");    
        gameManagerSeleccion.personajesSeleccionados = new int[numberPlayers];  
        gameManager.jugadores = new List<Player>(numberPlayers);    //Numero de jugadores a crear

        for (int i = 0; i < gameManagerSeleccion.personajesSeleccionados.Length; i++)
        {
            gameManagerSeleccion.personajesSeleccionados[i] = -1;
        }
        
        jugador = 0;
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

        if(Contains(gameManagerSeleccion.personajesSeleccionados,index)){
            BotonSiguiente();
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

        if(Contains(gameManagerSeleccion.personajesSeleccionados,index)){
            BotonAnterior();
        }

        CambiarPantalla();
    }

    public void BotonSeleccionar(){
        numberPlayers --;
        if(numberPlayers == 0){
            gameManagerSeleccion.personajesSeleccionados[jugador] = index;

            GameObject jugadorObject = new GameObject("Jugador" + (jugador + 1));
            Player jugadorComponent = jugadorObject.AddComponent<Player>();
            jugadorComponent.playerMovement = jugadorObject.AddComponent<PlayerMove>();
            jugadorComponent.wallet = jugadorObject.AddComponent<PlayerWallet>();
            gameManager.jugadores.Add(jugadorComponent);
            gameManager.jugadores[jugador].nombre = "Jugador" + (jugador + 1);
            gameManager.jugadores[jugador].money = 1000;
            gameManager.jugadores[jugador].personaje = gameManager.personajes[index];

            pepe.text = gameManager.jugadores[jugador].nombre;
            gameManager.jugadores[jugador].SetPersonaje(gameManager.personajes[index]);
            gameManager.jugadores[jugador].transform.SetAsFirstSibling();
            SceneManager.LoadScene("Scenes/Game");
        }
        else{
            gameManagerSeleccion.personajesSeleccionados[jugador] = index;
            GameObject jugadorObject = new GameObject("Jugador" + (jugador + 1));
            Player jugadorComponent = jugadorObject.AddComponent<Player>();
            jugadorComponent.playerMovement = jugadorObject.AddComponent<PlayerMove>();
            jugadorComponent.wallet = jugadorObject.AddComponent<PlayerWallet>();
            gameManager.jugadores.Add(jugadorComponent);
            gameManager.jugadores[jugador].nombre = "Jugador" + (jugador + 1);
            gameManager.jugadores[jugador].money = 1000;
            gameManager.jugadores[jugador].personaje = gameManager.personajes[index];
            pepe.text = gameManager.jugadores[jugador].nombre;
            gameManager.jugadores[jugador].SetPersonaje(gameManager.personajes[index]);
            gameManager.jugadores[jugador].transform.SetAsFirstSibling();
            jugador ++;
            BotonSiguiente();
        }
    }

    public bool Contains(int[] valores,int entero){
        for (int i = 0; i < valores.Length; i++)
        {
            if(valores[i] == entero){
                return true;
            }
        }

        return false;
    }
}
