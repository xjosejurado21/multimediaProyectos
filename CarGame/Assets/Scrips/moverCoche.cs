using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moverCoche : MonoBehaviour
{
    public float velocidad = 15f;
    public float velocidadGiro = 25;
    public float controlHorizontal;
    public float controlAvance;

    void Start()
    {
        // Código de inicialización (si es necesario)
    }

    void Update()
    {
        controlHorizontal = Input.GetAxis("Horizontal");
        controlAvance = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * Time.deltaTime * velocidad * controlAvance);
        transform.Translate(Vector3.right * Time.deltaTime * velocidadGiro * controlHorizontal);
        transform.Rotate(Vector3.up * Time.deltaTime * velocidadGiro * controlHorizontal);
    }
}

