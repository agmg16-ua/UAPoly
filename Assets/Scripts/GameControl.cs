using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    private PodioController podio;

    private CardDatabase cardDatabase;

    private GameManager gameManager;

    // Referencias a los objetos de los jugadores
    public static Player[] players;

    // indices de inicio de los waypoints para cada jugador
    public static int[] playerStartWaypoint;

    // Numero de caras del dado arrojado
    public static int diceSideThrown = 0;

    // Booleano que indica si el juego ha terminado
    public static bool gameOver = false;
    public static bool[] inJail = new bool[4];

    public static bool[] restado = new bool[4];
    // Contador de turnos en la cárcel para cada jugador
    public static int[] jailTurns = new int[4];

    // Componente para las Tarjetas de Suerte/Comunidad
    public ManejoTarjetasSuerte manejoTarjetasSuerte;
    public ManejoTarjetasCC manejoTarjetasCC;

    public static int bote = 10;

    public static int whosTurn = 1;
    public static int num_jugadores;

    // Se llama al inicio del script
    void Start()
    {
        gameManager = GameManager.instance;
        cardDatabase = CardDatabase.instance;
        podio = PodioController.instance;

        players = new Player[gameManager.jugadores.Count];
        playerStartWaypoint = new int[gameManager.jugadores.Count];
        for (int i = 0; i < gameManager.jugadores.Count; i++)
        {
            num_jugadores = gameManager.jugadores.Count;
            restado[i] = false;
            inJail[i] = false;
            players[i] = gameManager.jugadores[i];
            playerStartWaypoint[i] = 0;
            players[i].playerMovement.moveAllowed = false;
            players[i].playerMovement.InitializeWaypoints();
            players[i].turnosRestantesCarcel = 0;

            // Añade un componente SpriteRenderer al GameObject del jugador
            SpriteRenderer spriteRenderer = players[i].gameObject.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = players[i].personaje.imagen;

            // Define el tamaño del sprite
            float spriteSize = 1.0f; // Cambia esto al tamaño que desees
            players[i].transform.localScale = new Vector3(spriteSize, spriteSize, 1);

            // Asegúrate de que el GameObject está en una posición donde la cámara pueda verlo
            players[i].transform.position = players[i].playerMovement.waypoints[0].position;
        }
    }

    // Se llama una vez por fotograma
    void Update()
    {
        if(players[0].money == 0){
            UnityEngine.Debug.Log("Voy a entrar en el gameOver");
            podio.gameOverPlayer(players[0]);
            UnityEngine.Debug.Log("YATA");
        }

        // Verifica si cada jugador ha alcanzado su waypoint objetivo
        for (int i = 0; i < gameManager.jugadores.Count; i++)
        {
            players[i].turnosRestantesCarcel = jailTurns[i];
            if ((playerStartWaypoint[i] + 1) + diceSideThrown == players[i].playerMovement.waypointIndex && (players[i].playerMovement.waypointIndex == 37 || players[i].playerMovement.waypointIndex == 23 || players[i].playerMovement.waypointIndex == 8))
            {
                // Selecciona una tarjeta de suerte aleatoria
                StartCoroutine(manejoTarjetasSuerte.selectRandomCard());

                // Obtiene el valor de la tarjeta seleccionada
                int cardIndex = manejoTarjetasSuerte.GetLastCardIndex();
                int dinero = manejoTarjetasSuerte.GetLastCardMoney();
                int casillas = manejoTarjetasSuerte.GetLastCardSpaces();

                // Aplica los efectos de la tarjeta al jugador actual
                if (dinero > 0)
                {
                    players[i].wallet.addMoney(dinero);
                }
                if (dinero < 0)
                {
                    players[i].wallet.subtractMoney(dinero);
                }
                if (casillas != 0)
                {
                    players[i].playerMovement.waypointIndex = casillas;
                }
            }

            if ((playerStartWaypoint[i] + 1) + diceSideThrown == players[i].playerMovement.waypointIndex && (players[i].playerMovement.waypointIndex == 3 || players[i].playerMovement.waypointIndex == 18 || players[i].playerMovement.waypointIndex == 34))
            {
                // Selecciona una tarjeta de suerte aleatoria
                StartCoroutine(manejoTarjetasCC.selectRandomCard());

                // Obtiene el valor de la tarjeta seleccionada
                int cardIndex = manejoTarjetasCC.GetLastCardIndex();
                int dinero = manejoTarjetasCC.GetLastCardMoney();
                int casillas = manejoTarjetasCC.GetLastCardSpaces();
                string name = manejoTarjetasCC.GetLastCardName();

                // Aplica los efectos de la tarjeta al jugador actual
                if (dinero > 0)
                {
                    players[i].wallet.addMoney(dinero);
                }
                if (dinero < 0)
                {
                    players[i].wallet.subtractMoney(dinero);
                }

                int destination = 0;

                if (name == "CC23")
                {
                    int actual = players[i].playerMovement.waypointIndex;
                    int distanceTo15 = (15 - actual + 40) % 40;
                    int distanceTo36 = (36 - actual + 40) % 40;
                    destination = (distanceTo15 < distanceTo36) ? 15 : 36;
                } else
                {
                    destination = casillas;
                }
                if(destination != 0)
                {
                    players[i].playerMovement.waypointIndex = destination;
                }
            }

            if ((playerStartWaypoint[i]+1) + diceSideThrown == players[i].playerMovement.waypointIndex && players[i].playerMovement.waypointIndex == 31)
            {
                StartCoroutine(SendToJail(i));
            }
            else if (((playerStartWaypoint[i]) + diceSideThrown == players[i].playerMovement.waypointIndex && players[i].playerMovement.waypointIndex == 4) || ((playerStartWaypoint[i]) + diceSideThrown == players[i].playerMovement.waypointIndex && players[i].playerMovement.waypointIndex == 38))
            {
                StartCoroutine(restarImpuesto(i, 100));
                
            }
            else if ((playerStartWaypoint[i]) + diceSideThrown == players[i].playerMovement.waypointIndex && players[i].playerMovement.waypointIndex == 20)
            {
                StartCoroutine(sumarBote(i));
            }
            else if ((playerStartWaypoint[i]+1) + diceSideThrown > players[i].playerMovement.waypoints.Length - 1)
            {
                int waypointsNextLap = diceSideThrown - (players[i].playerMovement.waypoints.Length - 1 - playerStartWaypoint[i]);

                players[i].playerMovement.otraVuelta = true;

                if (players[i].playerMovement.waypointIndex == 0)
                {
                    playerStartWaypoint[i] = 0;
                    diceSideThrown = waypointsNextLap;
                    UnityEngine.Debug.Log("Player " + (i + 1) + " has completed a lap");

                    // Añadir $200 a la cartera del jugador
                    players[i].wallet.addMoney(200);
                    UnityEngine.Debug.Log("Player " + (i + 1) + " received $200 for completing a lap. New balance: " + players[i].wallet.getWalletAmount());
                }
            }

            //Debug.Log("Player " + (i + 1) + " is at waypoint " + players[i].playerMovement.waypointIndex);
            if (players[i].playerMovement.waypointIndex > playerStartWaypoint[i] + diceSideThrown){
                players[i].playerMovement.moveAllowed = false;
                players[i].playerMovement.otraVuelta = false;
                playerStartWaypoint[i] = players[i].playerMovement.waypointIndex - 1;
            }
            else if (players[i].playerMovement.waypointIndex == players[i].playerMovement.waypoints.Length)
            {
                gameOver = true;
            }
        }
    }

    // Método estático para mover al jugador especificado
    public static void MovePlayer(int playerToMove)
    {
        players[playerToMove - 1].playerMovement.moveAllowed = true;
        if (players[playerToMove - 1].playerMovement.waypointIndex == 20)
        {
            bote = 10;
        }
    }

    // Método para enviar al jugador a la cárcel
    private IEnumerator SendToJail(int playerIndex)
    {
        UnityEngine.Debug.Log("Player " + (playerIndex + 1) + " is in jail!");
        // Mueve al jugador a la casilla 10
        playerStartWaypoint[playerIndex] = 10;
        players[playerIndex].playerMovement.waypointIndex = 10;

        // Desactiva el movimiento del jugador
        players[playerIndex].playerMovement.moveAllowed = false;

        // Establece el contador de turnos en la cárcel a 3
        jailTurns[playerIndex] = 3;
        //players[playerIndex].turnosRestantesCarcel = 3;
        inJail[playerIndex] = true;

        // Espera tres segundos en tiempo de juego antes de reactivar el movimiento
        // Espera tres turnos antes de reactivar el movimiento
        yield return new WaitForSeconds(3 * Time.deltaTime * 60);
    }

    // Método para restar impuestos
    private IEnumerator restarImpuesto(int playerIndex, int amount)
    {
        if(restado[playerIndex]== false)
        {
            // Restar $100 de la cartera del jugador
            players[playerIndex].wallet.subtractMoney(100);
            UnityEngine.Debug.Log("Player " + (playerIndex + 1) + " lost $100. New balance: $" + players[playerIndex].wallet.getWalletAmount());

            // Agrega la cantidad del impuesto al bote
            bote += amount;
            UnityEngine.Debug.Log("Added $" + amount + " to the pot. Current pot: $" + bote);

        }
        restado[playerIndex] = true;

        // Espera tres segundos en tiempo de juego antes de reactivar el movimiento
        // Espera tres turnos antes de reactivar el movimiento
        yield return new WaitForSeconds(3 * Time.deltaTime * 60);
    }


    // Método para sumar bote
    private IEnumerator sumarBote(int playerIndex)
    {
        if (restado[playerIndex] == false)
        {
            // Restar $100 de la cartera del jugador
            players[playerIndex].wallet.addMoney(bote);
            UnityEngine.Debug.Log("Player " + (playerIndex + 1) + " won the pot! Added $" + bote + " to their wallet. New balance: $" + players[playerIndex].wallet.getWalletAmount());

            // Reiniciar el bote
            bote = 0;
        }
        restado[playerIndex] = true;

        // Espera tres segundos en tiempo de juego antes de reactivar el movimiento
        // Espera tres turnos antes de reactivar el movimiento
        yield return new WaitForSeconds(3 * Time.deltaTime * 60);
    }

    // Método para pasar al siguiente turno
    public static void pasarTurno()
    {
        // Cambia el turno al siguiente jugador
        if (players[whosTurn-1].haTirado) {
            players[whosTurn-1].haTirado = false;
            whosTurn = (whosTurn % num_jugadores) + 1;
            UnityEngine.Debug.Log("Now it's Player " + whosTurn + "'s turn!");

            for (int i = 0; i < players.Length; i++) {
                if (inJail[i]) {
                    players[i].turnosRestantesCarcel--;
                }
            }
        }
        else {
            UnityEngine.Debug.Log("Player " + whosTurn + " must roll the dice first!");
        }
        
        CardDatabase.updateCardList(whosTurn-1,false);
    }
}