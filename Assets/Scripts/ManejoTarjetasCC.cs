using System.Collections;
using UnityEngine;

public class ManejoTarjetasCC : MonoBehaviour {

    // Array of dice sides sprites to load from Resources folder
    private Sprite[] tarjetasCC;

    // Reference to sprite renderer to change sprites
    private SpriteRenderer rendTarjetasCC;

    // Use this for initialization
    private void Start() {

        // Assign Renderer component
        rendTarjetasCC = GetComponent<SpriteRenderer>();

        // Load dice sides sprites to array from DiceSides subfolder of Resources folder
        tarjetasCC = Resources.LoadAll<Sprite>("TarjetasComunidad/");

        rendTarjetasCC.sprite = tarjetasCC[0];
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
            // Pick up random value from 0 to 5 (All inclusive)
            randomCard = Random.Range(1, 23);

            // Set sprite to upper face of dice from array according to random value
            rendTarjetasCC.sprite = tarjetasCC[randomCard];

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

