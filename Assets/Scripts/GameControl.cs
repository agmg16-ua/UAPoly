using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    private static GameObject player1, player2;

    public static int diceSideThrown = 0;
    public static int player1StartWaypoint = 0;
    public static int player2StartWaypoint = 0;

    public static bool gameOver = false;
    // Start is called before the first frame update
    void Start()
    {
        player1 = GameObject.Find("Player1");
        player2 = GameObject.Find("Player2");

        player1.GetComponent<PlayerMove>().moveAllowed = false;
        player2.GetComponent<PlayerMove>().moveAllowed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(player1.GetComponent<PlayerMove>().waypointIndex > 
            player1StartWaypoint + diceSideThrown) 
        {
            player1.GetComponent<PlayerMove>().moveAllowed = false;
            player1StartWaypoint = player1.GetComponent<PlayerMove>().waypointIndex - 1;
        }

        if(player2.GetComponent<PlayerMove>().waypointIndex > 
            player2StartWaypoint + diceSideThrown) 
        {
            player2.GetComponent<PlayerMove>().moveAllowed = false;
            player2StartWaypoint = player2.GetComponent<PlayerMove>().waypointIndex - 1;
        }

        if(player1.GetComponent<PlayerMove>().waypointIndex == 
                       player1.GetComponent<PlayerMove>().waypoints.Length) {
            gameOver = true;
        }

        if(player2.GetComponent<PlayerMove>().waypointIndex == 
            player2.GetComponent<PlayerMove>().waypoints.Length) 
        {
            gameOver = true;
        }
        
    }

    public static void MovePlayer(int playerToMove) {
        switch(playerToMove) {
            case 1:
                player1.GetComponent<PlayerMove>().moveAllowed = true;
                break;
            case 2:
                player2.GetComponent<PlayerMove>().moveAllowed = true;
                break;
        }
    }   
}
