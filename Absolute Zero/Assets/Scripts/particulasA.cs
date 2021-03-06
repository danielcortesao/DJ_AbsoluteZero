﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class particulasA : MonoBehaviour
{
    public int nivelTamanho;
    public float velocidade;
    public Rigidbody2D rb;

    private float latestDirectionChangeTime;
    private readonly float directionChangeTime = 10f;
    public Vector2 movementDirection;

    public GameObject personagem;
    public GameObject particulaA;

    public Vector2 movementPerSecond;


    //vars de area de movimento da particula
    public double centroX,centroY;
    public double d1Dentro, d2Dentro;
    public double d1Fora, d2Fora;
    public double anguloDeMovimento;
    public GameObject controladorCamada;
    public string nomeZona;
    public bool lento;

    //Constructores particulas A
    public particulasA(){
    }
    public particulasA(int tam, float vel){
        nivelTamanho = tam;
        velocidade = vel;
    }

    //Instanciar objecto de outro lado
    //public particulasA testA = new particulasB(3,3);

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        latestDirectionChangeTime = 0f; // para movB
        CalcuateNewMovementVector();
        lento = false;
        particulaA = GameObject.FindWithTag("ParticulasA");
    }

    void Update()
    {
        MovA();
        // slow motion nas partículas

        /*
        if (personagem.GetComponent<powerUps>().isLento)
        {
            Time.timeScale = 0.1f;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
        }
        else{
            Time.timeScale = 1f;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
        }*/
        
        float novoTam = ((float)nivelTamanho*0.13f-((10-(float)nivelTamanho))*0.06f)+0.8f;
        rb.transform.localScale = new Vector3(novoTam,novoTam, 0);
    }

    void MovA()
    {
         //if the changeTime was reached, calculate a new movement vector
         if (Time.time - latestDirectionChangeTime > directionChangeTime)
         {
             latestDirectionChangeTime = Time.time;
             CalcuateNewMovementVector();
         }



        //move enemy: 
        //implemementar condição para ficar dentro dos limites.
        Scene currentScene = SceneManager.GetActiveScene();

        // Retrieve the name of this scene.
        string sceneName = currentScene.name;

        if (sceneName == "Main")
        {
        
            Vector2 norte = new Vector2(0.0f, 30.0f);
            Vector2 sul = new Vector2(0.0f, -20.0f);
            Vector2 este = new Vector2(40.0f, 0.0f);
            Vector2 oeste = new Vector2(-40.0f, 0.0f);



            if (transform.localPosition.y >= norte.y)
                movementDirection = new Vector2(Random.Range(-1.0f, 1.0f), -1.0f).normalized;
            if (transform.localPosition.y <= sul.y)
                movementDirection = new Vector2(Random.Range(-1.0f, 1.0f), 1.0f).normalized;
            if (transform.localPosition.x >= este.x)
                movementDirection = new Vector2(-1.0f, Random.Range(-1.0f, 1.0f)).normalized;
            if (transform.localPosition.x <= oeste.x)
                movementDirection = new Vector2(1.0f, Random.Range(-1.0f, 1.0f)).normalized;

            movementPerSecond = movementDirection * velocidade;

            transform.localPosition = new Vector2(transform.localPosition.x + (movementPerSecond.x * Time.smoothDeltaTime),
            transform.localPosition.y + (movementPerSecond.y * Time.smoothDeltaTime));

        }
        

        //criar vetor de movimentação
        /*

        ERRO --------------------------------------------------------------
        double anguloAdicional = UnityEngine.Random.Range(-45.0f, 45.0f);
        anguloDeMovimento += anguloDeMovimento;
        if(anguloDeMovimento> 360.0){
            anguloDeMovimento -= 360.0;
        }
        anguloAdicional = anguloDeMovimento * (Mathf.PI/180);
        float addVetorX = Mathf.Cos((float)anguloAdicional) * 10;
        float addVetorY = Mathf.Sin((float)anguloAdicional) * 10;
        

    -------------------------------
        */

        if (sceneName == "Scene_Mundo")
        {
            double newPosicaoX = transform.localPosition.x + (movementPerSecond.x*Time.smoothDeltaTime);
            double newPosicaoY = transform.localPosition.y + movementPerSecond.y*Time.smoothDeltaTime;

            bool newPosicaoValida = false;
            //verificar se a nova posicao está dentro dos limites
            double verificarFora = Mathf.Pow((float)(newPosicaoX-centroX),2)/Mathf.Pow((float)d2Fora,2) + Mathf.Pow((float)(newPosicaoY-centroY),2)/Mathf.Pow((float)d1Fora,2);
            //Debug.Log(verificarFora);

            if(verificarFora <= 1.0){
                //Debug.Log("Fora Check");
                if(d1Dentro == 0){
                    newPosicaoValida = true;
                }
                else{
                    double verificarDentro = Mathf.Pow((float)(newPosicaoX-centroX),2)/Mathf.Pow((float)d2Dentro,2) + Mathf.Pow((float)(newPosicaoY-centroY),2)/Mathf.Pow((float)d1Dentro,2);
                    if(verificarFora <= 1.0){
                        newPosicaoValida = true;
                    }
                }

            }

            if(newPosicaoValida){
                //Debug.Log("Moveu");
                transform.localPosition = new Vector3(transform.localPosition.x + (movementPerSecond.x * Time.smoothDeltaTime),
                                                transform.localPosition.y + (movementPerSecond.y * Time.smoothDeltaTime),
                                                0.0f
                                                );
            }
            if(newPosicaoValida==false)
            {
            //Debug.Log("mudou direcao");
            // transform.position = new Vector2(transform.position.x  + (movementPerSecond.x * -1 * Time.smoothDeltaTime),
                //                                 transform.position.y  + (movementPerSecond.y * -1 * Time.smoothDeltaTime));
                CalcuateNewMovementVector();
            }    
        }
    }




    void CalcuateNewMovementVector()
    {
        //create a random direction vector with the magnitude of 1, later multiply it with the velocity of the enemy
        movementDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;
        movementPerSecond = movementDirection * velocidade;
    }


    public void reposicao(){

        // Create a temporary reference to the current scene.
        Scene currentScene = SceneManager.GetActiveScene();

        // Retrieve the name of this scene.
        string sceneName = currentScene.name;

        if (sceneName == "Scene_Mundo")
        {
            controladorCamada.GetComponent<GeradorDeCamadas>().reposicaoParticula(nomeZona);
        }
       
    }
}
