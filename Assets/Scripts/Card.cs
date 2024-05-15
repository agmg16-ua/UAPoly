using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class Card : MonoBehaviour
{
    public Player player;

    //Posicion de la carta
    public float[] pos_Card = {0,0};

    public float[] scale = {20,20,1};

    public Card(Player player, float[] pos_Card) {
        this.player = player;
    }
}
