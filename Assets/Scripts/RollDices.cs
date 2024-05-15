using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class RollDices : MonoBehaviour
{

    public static int value = 0;

    private static GameObject Dado1, Dado2;

    private int whosTurn = 1;

    private bool coroutineAllowed = true;

    private int num_jugadores;

    // Start is called before the first frame update
    void Start()
    {
        Dado1 = GameObject.Find("Dado1");
        Dado2 = GameObject.Find("Dado2");
        num_jugadores = GameManager.instance.jugadores.Count;
    }

    // Update is called once per frame
    private void OnMouseDown()
    {
        if (!GameControl.gameOver && coroutineAllowed) {
            StartCoroutine("RollTheDice");
        }
    }

    private IEnumerator RollTheDice() {
        coroutineAllowed = false;
        value = 0;
        
        Dado1.GetComponent<Dice>().moveDice();
        Dado2.GetComponent<Dice>().moveDice();
        yield return new WaitForSeconds(3.0f);
        GameControl.diceSideThrown = value;
        UnityEngine.Debug.Log("Suma dados " +value);


        if (GameControl.jailTurns[whosTurn-1] > 0 && GameControl.inJail[whosTurn-1]) {
            GameControl.jailTurns[whosTurn-1]--;
            UnityEngine.Debug.Log("Turnos Player " + whosTurn + " in jail!: " + (GameControl.jailTurns[whosTurn-1]));
            if (GameControl.jailTurns[whosTurn-1] == 0) {
                UnityEngine.Debug.Log("Player " + whosTurn + " is out of jail!");
                GameControl.players[whosTurn-1].playerMovement.moveAllowed = true; // Reactivar el movimiento del jugador
                GameControl.inJail[whosTurn-1] = false;
                whosTurn = (whosTurn % num_jugadores) + 1;
            } else {
                whosTurn = (whosTurn % num_jugadores) + 1;
            }   
        } 
        else {
            GameControl.MovePlayer(whosTurn);
            UnityEngine.Debug.Log("Player " + whosTurn);
            whosTurn = (whosTurn % num_jugadores) + 1;
        }
        GameControl.restado[whosTurn-1] = false;





        /*if (whosTurn == 1)
        {
            if (GameControl.jailTurns[0] > 0 && GameControl.inJail[0])
            {
                GameControl.jailTurns[0]--;
                UnityEngine.Debug.Log("Turnos Player 1  in jail!: " + (GameControl.jailTurns[0]));
                if (GameControl.jailTurns[0] == 0)
                {
                    UnityEngine.Debug.Log("Player 1 is out of jail!");
                    GameControl.players[0].playerMovement.moveAllowed = true; // Reactivar el movimiento del jugador
                    GameControl.inJail[0] = false;
                    whosTurn = 2;
                }
                else
                {
                    whosTurn = 2;
                }
            }
            else
            {
                GameControl.MovePlayer(1);
                UnityEngine.Debug.Log("Player 1");
                whosTurn = 2;
                
            }
            GameControl.restado[0] = false;
        }
        else if (whosTurn == 2)
        {
            if (GameControl.jailTurns[1] > 0 && GameControl.inJail[1])
            {
                GameControl.jailTurns[1]--;
                UnityEngine.Debug.Log("Turnos Player 2 in Jail!: " + (GameControl.jailTurns[1]));
                if (GameControl.jailTurns[1] == 0)
                {
                    UnityEngine.Debug.Log("Player 2 is out of jail!");
                    GameControl.players[1].playerMovement.moveAllowed = true;// Reactivar el movimiento del jugador
                    GameControl.inJail[1] = false;
                    whosTurn = 3;
                }
                else
                {
                    whosTurn = 3;
                }
            }
            else
            {
                GameControl.MovePlayer(2);
                UnityEngine.Debug.Log("Player 2");
                whosTurn = 3;
            }
            GameControl.restado[1] = false;

        }
        else if(whosTurn == 3)
        {
            if (GameControl.jailTurns[2] > 0 && GameControl.inJail[2])
            {
                GameControl.jailTurns[2]--;
                UnityEngine.Debug.Log("Turnos Player 3 in Jail!: " + (GameControl.jailTurns[2]));
                if (GameControl.jailTurns[2] == 0)
                {
                    UnityEngine.Debug.Log("Player 3 is out of jail!");
                    GameControl.players[2].playerMovement.moveAllowed = true;// Reactivar el movimiento del jugador
                    GameControl.inJail[2] = false;
                    whosTurn = 4;
                }
                else
                {
                    whosTurn = 4;

                }
            }
            else
            {
                GameControl.MovePlayer(3);
                UnityEngine.Debug.Log("Player 3");
                whosTurn = 4;
            }
            GameControl.restado[2] = false;


        }
        else if( whosTurn == 4)
        {
            if (GameControl.jailTurns[3] > 0 && GameControl.inJail[3])
            {
                GameControl.jailTurns[3]--;
                UnityEngine.Debug.Log("Turnos Player 4 in Jail!: " + (GameControl.jailTurns[3]));
                if (GameControl.jailTurns[3] == 0)
                {
                    UnityEngine.Debug.Log("Player 4 is out of jail!");
                    GameControl.players[3].playerMovement.moveAllowed = true;// Reactivar el movimiento del jugador
                    GameControl.inJail[3] = false;
                    whosTurn = 1;
                }
                else
                {
                    whosTurn = 1;
                }
            }
            else
            {
                GameControl.MovePlayer(4);
                UnityEngine.Debug.Log("Player 4");
                whosTurn = 1;
            }
            GameControl.restado[3] = false;

        }*/
        coroutineAllowed = true;
    }
}
