using UnityEngine;

public class PropertyTile : MonoBehaviour
{
    public int price = 200;
    public GameObject owner;

    public void BuyProperty(GameObject newOwner)
    {
        owner = newOwner;
        newOwner.GetComponent<PlayerWallet>().subtractMoney(price);
        newOwner.GetComponent<PlayerWallet>().addLand(gameObject); // Pasar la GameObject de la propiedad
    }

    public void PayRent(GameObject player)
    {
        PlayerWallet playerWallet = player.GetComponent<PlayerWallet>();
        playerWallet.subtractMoney(50);
        owner.GetComponent<PlayerWallet>().addMoney(50);
        Debug.Log(player.name + " paid $50 rent to " + owner.name);
    }
}
