using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class Card : MonoBehaviour {
    public Player player;
    public TMP_Text playerMoneyText;

    // Posici�n de la carta
    public float[] pos_CCard = { 0, 0 };

    public float[] scale = { 20, 20, 1 };

    private TMP_Text cifraDineroText;

    public void Initialize(Player player, float[] pos_CCard) {
        this.player = player;
        this.pos_CCard = pos_CCard;

        Transform campoDinero = transform.Find("Contenido/CampoDinero");
        if (campoDinero == null) {
            Debug.LogError("No se encontr� el objeto 'CampoDinero'. Aseg�rate de que est� en la jerarqu�a correcta.");
            return;
        }

        Transform cifraDinero = campoDinero.Find("CifraDinero");
        if (cifraDinero == null) {
            Debug.LogError("No se encontr� el objeto 'CifraDinero' bajo 'CampoDinero'. Aseg�rate de que est� en la jerarqu�a correcta.");
            return;
        }

        cifraDineroText = cifraDinero.GetComponent<TMP_Text>();
        if (cifraDineroText == null) {
            Debug.LogError("No se encontr� el componente TMP_Text 'CifraDinero'. Aseg�rate de que est� en la jerarqu�a correcta.");
            return;
        }

        UpdateCard();
    }


    public void UpdateCard() {
        if (player != null && player.wallet != null && cifraDineroText != null) {
            cifraDineroText.text = "Dinero: " + player.wallet.getWalletAmount() + "�";
        }
    }

    private void Update() {
        UpdateCard();
    }
}
