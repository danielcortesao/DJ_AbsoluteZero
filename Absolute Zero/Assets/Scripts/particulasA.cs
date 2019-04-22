using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class particulasA : MonoBehaviour
{
    public int nivelTamanho;
    public float velocidade;
    public Rigidbody2D rb;

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

}
