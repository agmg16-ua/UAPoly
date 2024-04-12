using UnityEngine;
using System.Collections;

public class PlayerWallet : MonoBehaviour
{//this will store all the information a player has regarding money and properties
	private int myWallet;
	public ArrayList myLandList;
	public GameManager monopolyGame;
	public bool isBankrupt;


	// Use this for initialization
	void Start()
	{
		isBankrupt = false;
		myWallet = 1500;
		myLandList = new ArrayList();
	}

	// Update is called once per frame
	void Update()
	{
		//~~@!!constantly check to see if you go backrupt, if at anypoint in debt, end game
		if (myWallet < 0)
		{
			//print ("stop! stop! stop!");
			isBankrupt = true;
			monopolyGame.updateGameStatus(false);
		}
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

	public void addLand(ArrayList newLand)
	{
		myLandList.Add(newLand);
	}
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