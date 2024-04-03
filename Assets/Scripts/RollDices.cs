using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollDices : MonoBehaviour
{

    public static int value = 0;

    private static GameObject Dado1, Dado2;

    private int whosTurn = 1;

    private bool coroutineAllowed = true;

    // Start is called before the first frame update
    void Start()
    {
        Dado1 = GameObject.Find("Dado1");
        Dado2 = GameObject.Find("Dado2");
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

        if (whosTurn == 1) {
            GameControl.MovePlayer(1);
            Debug.Log("Player 1");
        } else if (whosTurn == -1) {
            GameControl.MovePlayer(2);
            Debug.Log("Player 2");
        }

        whosTurn *= -1;
        coroutineAllowed = true;
    }
}
