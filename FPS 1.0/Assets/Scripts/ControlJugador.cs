using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlJugador : MonoBehaviour
{
    // Declaraci�n de variables p�blicas para ajustar desde el inspector de Unity.
    public float sensibilidadRaton = 120.0f; // Sensibilidad del rat�n para la rotaci�n.
    public float velocidad = 10.0f; // Velocidad de movimiento del jugador.
    public float distanciaDisparo = 50; // Distancia m�xima para disparar.
    public float fuerzaDisparo = 15; // Fuerza del disparo.

    Camera camara; // Referencia a la c�mara del jugador.
    public float rotacionCabeceo; // �ngulo de rotaci�n vertical de la c�mara.

    Rigidbody rbJugador; // Componente Rigidbody para mejorar el comportamiento de colisi�n.

    void Start()
    {
        // Obtener la c�mara del objeto actual y la configuraci�n inicial.
        camara = GetComponentInChildren<Camera>();
        rotacionCabeceo = camara.transform.rotation.x;
        rbJugador = GetComponent<Rigidbody>();

        // Ocultar el cursor del rat�n y mantenerlo bloqueado en el centro de la pantalla.
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Llamar a las funciones para manejar la rotaci�n, el movimiento y el disparo del jugador.
        mirar();
        mover();
        if (Input.GetButton("Disparo"))
        {
            disparar();
        }
    }

    void mover()
    {
        // Capturar la entrada del jugador para moverse lateralmente y hacia adelante.
        float moviLateral = Input.GetAxis("Horizontal") * velocidad * Time.deltaTime;
        float moviAvance = Input.GetAxis("Vertical") * velocidad * Time.deltaTime;

        // Crear un vector que representa el desplazamiento.
        Vector3 desplazamiento = new Vector3(moviLateral, 0, moviAvance);

        // Mover al jugador usando el componente Rigidbody en lugar de transform.Translate.
        rbJugador.MovePosition(rbJugador.position + desplazamiento);
    }

    void mirar()
    {
        // Capturar la entrada del rat�n para rotar la c�mara y el jugador.
        float xR = Input.GetAxis("Mouse X") * sensibilidadRaton * Time.deltaTime;
        float yR = Input.GetAxis("Mouse X") * sensibilidadRaton * Time.deltaTime;

        // Actualizar la rotaci�n vertical (cabeceo) y limitarla entre -90 y 90 grados.
        rotacionCabeceo -= yR;
        rotacionCabeceo = Mathf.Clamp(rotacionCabeceo, -90, 90);

        // Aplicar rotaci�n a la c�mara y el jugador.
        transform.Rotate(0, xR, 0);
        camara.transform.localRotation = Quaternion.Euler(rotacionCabeceo, 0, 0);
    }

    void disparar()
    {
        // Disparar un rayo desde la posici�n de la c�mara en la direcci�n hacia adelante.
        Ray rayo = new Ray(camara.transform.position, camara.transform.forward);

        // Realizar una detecci�n de colisi�n con objetos en el camino del rayo.
        RaycastHit disparo;

        if (Physics.Raycast(rayo, out disparo, distanciaDisparo) && disparo.rigidbody != null)
        {
            // Calcular una fuerza en direcci�n opuesta al normal de la superficie impactada y aplicarla al objeto.
            Vector3 vectorFuerza = -disparo.normal * fuerzaDisparo;
            disparo.rigidbody.AddForceAtPosition(vectorFuerza, disparo.point, ForceMode.Impulse);
        }
    }
}

