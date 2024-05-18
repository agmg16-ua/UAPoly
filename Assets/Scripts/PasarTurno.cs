using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasarTurno : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnMouseDown() {
        GameControl.pasarTurno();
    }
}
