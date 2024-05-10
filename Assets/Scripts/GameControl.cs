using System.Collections;
using System.Collections.Generic;
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

    // Se llama al inicio del script
    void Start()
    {
        gameManager = GameManager.instance;
        players = new Player[gameManager.jugadores.Count];
        playerStartWaypoint = new int[gameManager.jugadores.Count];
        for (int i = 0; i < gameManager.jugadores.Count; i++)
        {
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
            
            if (playerStartWaypoint[i] + diceSideThrown > players[i].playerMovement.waypoints.Length - 1)  {
                 
                int waypointsNextLap = diceSideThrown - (players[i].playerMovement.waypoints.Length - 1 - playerStartWaypoint[i]);

                players[i].playerMovement.otraVuelta = true;

                if (players[i].playerMovement.waypointIndex == 0) {
                    playerStartWaypoint[i] = 0;
                    diceSideThrown = waypointsNextLap;
                    Debug.Log("Player " + (i + 1) + " has completed a lap");

                    // Añadir $200 a la cartera del jugador
                    PlayerWallet playerWallet = players[i].GetComponent<PlayerWallet>();
                    playerWallet.addMoney(200);
                    Debug.Log("Player " + (i + 1) + " received $200 for completing a lap. New balance: " + playerWallet.getWalletAmount());
                }                                             
            }


            //Debug.Log("Player " + (i + 1) + " is at waypoint " + players[i].GetComponent<PlayerMove>().waypointIndex);
            if (players[i].playerMovement.waypointIndex > playerStartWaypoint[i] + diceSideThrown)
            {
                players[i].playerMovement.moveAllowed = false;
                players[i].playerMovement.otraVuelta = false;
                playerStartWaypoint[i] = players[i].playerMovement.waypointIndex - 1;
            }

            if (players[i].playerMovement.waypointIndex == players[i].playerMovement.waypoints.Length)
            {
                gameOver = true;
            }
        }
    }

    // M?todo est?tico para mover al jugador especificado
    public static void MovePlayer(int playerToMove)
    {
        players[playerToMove - 1].playerMovement.moveAllowed = true;
    }
}

