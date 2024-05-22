using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;


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

    public static int bote = 10;
    public static PropertyManager propertyManager;


    // Referencia al Text y Button para la entrada de usuario
    public Text userInputText;
    public Button confirmButton;

    // Variables internas
    private int currentPlayerIndex;
    private PropertyTile currentPropertyTile;



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
        propertyManager = GetComponent<PropertyManager>();
    }

    // Se llama una vez por fotograma
    void Update()
    {
        // Verifica si cada jugador ha alcanzado su waypoint objetivo
        for (int i = 0; i < 4; i++)
        {
            //compras
            if ((playerStartWaypoint[i] + diceSideThrown) < players[i].GetComponent<PlayerMove>().waypoints.Length)
            {
                GameObject currentWaypoint = players[i].GetComponent<PlayerMove>().waypoints[playerStartWaypoint[i] + diceSideThrown].gameObject;

                // Verificar si el waypoint actual es una propiedad
                if (currentWaypoint.GetComponent<PropertyTile>() != null)
                {
                    // Verificar si el jugador puede comprar la propiedad
                    CheckProperty(i, currentWaypoint);
                }
            }


            else if ((playerStartWaypoint[i]+1) + diceSideThrown == players[i].GetComponent<PlayerMove>().waypointIndex && players[i].GetComponent<PlayerMove>().waypointIndex == 31)
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



    // Método para mostrar opciones de compra de propiedades
    public void ShowBuyPropertyOption(int playerIndex, int propertyPrice)
    {
        userInputTextMesh.text = "Player " + (playerIndex + 1) + ", do you want to buy this property for $" + propertyPrice + "? (y/n)";
        confirmButton.gameObject.SetActive(true);
        currentPlayerIndex = playerIndex;
    }

    // Método llamado cuando se presiona el botón de confirmar
    public void OnConfirmButtonClicked()
    {
        string input = userInputTextMesh.text.ToLower();

        if (input == "y")
        {
            propertyManager.BuyProperty(currentPropertyTile, players[currentPlayerIndex]);
        }
        else
        {
            propertyManager.PayRent(currentPropertyTile, players[currentPlayerIndex]);
        }

        userInputTextMesh.text = "";
        confirmButton.gameObject.SetActive(false);
    }

    // Método para verificar si el jugador ha caído en una propiedad disponible para la compra
    public void CheckProperty(int playerIndex, GameObject property)
    {
        PropertyTile propertyTile = property.GetComponent<PropertyTile>();

        if (propertyTile.owner == null)
        {
            ShowBuyPropertyOption(playerIndex, propertyTile.price);
        }
        else
        {
            propertyManager.PayRent(propertyTile, players[playerIndex]);
        }

        currentPropertyTile = propertyTile;
    }
    /*
    // Método para mostrar opciones de compra de propiedades
    public static void ShowBuyPropertyOption(int playerIndex, int propertyPrice)
    {
        UnityEngine.Debug.Log("Player " + (playerIndex + 1) + ", do you want to buy this property for $" + propertyPrice + "? (y/n)");
    }
    // Método para leer la entrada del usuario desde la consola
    public static bool ReadUserInput()
    {
        string input = Console.ReadLine();
        return input.Trim().ToLower() == "y"; // Devuelve true si el usuario ingresa "y", false en caso contrario
    }
    // Método para verificar si el jugador ha caído en una propiedad disponible para la compra
    public static void CheckProperty(int playerIndex, GameObject property)
    {
        PropertyTile propertyTile = property.GetComponent<PropertyTile>();

        // Verificar si la propiedad tiene un propietario
        if (propertyTile.owner == null)
        {
            // Mostrar opción de compra al jugador
            ShowBuyPropertyOption(playerIndex, propertyTile.price);

            // Leer la entrada del jugador
            bool wantsToBuy = ReadUserInput();

            if (wantsToBuy)
            {
                // Comprar la propiedad
                propertyManager.BuyProperty(propertyTile, players[playerIndex]);
            }
        }
        else
        {
            // Pagar alquiler
            propertyManager.PayRent(propertyTile, players[playerIndex]);
        }
    }*/


}
