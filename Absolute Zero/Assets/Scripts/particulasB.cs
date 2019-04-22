using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
        velocidade = 5;
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

    public int qtdChaves(){
        int filhos = 0;
        filhos = CountTrue(chaves.plasma, chaves.gasoso,chaves.liquido, chaves.solido);
        return filhos;
    }
        public int qtdPSA(){
        int filhos = 0;
        filhos = CountTrue(particulasSA.sonar, particulasSA.magnetico, particulasSA.invisibildade, particulasSA.camaraLenta);
        return filhos;
    }
    public static int CountTrue(params bool[] args)
    {
    return args.Count(t => t);
    }

}
