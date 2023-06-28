using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
        GameObject player;
        Rigidbody2D rigidbody2D;
        bool encontroPlayer;    
        float horiozontal;    
    public bool tocaPiso;
    public float velocidadHorizontal;
    public float distanciaRayoRojo;
    public float fuerzaSalto;
    public int vidaTotal;
        int vidaActual;    
    public TMP_Text textoVidaPlayer;
    public AudioSource audioSource;


    public LayerMask piso;
    // Start is called before the first frame update    
    
    void Start()
    {
        rigidbody2D= GetComponent<Rigidbody2D>();
        player=GameObject.Find("Player");
        if(player==null){
            Debug.Log("No encontro el objeto player");
            encontroPlayer=false;
        }else{
            Debug.Log("Objecto Player asignado");
            encontroPlayer=true;
        }
        vidaActual= vidaTotal;
        
    }

    void Update()
    {
       MovimientoPersonaje();
        rigidbody2D= player.GetComponent<Rigidbody2D>();
       
    }
       
    public void MovimientoPersonaje(){
        horiozontal= Input.GetAxisRaw("Horizontal");
        if(horiozontal!=0 ){
            player.transform.position += Vector3.right * horiozontal * velocidadHorizontal* Time.deltaTime;
        }

        Debug.DrawRay(player.transform.position, Vector3.down * distanciaRayoRojo, Color.red, 0.1f );
        if(Physics2D.Raycast(player.transform.position, Vector2.down,distanciaRayoRojo, piso )){
            tocaPiso= true;
            Debug.Log("Hit"); 
            rigidbody2D.gravityScale=1f; 
        }else{
            tocaPiso=false;
            rigidbody2D.gravityScale=2f;  
        }

        if(Input.GetKeyDown(KeyCode.UpArrow)&& tocaPiso){
            Debug.Log("Salto");
            rigidbody2D.velocity= Vector2.up*fuerzaSalto;
        }     
    }

    public void Vida(int vidaPlayer){
        vidaActual = vidaActual- vidaPlayer;
        textoVidaPlayer.text= "Vida:"+ vidaActual.ToString();
        audioSource.Play();

    }
}
