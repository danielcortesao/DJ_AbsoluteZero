using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactoB : MonoBehaviour
{
    public GameObject C;
    public GameObject B; // para o controlo de particulas
    private float timeLastImpact = 0.9f;
    private Rigidbody2D rb;
    private float deltaTime = 1.0f;
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player")){
           //Correr apenas se passou 1 segundo
            if(Time.time >= timeLastImpact + deltaTime){
                    //Particle B <= Player 
                if (gameObject.transform.localScale.x <= other.transform.localScale.x)
                {
                    Debug.Log(gameObject.GetComponent<Rigidbody2D>());
                    //->Particula B desintegra-se:
                    //Criar Chaves
                    int totalFilhos = gameObject.GetComponent<particulasB>().qtdChaves() + gameObject.GetComponent<particulasB>().qtdPSA();
                    Debug.Log("totalFilhos:" + totalFilhos);
                    criaFilhos(gameObject.GetComponent<particulasB>().chaves, gameObject.GetComponent<particulasB>().particulasSA);
                    Destroy(gameObject);
                }
                else
                {
                    //Não acontece nada à particula B
                }
                timeLastImpact = Time.time;
            }
            //Destroy(other.gameObject);
        }

    }

    void criaFilhos(Chaves chaves, ParticulasSA particulasSA){
        if(chaves.plasma == true){criaChave("plasma");}
        if(chaves.gasoso == true){criaChave("gasoso");}
        if(chaves.liquido == true){criaChave("liquido");}
        if(chaves.solido == true){criaChave("solido");}

        if(particulasSA.sonar == true){criaPSA("sonar");}
        if(particulasSA.magnetico == true){criaPSA("magnetico");}
        if(particulasSA.invisibildade == true){criaPSA("invisibildade");}
        if(particulasSA.camaraLenta == true){criaPSA("camaraLenta");}
    }


    void  criaChave(string c){
        rb = GetComponent<Rigidbody2D>();
        // istantiate an object of the assigned public variable gameObect with coordinates ranging betwen min and max.
        //Vector3 position = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), Z);
        GameObject tmpObj =  Instantiate(criaParticulas, rb.position, Quaternion.identity);
        criaParticulas.GetComponent<particulasC>().activaChaves(c);
        
        //Destroi filhos passados 10 segundos
        Destroy(tmpObj,10.0f);
    }
    void  criaPSA(string p){
        rb = GetComponent<Rigidbody2D>();
        // istantiate an object of the assigned public variable gameObect with coordinates ranging betwen min and max.
        //Vector3 position = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), Z);
        GameObject tmpObj =  Instantiate(criaParticulas, rb.position, Quaternion.identity);
        criaParticulas.GetComponent<particulasC>().ActivaPSA(p);
        
        //Destroi filhos passados 10 segundos
        Destroy(tmpObj,10.0f);
    }

    // public void killSelf(float timeKill)
    // {
    //     StartCoroutine(killNow(timeKill));
    // }



}


