using UnityEngine;

public class EnemigoEventos : MonoBehaviour
{
    public Enemigo enemigo;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Evento_TerminoDeSerHerido()
    {
        enemigo.Evento_TerminoDeSerHerido();
    }
    public void Evento_TerminoEjecutarAtacar()
    {
        enemigo.Evento_TerminoEjecutarAtacar();
    }
    public void Evento_ActivarHitBoxEspada()
    {
        enemigo.Evento_ActivarHitBoxEspada();
    }
}
