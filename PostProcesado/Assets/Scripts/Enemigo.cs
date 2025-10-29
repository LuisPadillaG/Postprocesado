using UnityEngine;

public class Enemigo : MonoBehaviour
{
    Animator animator;
    float contador;
    public GameObject puntoCollisionEspada;
    public GameObject hitBoxEspada;
    float contadorDesactivarHitBoxEspada;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = this.transform.GetChild(0).GetComponent<Animator>();
        contador = 0;
        hitBoxEspada.SetActive(false);
        contadorDesactivarHitBoxEspada = 0;
    }

    // Update is called once per frame
    void Update()
    {
        contador += Time.deltaTime;
        if (contador > 5) {
            animator.SetInteger("Estado", 2);
        }

        if (contadorDesactivarHitBoxEspada > 0)
        {
            contadorDesactivarHitBoxEspada -= Time.deltaTime;
            if (contadorDesactivarHitBoxEspada <= 0)
            {
                hitBoxEspada.SetActive(false);
            }
        }
        hitBoxEspada.transform.position = puntoCollisionEspada.transform.position;
        hitBoxEspada.transform.rotation = puntoCollisionEspada.transform.rotation;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "DanoAEnemigo")
        {
            animator.SetInteger("Estado", 1);
        } 
    }
    public void Evento_ActivarHitBoxEspada()
    {
        hitBoxEspada.SetActive(true);
         
    }
    public void Evento_TerminoDeSerHerido()
    {
        Debug.Log("Recuperado siuuuu");
        animator.SetInteger("Estado", 0); 
        contador = 0;
    }
    public void Evento_TerminoEjecutarAtacar()
    {
        animator.SetInteger("Estado", 0);
        contadorDesactivarHitBoxEspada = 0.5f;
    } 
}
