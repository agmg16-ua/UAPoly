using System.Collections.Generic;
using UnityEngine;

public class PropertyManager : MonoBehaviour
{
    // Lista de todas las propiedades del juego
    public List<PropertyTile> properties = new List<PropertyTile>();

    // Método para verificar si una propiedad está disponible para comprar
    public bool IsPropertyAvailable(PropertyTile property)
    {
        return property.owner == null;
    }

    // Método para comprar una propiedad
    public void BuyProperty(PropertyTile property, GameObject buyer)
    {
        if (IsPropertyAvailable(property))
        {
            // Si la propiedad está disponible, el comprador la compra
            property.owner = buyer;
            buyer.GetComponent<PlayerWallet>().subtractMoney(property.price);
            buyer.GetComponent<PlayerWallet>().addLand(property.gameObject);
            Debug.Log("Player " + buyer.name + " bought property " + property.name + " for $" + property.price);
        }
        else
        {
            Debug.Log("Property " + property.name + " is already owned!");
        }
    }

    // Método para pagar el alquiler de una propiedad
    public void PayRent(PropertyTile property, GameObject player)
    {
        if (!IsPropertyAvailable(property) && property.owner != player)
        {
            property.PayRent(player);
        }
    }
}
