using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class Card : MonoBehaviour
{
    public int id;
    public string nombreJugador;
    //public Sprite imagen; ver como se hace esto
    public int dineroRestante;
    public int turnosRestantesCarcel;
    public List<string> listaPropiedades;

    public Card() {

    }

    public Card(int id, string nombreJugador, /*Sprite imagen,*/ int dineroRestante, int turnosRestantesCarcel, List<string> listaPropiedades) {
        this.id = id;
        this.nombreJugador = nombreJugador;
        //this.imagen = imagen;
        this.dineroRestante = dineroRestante;
        this.turnosRestantesCarcel = turnosRestantesCarcel;
        this.listaPropiedades = listaPropiedades;
    }
}
