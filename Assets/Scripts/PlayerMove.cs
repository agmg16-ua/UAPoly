using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Transform[] waypoints;

    [SerializeField]
    private float moveSpeed = 3f;

    public bool otraVuelta = false;

    [HideInInspector]
    public int waypointIndex = 0;

    public bool moveAllowed = false;


    // Start is called before the first frame update
    private void Start()
    {
        transform.position = waypoints[waypointIndex].transform.position;    
    }

    // Update is called once per frame
    private void Update()
    {
        if(moveAllowed) {
            Move();
        }
    }

    private void Move() {
        if(waypointIndex <= waypoints.Length - 1) {
            transform.position = Vector2.MoveTowards(transform.position, 
                waypoints[waypointIndex].transform.position, 
                moveSpeed * Time.deltaTime);

            if(transform.position == waypoints[waypointIndex].transform.position) {
                waypointIndex += 1;
            }

            if((waypointIndex == waypoints.Length-1) && (otraVuelta == true) ) {
                waypointIndex = 0;
            }
        }
    }


    ////cobrar 200 en salida
    public void OnPassingGo()
    {
        // Obtén la instancia de PlayerWallet y aumenta el saldo en $200
        PlayerWallet playerWallet = GetComponent<PlayerWallet>();
        playerWallet.CollectPassingGoMoney();
    }


}
