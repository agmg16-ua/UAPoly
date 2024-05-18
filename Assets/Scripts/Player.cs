using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    public static int numeroDeJugador = 0;

    public string nombre;

    public int turnosRestantesCarcel;

    public List<string> listaPropiedades;
    public int money;
    public Personajes personaje;

    public PlayerMove playerMovement;

    public PlayerWallet wallet;

    public bool haTirado = false;

    void Start()
    {
        // Marcar este objeto para no ser destruido al cargar una nueva escena
        DontDestroyOnLoad(gameObject);
    }
    
    public void SetPersonaje(Personajes personaje)
    {
        this.personaje = personaje;
    }
}