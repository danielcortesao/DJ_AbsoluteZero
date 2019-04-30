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
    private Vector2 movementDirection;
    private Vector2 movementPerSecond;

    //Constructores particulas A
    public particulasA(){
        nivelTamanho = 3;
        velocidade = 4;
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
    }

    void Update()
    {
        MovA();
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

        transform.position = new Vector2(transform.position.x + (movementPerSecond.x * Time.deltaTime),
        transform.position.y + (movementPerSecond.y * Time.deltaTime));
    }




    void CalcuateNewMovementVector()
    {
        //create a random direction vector with the magnitude of 1, later multiply it with the velocity of the enemy
        movementDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;
        movementPerSecond = movementDirection * velocidade;
    }


}
