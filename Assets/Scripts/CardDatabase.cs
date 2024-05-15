using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDatabase : MonoBehaviour
{
    public static CardDatabase instance;

    //Secciones cartas jugando y no jugando
    public bool jugando = false;

    public float[] pos_NonPlayingCard0 = {-306,165};

    public float[] pos_PlayingCard = {270,123};

    public float posY_NextNonPlayingCard = -167;

    //Lista de cartas
    private GameManager gameManager;  
    public static List<Card> cardList = new List<Card>();

    // Start is called before the first frame update
    private void Awake() {
        instance = this;
    }

    public void initializeCardList() {
        gameManager = GameManager.instance;

        for(int i = 0; i < gameManager.jugadores.Count; i++){
            if(i == 0){
                cardList.Add(new Card(gameManager.jugadores[i], pos_PlayingCard));
            }
            else{
                cardList.Add(new Card(gameManager.jugadores[i], 
                    [pos_NonPlayingCard0[0], posY_NextNonPlayingCard[1] + (i-1) * posY_NextNonPlayingCard]));
            }
        }

        GameObject canvas = new GameObject("CartasCanvas");
        Canvas cartasCanvas = canvas.AddComponent<Canvas>();

        GameObject childGameObject = new GameObject("TarjetaPlayer");
        childGameObject.transform.SetParent(canvas.transform, false);
    }

    public void updateCardList() {
        for(int i = 0; i < cardList.Count; i++){
            if(i == 0){
                cardList[i].pos_Card = pos_PlayingCard;
            }
            else{
                cardList[i].pos_Card = [pos_NonPlayingCard0[0], pos_NonPlayingCard0[1] + (i-1) * posY_NextNonPlayingCard];
            }
        }
    }

}
