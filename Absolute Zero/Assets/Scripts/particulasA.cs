using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class particulasA : MonoBehaviour
{
    public int nivelTamanho;
    public float velocidade;
    public Rigidbody2D rb;

    private float latestDirectionChangeTime;
    private readonly float directionChangeTime = 3f;
    public Vector2 movementDirection;
    private Vector2 movementPerSecond;


    //vars de area de movimento da particula
    public double centroX,centroY;
    public double d1Dentro, d2Dentro;
    public double d1Fora, d2Fora;
    public double anguloDeMovimento;

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
        CalcuateNewMovementVector();
    }

    void Update()
    {
        MovA();
    }

    void MovA()
    {
        // //if the changeTime was reached, calculate a new movement vector
        // if (Time.time - latestDirectionChangeTime > directionChangeTime)
        // {
        //     latestDirectionChangeTime = Time.time;
        //     CalcuateNewMovementVector();
        // }



        //move enemy: 
        //implemementar condição para ficar dentro dos limites.
        


        Vector2 norte = new Vector2(0.0f, 30.0f);
        Vector2 sul = new Vector2(0.0f, -20.0f);
        Vector2 este = new Vector2(40.0f, 0.0f);
        Vector2 oeste = new Vector2(-40.0f, 0.0f);



        if (transform.position.y >= norte.y)
            movementDirection = new Vector2(Random.Range(-1.0f, 1.0f), -1.0f).normalized;
        if (transform.position.y <= sul.y)
            movementDirection = new Vector2(Random.Range(-1.0f, 1.0f), 1.0f).normalized;
        if (transform.position.x >= este.x)
            movementDirection = new Vector2(-1.0f, Random.Range(-1.0f, 1.0f)).normalized;
        if (transform.position.x <= oeste.x)
            movementDirection = new Vector2(1.0f, Random.Range(-1.0f, 1.0f)).normalized;

        movementPerSecond = movementDirection * velocidade;

        CalcuateNewMovementVector();


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


        double newPosicaoX = transform.position.x + (movementPerSecond.x*Time.deltaTime);
        double newPosicaoY = transform.position.y + movementPerSecond.y*Time.deltaTime;

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
           
            
            transform.position = new Vector2(transform.position.x + (movementPerSecond.x * Time.deltaTime),
                                             transform.position.y + (movementPerSecond.y * Time.deltaTime));
        }

    
    }




    void CalcuateNewMovementVector()
    {
        //create a random direction vector with the magnitude of 1, later multiply it with the velocity of the enemy
        movementDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;
        movementPerSecond = movementDirection * velocidade;
    }


}
