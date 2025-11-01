using UnityEngine;

public class Jugador : MonoBehaviour
{
    Vector3 posicion, rotacion;
    Animator animator;
    int estado_anterior;
    int estado_actual;
    public GameObject prefabBala;
    public Transform puntoCreacionBala;
    float contadorCancelarAtaque;
    public GameObject LugarHitBoxEspada;
    public GameObject hitBoxEspada;

    float contador_activar_hitboxespada;
    int estado_hitboxespada;
    float contador_recuperarse_deherido;
    int vida;
    AudioSource audioSource;
    public AudioClip sonidoSlashEspada;
    public AudioClip sonidoHerido;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        posicion = Vector3.zero;
        rotacion = Vector3.zero;
        animator = this.transform.GetChild(0).GetComponent<Animator>();
        estado_anterior = 0;
        estado_anterior = 0;
        contadorCancelarAtaque = 0;
        estado_hitboxespada = 0;
        hitBoxEspada.SetActive(false);
        contador_recuperarse_deherido = 0;
        vida = 3;
        audioSource = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(estado_actual != 5)
        {
            if(estado_actual != 3)
            {
                posicion.x += Input.GetAxis("Horizontal") * Time.deltaTime * 3;
            }
            if (contador_recuperarse_deherido > 0)
            {
                contador_recuperarse_deherido -= Time.deltaTime;
                if (contador_recuperarse_deherido <= 0)
                {
                    estado_actual = 0;
                }
            }
            if (estado_actual != 2 && estado_actual != 3 && estado_actual != 4)
            {
                if (Input.GetAxis("Horizontal") > 0)
                {
                    rotacion.y = 0;
                    estado_actual = 1;
                }
                else if (Input.GetAxis("Horizontal") < 0)
                {
                    rotacion.y = -180;
                    estado_actual = 1;
                }
                else
                {
                    estado_actual = 0;
                }
            }

            if (Input.GetKey(KeyCode.K))
            {
                estado_actual = 2;
                contadorCancelarAtaque = 2;
            }
            if (contadorCancelarAtaque > 0)
            {
                contadorCancelarAtaque -= Time.deltaTime;
                if (contadorCancelarAtaque < 0)
                {
                    estado_actual = 0;
                }
            }
            if (Input.GetKey(KeyCode.J))
            {
                if (estado_actual != 3)
                {
                    estado_actual = 3;
                    contador_activar_hitboxespada = 0.7f;
                }
            }

            if (estado_hitboxespada == 0)
            {
                if (contador_activar_hitboxespada > 0)
                {
                    contador_activar_hitboxespada -= Time.deltaTime;
                    if (contador_activar_hitboxespada <= 0)
                    {
                        hitBoxEspada.SetActive(true);
                        estado_hitboxespada = 1;
                        contador_activar_hitboxespada = 0.2f;
                        audioSource.clip = sonidoSlashEspada;
                        audioSource.Play();
                    }
                }
            }
            else
            {
                if (contador_activar_hitboxespada > 0)
                {
                    contador_activar_hitboxespada -= Time.deltaTime;
                    if (contador_activar_hitboxespada <= 0)
                    {
                        hitBoxEspada.SetActive(false);
                        contador_activar_hitboxespada = 0f;
                    }
                }
            }
        }
            animator.SetInteger("Estado", estado_actual);
        if (estado_anterior != estado_actual)
        {
            animator.SetTrigger("cambioEstado");
        }

        hitBoxEspada.transform.position = LugarHitBoxEspada.transform.position;

        estado_anterior = animator.GetInteger("Estado");
        this.transform.position = posicion;
        this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation,Quaternion.Euler(rotacion), 600 * Time.deltaTime);
    }
    public void Evento_TerminaDisparo()
    {
        estado_actual = 0;
    }
    public void Evento_CrearBala()
    { 
        Instantiate(prefabBala, puntoCreacionBala.position, Quaternion.identity);
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("atac" + collision.gameObject.tag);
        if(collision.gameObject.tag == "DanoAJugador")
        {
            collision.gameObject.SetActive(false);
            audioSource.clip = sonidoHerido;
            audioSource.Play();
            //Destroy(this.gameObject);
            estado_actual = 4;
            contador_recuperarse_deherido = 1;
            vida--;
            if (vida <= 0)
            {
                estado_actual = 5;
            }
        } 
    }
}
