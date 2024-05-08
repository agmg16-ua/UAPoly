using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class Card : MonoBehaviour
{
    public Player player;
    public int turnosRestantesCarcel;
    public List<string> listaPropiedades;

    public Card() {

    }

    public Card(Player player, int turnosRestantesCarcel, List<string> listaPropiedades) {
        this.player = player;
        this.turnosRestantesCarcel = turnosRestantesCarcel;
        this.listaPropiedades = listaPropiedades;
    }
}
