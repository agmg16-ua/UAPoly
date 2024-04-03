using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerSeleccion : MonoBehaviour
{
    public static GameManagerSeleccion instance;
    public int numeroJugadores;

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
