using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cARACTERCONTROLLERt : MonoBehaviour
{
    
    private int vidas = 3;

    // float nivelPiso             = -2.76f;   // Este valor representa el nivel del piso para el personaje 
    float nivelTecho            = 6.02f;    // Este valor representa la parte superiror de la escena 
    float limeteR               = 10.72f;   // Este valor representa al limite deracho de la camara para el personaje 
    float limiteL               = -10.73f;  // Este valor represneta al limite izquierdo de la camra para el personaje 
    float velocidad             = 3f;       // Velocidad de desplazamiento del personaje 
    float fuerzaSalto           = 50;       // x veces la masa del personaje 
    float fuerzaSDesplazamiento = 500;        // Fuerza en Newtons


    bool enElPiso = true;

    [SerializeField] private AudioSource salto_SFX;

    void Start()
    {
       // Persona siempre inicia en la posición (-10.65, -2.5)
       gameObject.transform.position = new Vector3(-10.65f,nivelTecho,0);
       Debug.Log("INIT");
       Debug.Log("VIDAS: " + vidas);
    }

    void Update()
    {
        if(gameObject.transform.rotation.z > 0.3 || gameObject.transform.rotation.z < -0.3){
            Debug.Log("ROTATION: " + gameObject.transform.rotation.z);
            gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }

        if(Input.GetKey("right") && enElPiso){
            Debug.Log("right");
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(fuerzaSDesplazamiento, 0));     
        } 
        else if(Input.GetKey("left") && gameObject.transform.position.x > limiteL){
            Debug.Log("left");
            gameObject.transform.Translate(-velocidad*Time.deltaTime, 0, 0);
        }
         
        if(Input.GetKeyDown("space") && enElPiso){
            Debug.Log("UP - enElPiso: " + enElPiso);
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -fuerzaSalto*Physics2D.gravity[1]*gameObject.GetComponent<Rigidbody2D>().mass));
            salto_SFX.Play();
            enElPiso = false;
        }   
   
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.transform.tag == "Ground"){
            enElPiso = true;
            Debug.Log("GROUND COLLISION");
        }
        else if(collision.transform.tag == "Cuadrado"){
            enElPiso = true;
            Debug.Log("Cuadrado COLLISION");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
        Debug.Log("Caída");
        vidas -= 1; 
        Debug.Log("VIDAS: " + vidas);
        if(vidas <= 0){
           Debug.Log("GAME OVER");
           vidas = 3; 
        }
        gameObject.transform.position = new Vector3(-10.65f,nivelTecho,0);   
    }  
}
