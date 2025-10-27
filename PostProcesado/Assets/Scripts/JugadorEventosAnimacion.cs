using UnityEngine;

public class JugadorEventosAnimacion : MonoBehaviour
{
    Jugador scriptJugador;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scriptJugador = this.transform.parent.GetComponent<Jugador>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Evento_TerminaDisparo()
    {
        scriptJugador.Evento_TerminaDisparo();
        Debug.Log("Hola");
    }
    public void Evento_CrearBala(){
        scriptJugador.Evento_CrearBala();
    }
}
