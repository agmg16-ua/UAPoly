using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]

public class Card : MonoBehaviour
{
    public Player player;
    public TMP_Text playerMoneyText;

    //Posicion de la carta
    public float[] pos_Card = {0,0};

    public float[] scale = {20,20,1};
    public void SetPlayer(Player player) {
        this.player = player;
        UpdateCard();
    }

    public Card(Player player, float[] pos_Card) {
        this.player = player;
        UpdateCard();
    }

    public void UpdateCard() {
        if (player != null) {
            playerMoneyText.text = " " + player.wallet.getWalletAmount();
        }
    }

    private void Update() {
        UpdateCard();
    }
}
