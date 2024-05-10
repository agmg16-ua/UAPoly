using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    private GameManager gameManager;

    // Referencias a los objetos de los jugadores
    private static Player[] players;

    // indices de inicio de los waypoints para cada jugador
    public static int[] playerStartWaypoint;

    // Numero de caras del dado arrojado
    public static int diceSideThrown = 0;

    // Booleano que indica si el juego ha terminado
    public static bool gameOver = false;
    public static bool inJail = false;

    public static bool[] restado = new bool[4];
    // Contador de turnos en la cárcel para cada jugador
    public static int[] jailTurns = new int[4];

    public static int bote = 10;
    // Se llama al inicio del script
    void Start()
    {
        gameManager = GameManager.instance;
        players = new Player[gameManager.jugadores.Count];
        playerStartWaypoint = new int[gameManager.jugadores.Count];
        for (int i = 0; i < gameManager.jugadores.Count; i++)
        {
            restado[i] = false;
            players[i] = gameManager.jugadores[i];
            playerStartWaypoint[i] = 0;
            players[i].playerMovement.moveAllowed = false;
            players[i].playerMovement.InitializeWaypoints();

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
        // Verifica si cada jugador ha alcanzado su waypoint objetivo
        for (int i = 0; i < gameManager.jugadores.Count; i++)
        {

            if ((playerStartWaypoint[i]+1) + diceSideThrown == players[i].GetComponent<PlayerMove>().waypointIndex && players[i].playerMovement.waypointIndex == 31)
            {
                StartCoroutine(SendToJail(i));
            }
            else if (((playerStartWaypoint[i]) + diceSideThrown == players[i].GetComponent<PlayerMove>().waypointIndex && players[i].GetComponent<PlayerMove>().waypointIndex == 4) || ((playerStartWaypoint[i]) + diceSideThrown == players[i].GetComponent<PlayerMove>().waypointIndex && players[i].GetComponent<PlayerMove>().waypointIndex == 38))
            {
                StartCoroutine(restarImpuesto(i, 100));
                
            }
            else if ((playerStartWaypoint[i]) + diceSideThrown == players[i].GetComponent<PlayerMove>().waypointIndex && players[i].GetComponent<PlayerMove>().waypointIndex == 20)
            {
                StartCoroutine(sumarBote(i));
            }
            else if ((playerStartWaypoint[i]+1) + diceSideThrown > players[i].GetComponent<PlayerMove>().waypoints.Length - 1)
            {
                int waypointsNextLap = diceSideThrown - (players[i].playerMovement.waypoints.Length - 1 - playerStartWaypoint[i]);

                players[i].playerMovement.otraVuelta = true;

                if (players[i].playerMovement.waypointIndex == 0) {
                if (players[i].GetComponent<PlayerMove>().waypointIndex == 0)
                {
                    playerStartWaypoint[i] = 0;
                    diceSideThrown = waypointsNextLap;
                    UnityEngine.Debug.Log("Player " + (i + 1) + " has completed a lap");

                    // Añadir $200 a la cartera del jugador
                    PlayerWallet playerWallet = players[i].GetComponent<PlayerWallet>();
                    playerWallet.addMoney(200);
                    UnityEngine.Debug.Log("Player " + (i + 1) + " received $200 for completing a lap. New balance: " + playerWallet.getWalletAmount());
                }
            }

            //Debug.Log("Player " + (i + 1) + " is at waypoint " + players[i].GetComponent<PlayerMove>().waypointIndex);
            if (players[i].playerMovement.waypointIndex > playerStartWaypoint[i] + diceSideThrown)
            else if (players[i].GetComponent<PlayerMove>().waypointIndex > playerStartWaypoint[i] + diceSideThrown)
            {
                players[i].playerMovement.moveAllowed = false;
                players[i].playerMovement.otraVuelta = false;
                playerStartWaypoint[i] = players[i].playerMovement.waypointIndex - 1;
            }

            else if (players[i].GetComponent<PlayerMove>().waypointIndex == players[i].GetComponent<PlayerMove>().waypoints.Length)
            {
                gameOver = true;
            }
        }
    }

    // Método estático para mover al jugador especificado
    public static void MovePlayer(int playerToMove)
    {
        players[playerToMove - 1].GetComponent<PlayerMove>().moveAllowed = true;
        if (players[playerToMove - 1].GetComponent<PlayerMove>().waypointIndex == 20)
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
        players[playerIndex].GetComponent<PlayerMove>().waypointIndex = 10;

        // Desactiva el movimiento del jugador
        players[playerIndex].GetComponent<PlayerMove>().moveAllowed = false;

        // Establece el contador de turnos en la cárcel a 3
        jailTurns[playerIndex] = 3;
        inJail = true;

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
            PlayerWallet playerWallet = players[playerIndex].GetComponent<PlayerWallet>();
            playerWallet.subtractMoney(100);
            UnityEngine.Debug.Log("Player " + (playerIndex + 1) + " lost $100. New balance: $" + playerWallet.getWalletAmount());

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
            PlayerWallet playerWallet = players[playerIndex].GetComponent<PlayerWallet>();
            playerWallet.addMoney(bote);
            UnityEngine.Debug.Log("Player " + (playerIndex + 1) + " won the pot! Added $" + bote + " to their wallet. New balance: $" + playerWallet.getWalletAmount());

            // Reiniciar el bote
            bote = 0;
        }
        restado[playerIndex] = true;

        // Espera tres segundos en tiempo de juego antes de reactivar el movimiento
        // Espera tres turnos antes de reactivar el movimiento
        yield return new WaitForSeconds(3 * Time.deltaTime * 60);
    }
}
