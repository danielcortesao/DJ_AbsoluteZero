using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particulasC : MonoBehaviour
{
    // public int nivelTamanho;
    public bool ativa;
    public Chaves chaves;
    public ParticulasSA particulasSA;
    public Rigidbody2D rb;

    //Constructores particulas C
    public particulasC(){
        ativa = true;
        // int r = Random.Range(1, 9);
        
    }
    public void activaChaves(Chaves c){
        chaves = c;
    }
    public void activaChaves(string chave){
        switch (chave)
        {
            case "plasma":
                chaves.plasma = true;
                chaves.gasoso = false;
                chaves.liquido = false;
                chaves.solido = false;
                break;
            case "gasoso":
                chaves.plasma = false;
                chaves.gasoso = true;
                chaves.liquido = false;
                chaves.solido = false;
                break;
            case "liquido":
                chaves.plasma = false;
                chaves.gasoso = false;
                chaves.liquido = true;
                chaves.solido = false;
                break;
            case "solido":
                chaves.plasma = false;
                chaves.gasoso = false;
                chaves.liquido = false;
                chaves.solido = true;
                break;
            default:
                chaves.plasma = false;
                chaves.gasoso = false;
                chaves.liquido = false;
                chaves.solido = false;
                break;
        }
    }
    public void ActivaPSA(ParticulasSA c){
        // nivelTamanho = tam;
        // velocidade = vel;
        particulasSA = c;
    }
        public void ActivaPSA(string psa){
       switch (psa)
        {
            case "sonar":
                particulasSA.sonar = true;
                particulasSA.magnetico = false;
                particulasSA.invisibildade = false;
                particulasSA.camaraLenta = false;
                break;
            case "magnetico":
                particulasSA.sonar = false;
                particulasSA.magnetico = true;
                particulasSA.invisibildade = false;
                particulasSA.camaraLenta = false;
                break;
            case "invisibildade":
                particulasSA.sonar = false;
                particulasSA.magnetico = false;
                particulasSA.invisibildade = true;
                particulasSA.camaraLenta = false;
                break;
            case "camaraLenta":
                particulasSA.sonar = false;
                particulasSA.magnetico = false;
                particulasSA.invisibildade = false;
                particulasSA.camaraLenta = true;
                break;
            default:
                particulasSA.sonar = false;
                particulasSA.magnetico = false;
                particulasSA.invisibildade = false;
                particulasSA.camaraLenta = false;
                break;
        }
    }

    //Instanciar objecto de outro lado
    //public particulasA testA = new particulasB(3,3);

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

}
