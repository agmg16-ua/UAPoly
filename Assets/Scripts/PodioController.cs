using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PodioController : MonoBehaviour
{
    public static PodioController instance;
    public List<Player> orden;

    public void Awake(){
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
        orden = new List<Player>();
    }
    
    public void gameOverPlayer(Player player){
        for(int i = 0; i<orden.Count; i++){
            orden.Add(orden[i]);
        }
        
        if(orden.Count == 0){
            orden.Add(player);
        }
        else{
            orden[0] = player;
        }
        
        for(int i = 0; i<GameManager.instance.jugadores.Count; i++){
            if(GameControl.players[i].nombre == player.nombre){
                Destroy(GameControl.players[i].gameObject);
            }
        }

        UnityEngine.Debug.Log("Se ha destruido");
    }
}