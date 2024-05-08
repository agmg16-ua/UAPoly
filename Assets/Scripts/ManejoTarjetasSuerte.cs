using System.Collections;
using UnityEngine;

public class ManejoTarjetasSuerte : MonoBehaviour {

    // Array of dice sides sprites to load from Resources folder
    private Sprite[] tarjetasSuerte;

    // Reference to sprite renderer to change sprites
    private SpriteRenderer rendTarjetas;

    // Use this for initialization
    private void Start() {

        // Assign Renderer component
        rendTarjetas = GetComponent<SpriteRenderer>();

        // Load dice sides sprites to array from DiceSides subfolder of Resources folder
        tarjetasSuerte = Resources.LoadAll<Sprite>("TarjetasSuerte/");

        rendTarjetas.sprite = tarjetasSuerte[0];
    }

    // If you left click over the dice then RollTheDice coroutine is started
    public void OnMouseDown() {
        StartCoroutine("selectRandomCard");
    }

    // Coroutine that rolls the dice
    private IEnumerator selectRandomCard() {

        // Variable to contain random dice side number.
        // It needs to be assigned. Let it be 0 initially
        int randomCard = 0;

        // Final side or value that dice reads in the end of coroutine
        int finalCard = 0;

        // Loop to switch dice sides ramdomly
        // before final side appears. 20 itterations here.
        for (int i = 0; i <= 20; i++) {
            
            randomCard = Random.Range(1, 21);

            // Set sprite to upper face of dice from array according to random value
            rendTarjetas.sprite = tarjetasSuerte[randomCard];

            // Pause before next itteration
            yield return new WaitForSeconds(0.05f);
        }

        // Assigning final side so you can use this value later in your game
        // for player movement for example
        finalCard = randomCard;

        // Show final dice value in Console
        Debug.Log(finalCard);
    }
}
