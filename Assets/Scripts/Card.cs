using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class Card : MonoBehaviour {
    public Player player;
    public TMP_Text playerMoneyText;
    public TMP_Text playerCarcelText;

    // Posición de la carta
    public float[] pos_CCard = { 0, 0 };

    public float[] scale = { 20, 20, 1 };

    private TMP_Text cifraDineroText;
    private TMP_Text cifraCarcelText;

    public void Initialize(Player player, float[] pos_CCard) {
        this.player = player;
        this.pos_CCard = pos_CCard;

        Transform campoDinero = transform.Find("Contenido/CampoDinero");
        if (campoDinero == null) {
            Debug.LogError("No se encontró el objeto 'CampoDinero'. Asegúrate de que esté en la jerarquía correcta.");
            return;
        }

        Transform cifraDinero = campoDinero.Find("CifraDinero");
        if (cifraDinero == null) {
            Debug.LogError("No se encontró el objeto 'CifraDinero' bajo 'CampoDinero'. Asegúrate de que esté en la jerarquía correcta.");
            return;
        }

        cifraDineroText = cifraDinero.GetComponent<TMP_Text>();
        if (cifraDineroText == null) {
            Debug.LogError("No se encontró el componente TMP_Text 'CifraDinero'. Asegúrate de que esté en la jerarquía correcta.");
            return;
        }

        Transform campoCarcel = transform.Find("Contenido/CampoCarcel");
        if (campoCarcel == null) {
            Debug.LogError("No se encontró el objeto 'CampoCarcel'. Asegúrate de que esté en la jerarquía correcta.");
            return;
        }

        Transform cifraCarcel = campoCarcel.Find("CifraCarcel");
        if (cifraCarcel == null) {
            Debug.LogError("No se encontró el objeto 'CifraCarcel' bajo 'CampoCarcel'. Asegúrate de que esté en la jerarquía correcta.");
            return;
        }
        cifraCarcelText = cifraCarcel.GetComponent<TMP_Text>();
        if (cifraCarcelText == null) {
            Debug.LogError("No se encontró el componente TMP_Text 'CifraCarcel'. Asegúrate de que esté en la jerarquía correcta.");
            return;
        }
        UpdateCard();
    }


    public void UpdateCard() {
        if (player != null && player.wallet != null && cifraDineroText != null && cifraCarcelText != null) {
            cifraDineroText.text = "Dinero: " + player.wallet.getWalletAmount() + "€";
            cifraCarcelText.text = "Turnos restantes carcel: " + player.turnosRestantesCarcel;
        }
    }

    private void Update() {
        UpdateCard();
    }
}
