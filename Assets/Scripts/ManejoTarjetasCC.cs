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

    // Variable para almacenar el valor de la última tarjeta seleccionada
    private int lastCardIndex;

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
    }

    // Corrutina para seleccionar una tarjeta de suerte aleatoria y ejecutar un callback
    public IEnumerator selectRandomCard(Player player, int playerIndex, int[] playerStartWaypoint)
    {
        // Lógica para seleccionar una tarjeta aleatoria
        yield return new WaitForSeconds(1); // Simulación de espera

        // Selecciona una tarjeta aleatoria
        lastCardIndex = Random.Range(0, tarjetasSuerte.Length);
        TarjetaSuerte selectedCard = tarjetasSuerte[lastCardIndex];

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

        int destination = 0;

        if (name == "CC23")
        {
            int actual = player.playerMovement.waypointIndex;
            int distanceTo15 = (15 - actual + 40) % 40;
            int distanceTo36 = (36 - actual + 40) % 40;
            destination = (distanceTo15 < distanceTo36) ? 15 : 36;
        }
        else
        {
            destination = tarjetasSuerte[lastCardIndex].casillas;
        }

        if (destination != 0)
        {
            StartCoroutine(MovePlayerToPosition(player, tarjetasSuerte[lastCardIndex].casillas, playerIndex, playerStartWaypoint));
        }
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

    // Método para enviar al jugador a otra casilla
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
