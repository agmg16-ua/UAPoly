using System.Collections;
using UnityEngine;

public class ManejoTarjetasCC : MonoBehaviour
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
    private string lastCardName;
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

        if (jsonFile != null)
        {
            // Leer el archivo JSON
            string json = jsonFile.text;

            // Deserializar el JSON en un array de objetos TarjetaSuerte
            tarjetasSuerte = FromJson<TarjetaSuerte>(json);

            // Cargar las imágenes de las tarjetas de suerte
            foreach (TarjetaSuerte tarjeta in tarjetasSuerte)
            {
                // Construir la ruta de la imagen
                string imagePath = "TarjetasComunidad/" + tarjeta.imagen;

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
        lastCardName = "";
        lastCardIndex = -1;
        lastCardMoney = 0;
        lastCardSpaces = 0;
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

    public string GetLastCardName()
    {
        return lastCardName;
    }

    // Corrutina para seleccionar una tarjeta de suerte aleatoria y ejecutar un callback
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
        lastCardName = selectedCard.imagen;

        // Actualiza el sprite render con la imagen de la tarjeta seleccionada
        rendTarjetas.sprite = selectedCard.imagenSprite;
    }

    // Método auxiliar para deserializar arrays de JSON
    public static T[] FromJson<T>(string json)
    {
        string wrappedJson = "{\"Items\":" + json + "}";
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(wrappedJson);
        return wrapper.Items;
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }

}
