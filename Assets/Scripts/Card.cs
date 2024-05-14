using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class Card : MonoBehaviour
{
    public Player player;

    public Card(Player player) {
        this.player = player;
    }
}
