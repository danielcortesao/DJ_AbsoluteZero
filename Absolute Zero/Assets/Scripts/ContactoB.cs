using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactoB : MonoBehaviour
{
    public GameObject ChavePrefab;
    public GameObject PowerUpPrefab;
    public GameObject B; // para o controlo de particulas
    private float timeLastImpact = -0.4f;
    private Rigidbody2D rb;
    private float deltaTime = 0.5f;


    public Sprite spriteSonar;
    public Sprite spriteMagnetico;
    public Sprite spriteInvisivel;
    public Sprite spriteLento;

    public Sprite spriteGasoso;
    public Sprite spriteLiquido;
    public Sprite spritePlasma;


    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player")){
           //Correr apenas se passou 1 segundo
            if(Time.time >= timeLastImpact + deltaTime){
                    //Particle B <= Player 
                if (gameObject.transform.localScale.x <= other.transform.localScale.x)
                {
                    Debug.Log(gameObject.GetComponent<Rigidbody2D>());
                    //Particula B desintegra-se e Cria Chaves
                    int totalFilhos = gameObject.GetComponent<particulasB>().qtdChaves() + gameObject.GetComponent<particulasB>().qtdPSA();
                    criaFilhos(gameObject.GetComponent<particulasB>().chaves, gameObject.GetComponent<particulasB>().particulasSA);
                    this.GetComponent<particulasB>().reposicao();
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
        //x' = x cos θ − y sin θ
        //y' = x sin θ + y cos θ

        Vector2 directionForce = new Vector2(1,0);

        GameObject[] listaIrmao = new GameObject[3];
        int numIrmaos=0;
        if(chaves.plasma == true){
            GameObject newFilho = criaFilhoB("plasma",directionForce);
            listaIrmao[numIrmaos]= newFilho;
            directionForce = new Vector2(directionForce.x * Mathf.Cos(Mathf.PI /2) - directionForce.y * Mathf.Sin(Mathf.PI /2) ,directionForce.x * Mathf.Sin(Mathf.PI /2) + directionForce.y * Mathf.Cos(Mathf.PI /2) );
            numIrmaos++;
        }
        if(chaves.gasoso == true){
            GameObject newFilho = criaFilhoB("gasoso",directionForce);
            listaIrmao[numIrmaos]= newFilho;
            directionForce = new Vector2(directionForce.x * Mathf.Cos(Mathf.PI /2) - directionForce.y * Mathf.Sin(Mathf.PI /2) ,directionForce.x * Mathf.Sin(Mathf.PI /2) + directionForce.y * Mathf.Cos(Mathf.PI /2) );
            numIrmaos++;
        }
        if(chaves.liquido == true){
            GameObject newFilho = criaFilhoB("liquido",directionForce);
            listaIrmao[numIrmaos]= newFilho;
            directionForce = new Vector2(directionForce.x * Mathf.Cos(Mathf.PI /2) - directionForce.y * Mathf.Sin(Mathf.PI /2) ,directionForce.x * Mathf.Sin(Mathf.PI /2) + directionForce.y * Mathf.Cos(Mathf.PI /2) );
            numIrmaos++;
        }
        if(chaves.solido == true){
            GameObject newFilho = criaFilhoB("solido",directionForce);
            listaIrmao[numIrmaos]= newFilho;
            directionForce = new Vector2(directionForce.x * Mathf.Cos(Mathf.PI /2) - directionForce.y * Mathf.Sin(Mathf.PI /2) ,directionForce.x * Mathf.Sin(Mathf.PI /2) + directionForce.y * Mathf.Cos(Mathf.PI /2) );
            numIrmaos++;
        }

        if(particulasSA.sonar == true){
            GameObject newFilho = criaFilhoB("sonar",directionForce);
            listaIrmao[numIrmaos]= newFilho;
            directionForce = new Vector2(directionForce.x * Mathf.Cos(Mathf.PI /2) - directionForce.y * Mathf.Sin(Mathf.PI /2) ,directionForce.x * Mathf.Sin(Mathf.PI /2) + directionForce.y * Mathf.Cos(Mathf.PI /2) );
            numIrmaos++;
        }
        if(particulasSA.magnetico == true){
            GameObject newFilho = criaFilhoB("magnetico",directionForce);
            listaIrmao[numIrmaos]= newFilho;
            directionForce = new Vector2(directionForce.x * Mathf.Cos(Mathf.PI /2) - directionForce.y * Mathf.Sin(Mathf.PI /2) ,directionForce.x * Mathf.Sin(Mathf.PI /2) + directionForce.y * Mathf.Cos(Mathf.PI /2) );
            numIrmaos++;
        }
        if(particulasSA.invisibildade == true){
            GameObject newFilho = criaFilhoB("invisibildade",directionForce);
            listaIrmao[numIrmaos]= newFilho;
            directionForce = new Vector2(directionForce.x * Mathf.Cos(Mathf.PI /2) - directionForce.y * Mathf.Sin(Mathf.PI /2) ,directionForce.x * Mathf.Sin(Mathf.PI /2) + directionForce.y * Mathf.Cos(Mathf.PI /2) );
            numIrmaos++;
        }
        if(particulasSA.camaraLenta == true){
            GameObject newFilho = criaFilhoB("camaraLenta",directionForce);
            listaIrmao[numIrmaos]= newFilho;
            directionForce = new Vector2(directionForce.x * Mathf.Cos(Mathf.PI /2) - directionForce.y * Mathf.Sin(Mathf.PI /2) ,directionForce.x * Mathf.Sin(Mathf.PI /2) + directionForce.y * Mathf.Cos(Mathf.PI /2) );
            numIrmaos++;
        }

        for(int i =0;i< numIrmaos;i++){
            listaIrmao[i].GetComponent<particulasC>().numIrmaos = numIrmaos;
            listaIrmao[i].GetComponent<particulasC>().irmaos = listaIrmao;
        }


    }

    public GameObject criaFilhoB(string c, Vector2 directionForce){
        rb = GetComponent<Rigidbody2D>();
        // istantiate an object of the assigned public variable gameObect with coordinates ranging betwen min and max.
        //Vector3 position = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), Z);
        if(c == "plasma" || c == "gasoso" || c == "liquido" || c == "solido"){
            GameObject tmpObj =  Instantiate(ChavePrefab, rb.position, Quaternion.identity);
            tmpObj.transform.parent = gameObject.transform.parent.transform;
            tmpObj.transform.localPosition = new Vector3(tmpObj.transform.position.x, tmpObj.transform.position.y, 0);

            tmpObj.GetComponent<particulasC>().activaChaves(c,true);
            tmpObj.GetComponent<particulasC>().activaPSA("null",true);



            if(c=="plasma"){ //vermelho 
            	tmpObj.GetComponent<SpriteRenderer>().sprite = spritePlasma;
            }
            else if(c=="gasoso"){ // verde
            	tmpObj.GetComponent<SpriteRenderer>().sprite = spriteGasoso;
            }
            else if(c=="liquido"){ // AZUL
            	tmpObj.GetComponent<SpriteRenderer>().sprite = spriteLiquido;
            }
            else{ // cinza
            	//tmpObj.GetComponent<SpriteRenderer>().color = new Color(0,0,0,1);
            }

            tmpObj.GetComponent<Rigidbody2D>().AddForce(directionForce * (float)200);
            //Destroi filhos passados 5 segundos
            Destroy(tmpObj,5.0f);
            return tmpObj;


        }
        else if(c == "sonar" || c == "magnetico" || c == "invisibildade" || c == "camaraLenta"){
            GameObject tmpObj =  Instantiate(PowerUpPrefab, rb.position, Quaternion.identity);
            tmpObj.transform.parent = gameObject.transform.parent.transform;
            tmpObj.transform.localPosition = new Vector3(tmpObj.transform.position.x, tmpObj.transform.position.y, 0);
            
            tmpObj.GetComponent<particulasC>().activaPSA(c,true);
            tmpObj.GetComponent<particulasC>().activaChaves("null",true);
            //Destroi filhos passados 5 segundos

            if(c=="sonar"){ //branco 
            	//tmpObj.GetComponent<SpriteRenderer>().color = new Color(1,1,1,1);
            	//Sprite spriteAdd = Resources.Load<Sprite>("Assets/imgs/ParticulasImgs/powerUp1.png");
            	tmpObj.GetComponent<SpriteRenderer>().sprite = spriteSonar;
            }
            else if(c=="magnetico"){ // roxo
            	tmpObj.GetComponent<SpriteRenderer>().sprite = spriteMagnetico;
            }
            else if(c=="invisibildade"){ // preto
            	tmpObj.GetComponent<SpriteRenderer>().sprite = spriteInvisivel;
            }
            else{ // laranja
            	tmpObj.GetComponent<SpriteRenderer>().sprite = spriteLento;
            }	
            tmpObj.GetComponent<Rigidbody2D>().AddForce(directionForce * (float)200);



            Destroy(tmpObj,5.0f);
            return tmpObj;
        }
        return null;
    }
    
    // void  criaChave(string c){//DEPRECATED
    //     rb = GetComponent<Rigidbody2D>();
    //     // istantiate an object of the assigned public variable gameObect with coordinates ranging betwen min and max.
    //     //Vector3 position = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), Z);
    //     GameObject tmpObj =  Instantiate(criaParticulas, rb.position, Quaternion.identity);
    //     criaParticulas.GetComponent<particulasC>().activaChaves(c);
        
    //     //Destroi filhos passados 5 segundos
    //     Destroy(tmpObj,5.0f);
    // }
    // void  criaPSA(string p){
    //     rb = GetComponent<Rigidbody2D>();
    //     // istantiate an object of the assigned public variable gameObect with coordinates ranging betwen min and max.
    //     //Vector3 position = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), Z);
    //     GameObject tmpObj =  Instantiate(criaParticulas, rb.position, Quaternion.identity);
    //     criaParticulas.GetComponent<particulasC>().activaPSA(p);
        
    //     //Destroi filhos passados 5 segundos
    //     Destroy(tmpObj,5.0f);
    // }

    // public void killSelf(float timeKill)
    // {
    //     StartCoroutine(killNow(timeKill));
    // }



}


