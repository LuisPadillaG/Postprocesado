using UnityEngine;

public class Jugador : MonoBehaviour
{
    Vector3 posicion, rotacion;
    Animator animator;
    int estado_anterior;
    int estado_actual;
    public GameObject prefabBala;
    public Transform puntoCreacionBala;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        posicion = Vector3.zero;
        rotacion = Vector3.zero;
        animator = this.transform.GetChild(0).GetComponent<Animator>();
        estado_anterior = 0;
        estado_anterior = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        posicion.x += Input.GetAxis("Horizontal") * Time.deltaTime * 3;

        if (estado_actual != 2) {
            if (Input.GetAxis("Horizontal") > 0)
            {
                rotacion.y = -90;
                estado_actual = 1;
            }
            else if (Input.GetAxis("Horizontal") < 0)
            {
                rotacion.y = 90;
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
        }
        animator.SetInteger("Estado", estado_actual);
        if (estado_anterior != estado_actual)
        {
            animator.SetTrigger("cambioEstado");
        }

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
}
