using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ControlJugador : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody rbJugador;
    public float fuerzaSalto = 10;
    [SerializeField]
    private float modificadorGravedad = 2;
    [SerializeField]
    private bool estarSuelo = true;
    public bool gameOver;
    private Animator animacionJugador;
    public ParticleSystem explosion;
    public ParticleSystem polvareda;
    void Start()
    {
        //vamos a dotar de RigidBody al personaje principal pero en c�digo
        rbJugador = GetComponent<Rigidbody>();
        Physics.gravity *= modificadorGravedad;
        animacionJugador = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && estarSuelo && !gameOver)
        {

            //esto va  hacer que cuando pulsemos la barra espacio
            //se mueva hacia arriba pero la fuerza de la gravedad cuando dejemos de pulsar
            // le llevara hacia abajo.
            // al multiplicarla por algo, por 10 por ejemplo veremos que se impulsa, sino se moveria muy poco
            rbJugador.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
            estarSuelo = false;
            animacionJugador.SetTrigger("Jump_trig"); //podemos verlo en animator
            polvareda.Stop();
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            estarSuelo = true;
            polvareda.Play();
        }
        else if (collision.gameObject.CompareTag("Obstaculo"))
        {

            Debug.Log("GAME OVER");
            gameOver = true;
            animacionJugador.SetBool("Death_b", true);
            animacionJugador.SetInteger("DeathType_int", 1);
            explosion.Play();
            polvareda.Stop();


            // Cuando el jugador entre en colisi�n con otro objeto,
            // el valor de estar en el suelo pasar� a true
        }
    }

}

