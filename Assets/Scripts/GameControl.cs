using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameControl : MonoBehaviour
{
    // Referencias a los objetos de los jugadores
    private static GameObject[] players = new GameObject[4];

    // �ndices de inicio de los waypoints para cada jugador
    public static int[] playerStartWaypoint = new int[4];

    // N�mero de caras del dado arrojado
    public static int diceSideThrown = 0;

    // Booleano que indica si el juego ha terminado
    public static bool gameOver = false;

    // Se llama al inicio del script
    void Start()
    {
        // Encuentra los objetos de los jugadores y los almacena en un arreglo
        for (int i = 0; i < 4; i++)
        {
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
            if (players[i].GetComponent<PlayerMove>().waypointIndex > playerStartWaypoint[i] + diceSideThrown)
            {
                players[i].GetComponent<PlayerMove>().moveAllowed = false;
                playerStartWaypoint[i] = players[i].GetComponent<PlayerMove>().waypointIndex - 1;
            }

            if (players[i].GetComponent<PlayerMove>().waypointIndex == players[i].GetComponent<PlayerMove>().waypoints.Length)
            {
                gameOver = true;
            }
        }
    }

    // M�todo est�tico para mover al jugador especificado
    public static void MovePlayer(int playerToMove)
    {
        players[playerToMove - 1].GetComponent<PlayerMove>().moveAllowed = true;
    }
}

