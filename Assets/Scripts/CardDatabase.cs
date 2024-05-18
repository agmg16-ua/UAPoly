using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CardDatabase : MonoBehaviour
{
    public static CardDatabase instance;

    //Secciones cartas jugando y no jugando
    public static float[] pos_NonPlayingCard0 = {-13.5f,9.7f};

    public static float[] pos_PlayingCard = {15.9f,7.1f};

    public static float posY_NextNonPlayingCard = -9.4f;

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

        crearCanvasCartas();

        for(int i = 0; i < gameManager.jugadores.Count; i++){
            if(i == 0){
                cardList[i].Initialize(gameManager.jugadores[i], pos_PlayingCard);
                Debug.Log("Carta de jugador " + i + " = " + cardList[i].player.nombre);
            }
            else{
                float[] pos = new float[]{pos_NonPlayingCard0[0], pos_NonPlayingCard0[1] + (i-1) * posY_NextNonPlayingCard};
                cardList[i].Initialize(gameManager.jugadores[i], pos);
                Debug.Log("Carta de jugador " + i + " = " + cardList[i].player.nombre);
            }
        }

        mostrar = cardList;
    }

    public void crearCanvasCartas(){
        GameObject canvas = new GameObject("CartasCanvas");
        Canvas cartasCanvas = canvas.AddComponent<Canvas>();
        canvas.AddComponent<PasarTurno>();
        RectTransform rectTransformCanvas = canvas.GetComponent<RectTransform>();
        rectTransformCanvas.anchoredPosition = new Vector2(0,0);
        rectTransformCanvas.localScale = new Vector3(2,2,1);
        rectTransformCanvas.sizeDelta = new Vector2(88.3179f, 61.2888f);
        cartasCanvas.overrideSorting = true;
        cartasCanvas.sortingOrder = 1; // Ajusta este valor según tus necesidades

        for(int i = 0; i < gameManager.jugadores.Count; i++){
            GameObject tarjeta = new GameObject("TarjetaPlayer" + (i + 1));
            tarjeta.transform.SetParent(canvas.transform, false);
            cardList.Add(tarjeta.AddComponent<Card>());
            tarjeta.AddComponent<Image>().color = Color.black;
            RectTransform rectTransformTarjetas = tarjeta.GetComponent<RectTransform>();
            rectTransformTarjetas.anchoredPosition = new Vector2(cardList[i].pos_Card[0], cardList[i].pos_Card[1]);
            rectTransformTarjetas.localScale = new Vector3(1,1,1);
            rectTransformTarjetas.sizeDelta = new Vector2(5, 8);

                GameObject contenido = new GameObject("Contenido");
                contenido.transform.SetParent(tarjeta.transform, false);
                contenido.AddComponent<Image>().color = Color.white;
                contenido.AddComponent<Player>();
                RectTransform rectTransformContenido = contenido.GetComponent<RectTransform>();
                rectTransformContenido.anchoredPosition = new Vector2(0,0);
                rectTransformContenido.localScale = new Vector3(1,1,1);
                rectTransformContenido.sizeDelta = new Vector2(4.6f, 7.6f);

                    GameObject campoUsuario = new GameObject("CampoUsuario");
                    campoUsuario.transform.SetParent(contenido.transform, false);
                    campoUsuario.AddComponent<Image>();
                    RectTransform rectTransformUsuario = campoUsuario.GetComponent<RectTransform>();
                    rectTransformUsuario.anchoredPosition = new Vector2(-1.21f,2.68f);
                    rectTransformUsuario.localScale = new Vector3(1,1,1);
                    rectTransformUsuario.sizeDelta = new Vector2(2, 2);
                        GameObject logo = new GameObject("Logo");
                        logo.transform.SetParent(campoUsuario.transform, false);
                        Image image = logo.AddComponent<Image>();
                        image.sprite = gameManager.jugadores[i].personaje.imagen;
                        RectTransform rectTransformLogo = logo.GetComponent<RectTransform>();
                        rectTransformLogo.anchoredPosition = new Vector2(0,0);
                        rectTransformLogo.localScale = new Vector3(1,1,1);
                        rectTransformLogo.sizeDelta = new Vector2(1.75f, 1.75f);
                        
                        GameObject nombre = new GameObject("Nombre");
                        nombre.transform.SetParent(campoUsuario.transform, false);
                        TMPro.TextMeshProUGUI nombreJugador = nombre.AddComponent<TMPro.TextMeshProUGUI>();
                        nombreJugador.text = gameManager.jugadores[i].nombre;
                        nombreJugador.fontSize = 0.5f;
                        nombreJugador.color = Color.black;
                        RectTransform rectTransformNombre = nombre.GetComponent<RectTransform>();
                        rectTransformNombre.anchoredPosition = new Vector2(3.5f, 0.3f);
                        rectTransformNombre.localScale = new Vector3(1,1,1);
                        rectTransformNombre.sizeDelta = new Vector2(5, 0);

                    GameObject campoPropiedades = new GameObject("CampoPropiedades");
                    campoPropiedades.transform.SetParent(contenido.transform, false);
                    campoPropiedades.AddComponent<Image>();
                    RectTransform rectTransformPropiedades = campoPropiedades.GetComponent<RectTransform>();
                    rectTransformPropiedades.anchoredPosition = new Vector2(0,0);
                    rectTransformPropiedades.localScale = new Vector3(1,1,1);
                    rectTransformPropiedades.sizeDelta = new Vector2(4.5f, 2);
                        GameObject propiedades = new GameObject("Propiedades");
                        propiedades.transform.SetParent(campoPropiedades.transform, false);
                        TMPro.TextMeshProUGUI propiedadesText = propiedades.AddComponent<TMPro.TextMeshProUGUI>();
                        RectTransform rectTransformPropiedadesCampo = propiedades.GetComponent<RectTransform>();
                        rectTransformPropiedadesCampo.anchoredPosition = new Vector2(0,0);
                        rectTransformPropiedadesCampo.localScale = new Vector3(1,1,1);
                        rectTransformPropiedadesCampo.sizeDelta = new Vector2(0.3f, 0.3f);
                        propiedadesText.fontSize = 0.3f;
                        propiedadesText.color = Color.black;

                    GameObject campoDinero = new GameObject("CampoDinero");
                    campoDinero.transform.SetParent(contenido.transform, false);
                    campoDinero.AddComponent<Image>();
                    RectTransform rectTransformDinero = campoDinero.GetComponent<RectTransform>();
                    rectTransformDinero.anchoredPosition = new Vector2(0,-1.75f);
                    rectTransformDinero.localScale = new Vector3(1,1,1);
                    rectTransformDinero.sizeDelta = new Vector2(4.5f, 1);
                        GameObject dinero = new GameObject("DineroRestante");
                        dinero.transform.SetParent(campoDinero.transform, false);
                        TMPro.TextMeshProUGUI dineroText = dinero.AddComponent<TMPro.TextMeshProUGUI>();
                        RectTransform rectTransformDineroRestante = dinero.GetComponent<RectTransform>();
                        rectTransformDineroRestante.anchoredPosition = new Vector2(-0.7f,-0.4f);
                        rectTransformDineroRestante.localScale = new Vector3(1,1,1);
                        rectTransformDineroRestante.sizeDelta = new Vector2(3, 1);
                        dineroText.fontSize = 0.3f;
                        dineroText.color = Color.black;
                        dineroText.text = "Dinero: ";

                        GameObject cifraDinero = new GameObject("CifraDinero");
                        cifraDinero.transform.SetParent(campoDinero.transform, false);
                        TMPro.TextMeshProUGUI cifraDineroTexto = cifraDinero.AddComponent<TMPro.TextMeshProUGUI>();
                        cifraDinero.AddComponent<PlayerWallet>();
                        RectTransform rectTransformCifraDinero = cifraDinero.GetComponent<RectTransform>();
                        rectTransformCifraDinero.anchoredPosition = new Vector2(1.5f,-0.4f);
                        rectTransformCifraDinero.localScale = new Vector3(1,1,1);
                        rectTransformCifraDinero.sizeDelta = new Vector2(3, 1);
                        cifraDineroTexto.fontSize = 0.3f;
                        cifraDineroTexto.color = Color.black;

                    GameObject campoCarcel = new GameObject("CampoCarcel");
                    campoCarcel.transform.SetParent(contenido.transform, false);
                    campoCarcel.AddComponent<Image>();
                    RectTransform rectTransformCarcel = campoCarcel.GetComponent<RectTransform>();
                    rectTransformCarcel.anchoredPosition = new Vector2(0,-2.75f);
                    rectTransformCarcel.localScale = new Vector3(1,1,1);
                    rectTransformCarcel.sizeDelta = new Vector2(4.5f, 1);
                        GameObject carcel = new GameObject("RestantesCarcel");
                        carcel.transform.SetParent(campoCarcel.transform, false);
                        TMPro.TextMeshProUGUI carcelText = carcel.AddComponent<TMPro.TextMeshProUGUI>();
                        RectTransform rectTransformCarcelRestantes = carcel.GetComponent<RectTransform>();
                        rectTransformCarcelRestantes.anchoredPosition = new Vector2(-0.2f,-0.2f);
                        rectTransformCarcelRestantes.localScale = new Vector3(1,1,1);
                        rectTransformCarcelRestantes.sizeDelta = new Vector2(4, 1);
                        carcelText.fontSize = 0.3f;
                        carcelText.color = Color.black;
                        carcelText.text = "Turnos restantes en carcel: ";
                        
                        GameObject cifraCarcel = new GameObject("CifraCarcel");
                        cifraCarcel.transform.SetParent(campoCarcel.transform, false);
                        TMPro.TextMeshProUGUI cifraCarcelText = cifraCarcel.AddComponent<TMPro.TextMeshProUGUI>();
                        RectTransform rectTransformCifraCarcel = cifraCarcel.GetComponent<RectTransform>();
                        rectTransformCifraCarcel.anchoredPosition = new Vector2(2.65f,-0.2f);
                        rectTransformCifraCarcel.localScale = new Vector3(1,1,1);
                        rectTransformCifraCarcel.sizeDelta = new Vector2(3, 1);
                        cifraCarcelText.fontSize = 0.3f;
                        cifraCarcelText.color = Color.black;
                        cifraCarcelText.text = "0";
        }
        updateCardList(0, true);
    }

    public static void updateCardList(int whosTurn, bool inicial) {
        if(inicial == false){
            cardList.Add(cardList[0]);
            cardList.RemoveAt(0);
        }
        
        for(int i = 0; i < cardList.Count; i++){
            if(i == 0){
                RectTransform rectTransformTarjetas = cardList[i].gameObject.GetComponent<RectTransform>();
                cardList[i].pos_Card = pos_PlayingCard;
                rectTransformTarjetas.anchoredPosition = new Vector2(cardList[i].pos_Card[0], cardList[i].pos_Card[1]);
            }
            else{
                cardList[i].pos_Card = 
                    new float[]
                    {
                            pos_NonPlayingCard0[0], pos_NonPlayingCard0[1] + (i-1) * posY_NextNonPlayingCard
                    };
                RectTransform rectTransformTarjetas = cardList[i].gameObject.GetComponent<RectTransform>();
                rectTransformTarjetas.anchoredPosition = new Vector2(cardList[i].pos_Card[0], cardList[i].pos_Card[1]);
            }
        }

        for(int i = 0;i<cardList.Count;i++){
            Debug.Log("Posicion de la carta player " + i + " = [" + cardList[i].pos_Card[0] + "," + cardList[i].pos_Card[1] + "]");
        }
    }

}
