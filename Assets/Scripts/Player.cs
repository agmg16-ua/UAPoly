using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Player : MonoBehaviour
{
    public string playerName;
    public int money;
    public Personajes personaje;

    // Aquí puedes agregar más propiedades y métodos según tus necesidades

    private void Start()
    {
        money = 1500;
    }
}