using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    // Referencias a los objetos de los jugadores
    public static GameObject[] players = new GameObject[4];

    // Índices de inicio de los waypoints para cada jugador
    public static int[] playerStartWaypoint = new int[4];

    // Número de caras del dado arrojado
    public static int diceSideThrown = 0;

    // Booleano que indica si el juego ha terminado
    public static bool gameOver = false;
    public static bool inJail = false;

    public static bool[] restado = new bool[4];
    // Contador de turnos en la cárcel para cada jugador
    public static int[] jailTurns = new int[4];

    // Se llama al inicio del script
    void Start()
    {
        // Encuentra los objetos de los jugadores y los almacena en un arreglo
        for (int i = 0; i < 4; i++)
        {
            restado[i] = false;
            players[i] = GameObject.Find("Player" + (i + 1));
            playerStartWaypoint[i] = 0;
            players[i].GetComponent<PlayerMove>().moveAllowed = false;
        }
    }

    // Se llama una vez por fotograma
    void Update()
    {
        // Verifica si cada jugador ha alcanzado su waypoint objetivo
        for (int i = 0; i < 4; i++)
        {

            if ((playerStartWaypoint[i]+1) + diceSideThrown == players[i].GetComponent<PlayerMove>().waypointIndex && players[i].GetComponent<PlayerMove>().waypointIndex == 31)
            {
                StartCoroutine(SendToJail(i));
            }
            else if (((playerStartWaypoint[i]) + diceSideThrown == players[i].GetComponent<PlayerMove>().waypointIndex && players[i].GetComponent<PlayerMove>().waypointIndex == 4) || ((playerStartWaypoint[i]) + diceSideThrown == players[i].GetComponent<PlayerMove>().waypointIndex && players[i].GetComponent<PlayerMove>().waypointIndex == 38))
            {
                StartCoroutine(restarImpuesto(i));
            }
            else if ((playerStartWaypoint[i]+1) + diceSideThrown > players[i].GetComponent<PlayerMove>().waypoints.Length - 1)
            {
                int waypointsNextLap = diceSideThrown - (players[i].GetComponent<PlayerMove>().waypoints.Length - 1 - playerStartWaypoint[i]);

                players[i].GetComponent<PlayerMove>().otraVuelta = true;

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
            else if (players[i].GetComponent<PlayerMove>().waypointIndex > playerStartWaypoint[i] + diceSideThrown)
            {
                players[i].GetComponent<PlayerMove>().moveAllowed = false;
                players[i].GetComponent<PlayerMove>().otraVuelta = false;
                playerStartWaypoint[i] = players[i].GetComponent<PlayerMove>().waypointIndex - 1;
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

    // Método para enviar al jugador a la cárcel
    private IEnumerator restarImpuesto(int playerIndex)
    {
        if(restado[playerIndex]== false)
        {
            // Restar $100 de la cartera del jugador
            PlayerWallet playerWallet = players[playerIndex].GetComponent<PlayerWallet>();
            playerWallet.subtractMoney(100);
            UnityEngine.Debug.Log("Player " + (playerIndex + 1) + " lost $100. New balance: $" + playerWallet.getWalletAmount());
        }
        restado[playerIndex] = true;

        // Espera tres segundos en tiempo de juego antes de reactivar el movimiento
        // Espera tres turnos antes de reactivar el movimiento
        yield return new WaitForSeconds(3 * Time.deltaTime * 60);
    }
}
