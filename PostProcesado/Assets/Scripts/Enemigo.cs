using UnityEngine;

public class Enemigo : MonoBehaviour
{
    Animator animator;
    float contador;
    public GameObject puntoCollisionEspada;
    public GameObject hitBoxEspada;
    float contadorDesactivarHitBoxEspada;
    int vida;
    AudioSource audioSource;
    public AudioClip sonidoAtacar;
    public AudioClip sonidoHerido;
    public 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = this.transform.GetChild(0).GetComponent<Animator>();
        contador = 0;
        hitBoxEspada.SetActive(false);
        contadorDesactivarHitBoxEspada = 0;
        vida = 3;
        audioSource = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        contador += Time.deltaTime;
        if (contador > 5 && vida > 0) {
            animator.SetInteger("Estado", 2);
            contador = 0;

            audioSource.clip = sonidoAtacar;
            audioSource.Play();
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
        if(vida > 0)
        {
            if (collision.gameObject.tag == "DanoAEnemigo")
            {
                animator.SetInteger("Estado", 1);
                vida--;
                if (vida <= 0)
                {
                    animator.SetTrigger("Muerte");
                }
            }
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
