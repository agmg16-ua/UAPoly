using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerWallet : MonoBehaviour
{//this will store all the information a player has regarding money and properties
	private int myWallet;
	//public ArrayList myLandList;
	private List<GameObject> myLandList;
	public GameManager monopolyGame;
	public bool isBankrupt;
	public bool restado = false;


	// Use this for initialization
	void Start()
	{
		isBankrupt = false;
		myWallet = 1500;
		myLandList = new List<GameObject>();
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	public void CollectPassingGoMoney()
	{
		myWallet += 200;
		Debug.Log("Player received $200 for passing Go. New balance: " + myWallet);
	}

	public int getWalletAmount()
	{
		print("Player has this much money: " + myWallet);
		return myWallet;
	}
	public void setWalletAmount(int newBalance)
	{
		print("Setting Wallet Amount: " + newBalance);
		myWallet = newBalance;
		print("Wallet amount: " + myWallet);
	}
	public void addMoney(int moola)
	{
		myWallet = myWallet + moola;
	}

	public void subtractMoney(int debt)
	{

		myWallet = myWallet - debt;
		
	}
	public void addLand(GameObject newLand)
	{
		myLandList.Add(newLand); // Agregar la propiedad a la lista
	}
	/*public void addLand(ArrayList newLand)
	{
		myLandList.Add(newLand);
	}*/
	//something to manipulate landList

}
/*
 * // Supongamos que este m?todo se llama cuando el jugador pasa por la casilla de salida
void OnPlayerPassingGo() {
    // Obt?n la instancia de PlayerWallet del jugador actual
    PlayerWallet playerWallet = currentPlayer.GetComponent<PlayerWallet>();

    // A?ade 200 a la cartera del jugador
    playerWallet.CollectPassingGoMoney();
}
 */