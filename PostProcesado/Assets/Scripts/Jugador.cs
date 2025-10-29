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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        posicion = Vector3.zero;
        rotacion = Vector3.zero;
        animator = this.transform.GetChild(0).GetComponent<Animator>();
        estado_anterior = 0;
        estado_anterior = 0;
        contadorCancelarAtaque = 0;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        posicion.x += Input.GetAxis("Horizontal") * Time.deltaTime * 3;

        if (estado_actual != 2 && estado_actual != 3) {
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
        if(contadorCancelarAtaque > 0) {
            contadorCancelarAtaque -= Time.deltaTime;
            if (contadorCancelarAtaque < 0)
            {
                estado_actual = 0;
            }
        }
        if (Input.GetKey(KeyCode.J))
        {
            estado_actual = 3;
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
        Debug.Log("atac" + collision.gameObject.tag);
        if(collision.gameObject.tag == "DanoAJugador")
        {
            collision.gameObject.SetActive(false);
            Destroy(this.gameObject);
        }
    }
}
