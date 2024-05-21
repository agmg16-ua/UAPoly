using System.Collections;
using System.Collections.Generic;
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

    // Variables para almacenar el valor de la �ltima tarjeta seleccionada
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
            tarjetasSuerte = FromJson<TarjetaSuerte>(json);

            // Cargar las im�genes de las tarjetas de suerte
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

        // Inicializar las variables de la �ltima tarjeta seleccionada
        lastCardIndex = -1;
        lastCardMoney = 0;
        lastCardSpaces = 0;
    }

    // Coroutine that selects a random card
    public IEnumerator selectRandomCard(Player player, int playerIndex, int[] playerStartWaypoint)
    {
        // L�gica para seleccionar una tarjeta aleatoria
        yield return new WaitForSeconds(1); // Simulaci�n de espera

        // Selecciona una tarjeta aleatoria
        lastCardIndex = Random.Range(0, tarjetasSuerte.Length);
        TarjetaSuerte selectedCard = tarjetasSuerte[lastCardIndex];

        // Almacena los valores de la tarjeta seleccionada
        lastCardMoney = selectedCard.dinero;
        lastCardSpaces = selectedCard.casillas;

        // Actualiza el sprite render con la imagen de la tarjeta seleccionada
        rendTarjetas.sprite = selectedCard.imagenSprite;

        // Aplica los efectos de la tarjeta al jugador actual
        if (tarjetasSuerte[lastCardIndex].dinero > 0)
        {
            player.wallet.addMoney(tarjetasSuerte[lastCardIndex].dinero);
        }
        if (tarjetasSuerte[lastCardIndex].dinero < 0)
        {
            player.wallet.subtractMoney(-tarjetasSuerte[lastCardIndex].dinero);
        }
        if (tarjetasSuerte[lastCardIndex].casillas != 0)
        {
            StartCoroutine(MovePlayerToPosition(player, tarjetasSuerte[lastCardIndex].casillas, playerIndex, playerStartWaypoint));
        }
    }

    // M�todos para obtener los valores de la �ltima tarjeta seleccionada
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

    // M�todo auxiliar para deserializar arrays de JSON
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

    // M�todo para enviar al jugador a otra casilla
    private IEnumerator MovePlayerToPosition(Player player, int position, int playerIndex, int[] playerStartWaypoint)
    {
        UnityEngine.Debug.Log("Player " + (position + 1) + " is moving to new position...");
        // Mueve al jugador a la casilla de la tarjeta de evento.
        playerStartWaypoint[playerIndex] = position;
        player.playerMovement.waypointIndex = position;

        // Desactiva el movimiento del jugador
        player.playerMovement.moveAllowed = false;

        // Espera tres segundos en tiempo de juego antes de reactivar el movimiento
        // Espera tres turnos antes de reactivar el movimiento
        yield return new WaitForSeconds(3 * Time.deltaTime * 60);
    }

}
