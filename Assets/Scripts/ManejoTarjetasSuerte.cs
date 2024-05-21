using System.Collections;
using UnityEngine;

public class ManejoTarjetasSuerte : MonoBehaviour
{
    // Array de tarjetas de suerte cargadas desde JSON
    private TarjetaSuerte[] tarjetasSuerte;

    // Referencia al sprite renderer para cambiar los sprites
    private SpriteRenderer rendTarjetas;

    // Objeto TarjetasSuerte (Lee los datos del JSON).
    [System.Serializable]
    public class TarjetaSuerte
    {
        public string imagen;
        public int dinero;
        public int casillas;

        [System.NonSerialized]
        public Sprite imagenSprite;
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
        TextAsset jsonFile = Resources.Load<TextAsset>("suerte");

        if (jsonFile != null)
        {
            // Leer el archivo JSON
            string json = jsonFile.text;

            // Deserializar el JSON en un array de objetos TarjetaSuerte
            tarjetasSuerte = JsonUtility.FromJson<TarjetaSuerteArray>(json).tarjetas;

            // Cargar las imágenes de las tarjetas de suerte
            foreach (TarjetaSuerte tarjeta in tarjetasSuerte)
            {
                // Construir la ruta de la imagen
                string imagePath = "TarjetasSuerte/" + tarjeta.imagen;

                // Cargar la imagen desde la carpeta Resources
                tarjeta.imagenSprite = Resources.Load<Sprite>(imagePath);

                if (tarjeta.imagenSprite == null)
                {
                    Debug.LogError("No se pudo cargar la imagen en la ruta: " + imagePath);
                }
            }
        }
        else
        {
            Debug.LogError("No se pudo cargar el archivo JSON desde Resources");
        }

        // Inicializar las variables de la última tarjeta seleccionada
        lastCardIndex = -1;
        lastCardMoney = 0;
        lastCardSpaces = 0;
    }

    // Coroutine that selects a random card
    public IEnumerator selectRandomCard()
    {
        // Lógica para seleccionar una tarjeta aleatoria
        yield return new WaitForSeconds(1); // Simulación de espera

        // Selecciona una tarjeta aleatoria
        lastCardIndex = Random.Range(0, tarjetasSuerte.Length);
        TarjetaSuerte selectedCard = tarjetasSuerte[lastCardIndex];

        // Almacena los valores de la tarjeta seleccionada
        lastCardMoney = selectedCard.dinero;
        lastCardSpaces = selectedCard.casillas;

        // Actualiza el sprite render con la imagen de la tarjeta seleccionada
        rendTarjetas.sprite = selectedCard.imagenSprite;
    }

    // Métodos para obtener los valores de la última tarjeta seleccionada
    public int GetLastCardIndex()
    {
        return lastCardIndex;
    }

    public int GetLastCardMoney()
    {
        return lastCardMoney;
    }

    public int GetLastCardSpaces()
    {
        return lastCardSpaces;
    }

    [System.Serializable]
    private class TarjetaSuerteArray
    {
        public TarjetaSuerte[] tarjetas;
    }
}
