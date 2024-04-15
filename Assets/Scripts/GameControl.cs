using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameControl : MonoBehaviour
{
    // Referencias a los objetos de los jugadores
    private static GameObject[] players = new GameObject[4];

    // ?ndices de inicio de los waypoints para cada jugador
    public static int[] playerStartWaypoint = new int[4];

    // N?mero de caras del dado arrojado
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
            playerStartWaypoint[i] = 30;
            players[i].GetComponent<PlayerMove>().moveAllowed = false;
        }
    }

    // Se llama una vez por fotograma
    void Update()
    {
        // Verifica si cada jugador ha alcanzado su waypoint objetivo
        for (int i = 0; i < 4; i++)
        {
            
            if (playerStartWaypoint[i] + diceSideThrown > players[i].GetComponent<PlayerMove>().waypoints.Length - 1)  {
                
                int waypointsNextLap = diceSideThrown - (players[i].GetComponent<PlayerMove>().waypoints.Length - 1 - playerStartWaypoint[i]);

                players[i].GetComponent<PlayerMove>().otraVuelta = true;

                if (players[i].GetComponent<PlayerMove>().waypointIndex == 0) {
                    playerStartWaypoint[i] = 0;
                    diceSideThrown = waypointsNextLap;
                    Debug.Log("Player " + (i + 1) + " has completed a lap");
                }                                             
            }
            //Debug.Log("Player " + (i + 1) + " is at waypoint " + players[i].GetComponent<PlayerMove>().waypointIndex);
            if (players[i].GetComponent<PlayerMove>().waypointIndex > playerStartWaypoint[i] + diceSideThrown)
            {
                players[i].GetComponent<PlayerMove>().moveAllowed = false;
                players[i].GetComponent<PlayerMove>().otraVuelta = false;
                playerStartWaypoint[i] = players[i].GetComponent<PlayerMove>().waypointIndex - 1;
            }

            if (players[i].GetComponent<PlayerMove>().waypointIndex == players[i].GetComponent<PlayerMove>().waypoints.Length)
            {
                gameOver = true;
            }
        }
    }

    // M?todo est?tico para mover al jugador especificado
    public static void MovePlayer(int playerToMove)
    {
        players[playerToMove - 1].GetComponent<PlayerMove>().moveAllowed = true;

        /* Obtener la posici?n de la casilla de salida
        Vector3 salidaPosition = new Vector3(// coordenadas de la casilla de salida );

        // Obtener la posici?n actual del jugador
        Vector3 playerPosition = players[playerToMove - 1].transform.position;

        // Verificar si el jugador est? pasando por la casilla de salida (supongamos que si est? a una cierta distancia)
        float distanciaUmbral = // establecer la distancia umbral adecuada ;
        if (Vector3.Distance(playerPosition, salidaPosition) < distanciaUmbral)
        {
            // Llamar al m?todo para manejar el paso por la salida
            players[playerToMove - 1].GetComponent<PlayerMove>().OnPassingGo();
        }*/

        // Verifica si el jugador est? en el waypoint de salida (index 0)
        if (players[playerToMove - 1].GetComponent<PlayerMove>().waypointIndex == 0)
        {
            // Llama al m?todo para manejar el paso por la salida
            players[playerToMove - 1].GetComponent<PlayerMove>().OnPassingGo();
        }
    }
}

