using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDatabase : MonoBehaviour
{
    public static List<Card> cardList = new List<Card>();

    private void Awake() {
        cardList.Add(new Card(new Player(), 0, new List<string>()));
    }
}
