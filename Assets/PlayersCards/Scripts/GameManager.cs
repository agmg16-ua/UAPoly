using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public List<Personajes> personajes;
    public List<Player> jugadores;

    public bool gameContinue;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }


    public void updateGameStatus(bool newStatus) { gameContinue = newStatus; }

    //private int count = 0;
    public bool getGameStatus()
    {
        //!!~~update winner
        //count++;
        //if (count > 100) {gameContinue = false;} //temp
        return gameContinue;
    }
}

