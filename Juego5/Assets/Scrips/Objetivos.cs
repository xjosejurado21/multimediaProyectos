using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Objetivos : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody rbObjetivo;
    private ControlJuego controljuego;
    public int valorPuntos;
    private float rangoX = 4.0F;
    private float posY = -1.0f;
    private float minVelocidad = 12.0F;
    private float maxVelocidad = 16.0f;
    private float fuerzaTorsion = 10.0f;


    void Start()
    {
        rbObjetivo = GetComponent<Rigidbody>();

        transform.position = posGeneracion();

        rbObjetivo.AddForce(FuerzaImpulso(), ForceMode.Impulse);
        //añadimos fuerza de giro
        rbObjetivo.AddTorque(ValorTorsion(), ValorTorsion(),
           ValorTorsion(), ForceMode.Impulse);

        controljuego = GameObject.Find("Gestor de Juegos").GetComponent<ControlJuego>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    Vector3 posGeneracion()
    {
        return new Vector3(Random.Range(-rangoX, rangoX), posY);
    }

    Vector3 FuerzaImpulso()
    {
        return Vector3.up * Random.Range(minVelocidad, maxVelocidad);
    }
    float ValorTorsion()
    {
        return Random.Range(-fuerzaTorsion, fuerzaTorsion);

    }

    private void OnMouseDown()
    {
        if (controljuego.juegoEstaActivo) {
            controljuego.ActualizarMarcador(valorPuntos);
            Destroy(gameObject);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.CompareTag("Malo"))
        {
            controljuego.GameOver();
        }

        Destroy(gameObject);
    }




}
