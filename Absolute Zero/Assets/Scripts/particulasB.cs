using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particulasB : MonoBehaviour
{
    public int nivelTamanho;
    public float velocidade;
    public Chaves chaves;
    public ParticulasSA particulasSA;
    public Rigidbody2D rb;

    //Constructores particulas B
    public particulasB(){
        nivelTamanho = 3;
        velocidade = 3;
    }
    public particulasB(int tam, float vel, Chaves cha, ParticulasSA psa){
        nivelTamanho = tam;
        velocidade = vel;
        chaves = cha;
        particulasSA = psa;
    }

    //Instanciar objecto de outro lado
    // public particulasB testB = new particulasB(3, 3.0f, new Chaves(), new ParticulasSA());

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

}
