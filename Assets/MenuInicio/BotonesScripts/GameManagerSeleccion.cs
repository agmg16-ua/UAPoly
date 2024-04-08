using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerSeleccion : MonoBehaviour
{
    public static GameManagerSeleccion instance;
    public int numeroJugadores;
    public int[] personajesSeleccionados;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }
}
