using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDatabase : MonoBehaviour
{
    private GameManager gameManager;  
    public static List<Card> cardList = new List<Card>();

    private void Awake() {
        gameManager = GameManager.instance;

        for(int i = 0; i < gameManager.jugadores.Count; i++){
            cardList.Add(new Card(gameManager.jugadores[i]));
        }

        GameObject canvas = new GameObject("CartasCanvas");
        Canva cartasCanvas = canvas.AddComponent<Canvas>();

        GameObject childGameObject = new GameObject("TarjetaPlayer");
        childGameObject.transform.SetParent(canvas.transform, false);
    }
}
