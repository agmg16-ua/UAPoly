using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ManejoTarjetas : MonoBehaviour
{
    public List<Tarjeta> tarjetasSuerte;
    public List<Tarjeta> tarjetasCajaComunidad;

    public class Tarjeta {
        public string texto;
        public Action accion;
    }

    public delegate void Action();

    public void ObtenerTarjetaSuerte() {
        int index = Random.Range(0, tarjetasSuerte.Count);
        Tarjeta tarjeta = tarjetasSuerte[index];
        EjecutarAccion(tarjeta);
    }

    public void ObtenerTarjetaCajaComunidad() {
        int index = Random.Range(0, tarjetasCajaComunidad.Count);
        Tarjeta tarjeta = tarjetasCajaComunidad[index];
        EjecutarAccion(tarjeta);
    }

    void EjecutarAccion(Tarjeta tarjeta) {
        Debug.Log("Obtuviste la tarjeta: " + tarjeta.texto);
        tarjeta.accion?.Invoke();
    }

}
