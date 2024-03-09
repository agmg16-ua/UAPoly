using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NuevoPersonaje", menuName = "Personaje", order = 0)]
public class Personajes : ScriptableObject {
    public GameObject personaje;
    public string nombre;
    public Sprite imagen;
}
