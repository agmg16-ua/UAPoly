using System.Collections;
using System.IO;
using UnityEngine;

public class ManejoTarjetasCC : MonoBehaviour
{

    // Array de tarjetas de suerte cargadas desde JSON
    private TarjetaSuerte[] tarjetasSuerte;

    // Reference to sprite renderer to change sprites
    private SpriteRenderer rendTarjetas;

    // Objeto TarjetasSuerte (Lee los datos del JSON).
    public class TarjetaSuerte
    {
        public Sprite imagen;
        public string imagenArchivo;
        public int dinero;
        public int casillas;
    }
    // Variables para almacenar el valor de la última tarjeta seleccionada
    private int lastCardIndex;
    private int lastCardMoney;
    private int lastCardSpaces;


    // Use this for initialization
    private void Start()
    {

        // Asigna componente Renderer
        rendTarjetas = GetComponent<SpriteRenderer>();

        // Cargar el archivo JSON desde la carpeta Resources
        TextAsset jsonFile = Resources.Load<TextAsset>("comunidad");

        // Leer el archivo JSON
        string json = jsonFile.text;

        // Deserializar el JSON en un array de objetos TarjetaSuerte
        tarjetasSuerte = JsonUtility.FromJson<TarjetaSuerte[]>(json);

        // Cargar las imágenes de las tarjetas de suerte
        foreach (TarjetaSuerte tarjeta in tarjetasSuerte)
        {
            // Construir la ruta de la imagen
            string imagePath = "TarjetasComunidad/" + tarjeta.imagenArchivo;

            // Cargar la imagen desde la carpeta Resources
            tarjeta.imagen = Resources.Load<Sprite>(imagePath);
        }
    }

    // Coroutine that rolls the dice
    public IEnumerator selectRandomCard()
    {

        // Variable to contain random dice side number.
        // It needs to be assigned. Let it be 0 initially
        int randomCard = 0;

        // Final side or value that dice reads in the end of coroutine
        int finalCard = 0;

        // Loop to switch dice sides ramdomly
        // before final side appears. 20 itterations here.
        for (int i = 0; i <= 20; i++)
        {

            randomCard = Random.Range(1, 23);

            // Set sprite to upper face of dice from array according to random value
            rendTarjetas.sprite = tarjetasSuerte[randomCard].imagen;

            // Pause before next itteration
            yield return new WaitForSeconds(0.05f);
        }

        // Assigning final side so you can use this value later in your game
        // for player movement for example
        finalCard = randomCard;

        // Guarda la última carta seleccionada
        lastCardIndex = finalCard;
        lastCardMoney = tarjetasSuerte[finalCard].dinero;
        lastCardSpaces = tarjetasSuerte[finalCard].casillas;

        // Show final dice value in Console
        Debug.Log(finalCard);
    }

    // Getter for the index of the last card selected
    public int GetLastCardIndex()
    {
        return lastCardIndex;
    }

    // Getter for the money value of the last card selected
    public int GetLastCardMoney()
    {
        return lastCardMoney;
    }

    // Getter for the number of spaces to move of the last card selected
    public int GetLastCardSpaces()
    {
        return lastCardSpaces;
    }
}