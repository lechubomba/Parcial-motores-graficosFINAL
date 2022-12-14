using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ControlJugador : MonoBehaviour
{
    private Rigidbody rb;
    public int rapidez;
    private int cont;
    public LayerMask capaPiso;
    public float magnitudSalto;
    public SphereCollider col;
    float currentTime;
    public float startingTime = 80f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<SphereCollider>();
       
      
    }

    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && EstaEnPiso())
        {
            rb.AddForce(Vector3.up * magnitudSalto, ForceMode.Impulse);
        }
    }


    private bool EstaEnPiso()
    {
        return Physics.CheckCapsule(col.bounds.center, new Vector3(col.bounds.center.x, col.bounds.min.y, col.bounds.center.z), col.radius * .9f, capaPiso);
    }

    private void FixedUpdate()
    {
        float movimientoHorizontal = Input.GetAxis("Horizontal");
        float movimientoVertical = Input.GetAxis("Vertical");
        Vector3 vectorMovimiento = new Vector3(movimientoHorizontal, 0.0f, movimientoVertical);
        rb.AddForce(vectorMovimiento * rapidez);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("coleccionable") == true)
        {
            GestorDeAudio.instancia.ReproducirSonido("corre");
            other.gameObject.SetActive(false);
            rapidez = rapidez + 3;
           
          
           

        }
    }
  

    public GameObject powerup;

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "powerup")
        {
            Debug.Log("Collision detected");
            powerup.GetComponent<Renderer>().enabled = false;
            
        }
    }
    private void Sonido()
    {
        GestorDeAudio.instancia.ReproducirSonido("bombos");
    }

   



}

