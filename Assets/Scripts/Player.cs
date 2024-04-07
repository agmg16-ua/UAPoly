using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Player : MonoBehaviour
{
    public static int numeroDeJugador = 0;

    public string nombre;
    public int money;
    public Personajes personaje;

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