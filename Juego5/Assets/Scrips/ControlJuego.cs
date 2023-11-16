using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControlJuego : MonoBehaviour
{
    // Start is called before the first frame update
    //vamos a usar list en vez arrays
    public List<GameObject> objetivos;
    public TextMeshProUGUI textoMarcador; //añadimos el objeto marcador en unity
    public int marcador; //variable marcador
    private float retrasoGeneracion = 1.0f; //retraso de un segundo
    public TextMeshProUGUI textoGameOver;
    public bool juegoEstaActivo;
    public Button botonreinicio;

    public GameObject pantallaInicio;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator GenerarObjetivos()
    {
        //retardamos la tasa de espera de la generacion de enemigos
        while (juegoEstaActivo)
        {
            yield return new WaitForSeconds(retrasoGeneracion);
            int index = Random.Range(0, objetivos.Count);//count es como length
            Instantiate(objetivos[index]); //cada segundo generará un objetivo, y mientras sea true
            //generará una figura.
           

        }

    }

    //creamos un método que sume puntos al marcador
     public void ActualizarMarcador(int puntosASumar)
    {
        marcador += puntosASumar;
        textoMarcador.text = "Puntos " + marcador;
    }
    public void GameOver()
    {
        Debug.Log("HA TERMINADO EL JUEGO");
        textoGameOver.gameObject.SetActive(true);
        juegoEstaActivo = false;
        botonreinicio.gameObject.SetActive(true);

    }
    public void ReiniciarJuego()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
    public void IniciarJuego()
    {
        juegoEstaActivo = true;
        StartCoroutine(GenerarObjetivos());
        marcador = 0;
        ActualizarMarcador(0);
    }
    
}

