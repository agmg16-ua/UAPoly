using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CardDatabase : MonoBehaviour
{
    public static CardDatabase instance;

    //Secciones cartas jugando y no jugando
    public static float[] pos_NonPlayingCard0 = {-306,165};

    public static float[] pos_PlayingCard = {270,123};

    public static float posY_NextNonPlayingCard = -167;

    //Lista de cartas
    private GameManager gameManager;  
    public static List<Card> cardList = new List<Card>();

    public List<Card> mostrar = new List<Card>();

    // Start is called before the first frame update
    private void Awake() {
        instance = this;
        initializeCardList();
    }

    public void initializeCardList() {
        gameManager = GameManager.instance;
        Debug.Log("Numero de jugadores = " + gameManager.jugadores.Count);
        for(int i = 0; i < gameManager.jugadores.Count; i++){
            if(i == 0){
                cardList = new List<Card>(gameManager.jugadores.Count);
                cardList.Add(this.gameObject.AddComponent<Card>());
                cardList[i].Initialize(gameManager.jugadores[i], pos_PlayingCard);
                Debug.Log("Carta de jugador " + i + " = " + cardList[i].player.nombre);
            }
            else{
                float[] pos = new float[]{pos_NonPlayingCard0[0], pos_NonPlayingCard0[1] + (i-1) * posY_NextNonPlayingCard};
                cardList.Add(this.gameObject.AddComponent<Card>());
                cardList[i].Initialize(gameManager.jugadores[i], pos);
                Debug.Log("Carta de jugador " + i + " = " + cardList[i].player.nombre);
            }
        }

        mostrar = cardList;
        GameObject canvas = new GameObject("CartasCanvas");
        Canvas cartasCanvas = canvas.AddComponent<Canvas>();

        GameObject childGameObject = new GameObject("TarjetaPlayer");
        childGameObject.transform.SetParent(canvas.transform, false);
    }

    public static void updateCardList(int whosTurn) {
        cardList.Add(cardList[0]);
        cardList.RemoveAt(0);
        
        for(int i = 0; i < cardList.Count; i++){
            if(i == 0){
                cardList[i].pos_Card = pos_PlayingCard;
            }
            else{
                cardList[i].pos_Card = 
                    new float[]
                    {
                            pos_NonPlayingCard0[0], pos_NonPlayingCard0[1] + (i-1) * posY_NextNonPlayingCard
                    };
            }
        }

        for(int i = 0;i<cardList.Count;i++){
            Debug.Log("Posicion de la carta player " + i + " = [" + cardList[i].pos_Card[0] + "," + cardList[i].pos_Card[1] + "]");
        }
    }

}
