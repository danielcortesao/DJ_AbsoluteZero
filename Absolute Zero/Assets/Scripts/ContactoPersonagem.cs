using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactoPersonagem : MonoBehaviour
{
    private float timeLastImpact = -0.4f;
    private float deltaTime = 0.5f;
    private GameObject[] allObjectsC;

 

    public AudioSource[]audioSource;
    private Rigidbody2D rb;
    public GameObject criaParticulas;


    private void Start()
    {
        audioSource = GameObject.FindGameObjectWithTag("sons").GetComponentsInChildren<AudioSource>(); 
    }

    void OnCollisionEnter2D(Collision2D other)
    {   //Correr apenas se passou 1 segundo
        if(Time.time >= timeLastImpact + deltaTime){

            if(other.gameObject.CompareTag("ParticulasA")){
                Debug.Log(other.gameObject.name);
                //Se Player >= outro objecto (localScale tem que ser alterado para o parametro do tamanho)
                if (gameObject.transform.localScale.x >= other.transform.localScale.x)
                {
                    if(gameObject.GetComponent<personagem>().nivelTamanho < 9){
                        gameObject.GetComponent<personagem>().nivelTamanho +=1;
                    }
                    // if(gameObject.transform.localScale.x < 1.3f) { 
                    //     gameObject.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
                    // }
                }
                else
                {//Se Player < outro objecto
                    if(gameObject.GetComponent<personagem>().nivelTamanho > 2){
                        gameObject.GetComponent<personagem>().nivelTamanho -=1;
                    }

                    //                 if (gameObject.transform.localScale.x >= 0.4)
                    // {
                    //     gameObject.transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
                    // }
                    // if (gameObject.transform.localScale.x >= 0.4)
                    // {
                    //     gameObject.transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
                    // }
                }
                timeLastImpact = Time.time;
            } 
            else if(other.gameObject.CompareTag("ParticulasB")){
                 Debug.Log("Particula B" +other.gameObject.name);
                //Se Player >= outro objecto (localScale tem que ser alterado para o parametro do tamanho)
                if (gameObject.transform.localScale.x >= other.transform.localScale.x)
                {
                    //Não acontece nada ao player
                }
                else
                {   //persoangem perde particula C de forma aleatoria
                    if(arrayFilhos().Count > 0){
                        Debug.Log("arrayFilhos().Count:" + arrayFilhos().Count);
                        int index = Random.Range(0, arrayFilhos().Count);
                        print(index);
                        string str = (string)arrayFilhos()[index];
                        print(str);
                        removeEventario((string)arrayFilhos()[index]);
                        destroiFilho(gameObject, str);
                        }
                }
                timeLastImpact = Time.time;
            }
            else if(other.gameObject.CompareTag("ParticulasC")){
                Debug.Log("Particula C" + other.gameObject.name);
                //se está activa, as personagem ganha essa chave / PSA
                if(other.gameObject.GetComponent<particulasC>().ativa == true)
                    {
                    audioSource[2].Play();
                        Debug.Log("ganha chave/PSA");
                        if(other.gameObject.GetComponent<particulasC>().chaves.plasma == true){
                            gameObject.GetComponent<personagem>().chaves.plasma = true;}
                        else if(other.gameObject.GetComponent<particulasC>().chaves.gasoso == true){
                            gameObject.GetComponent<personagem>().chaves.gasoso = true;}
                        else if(other.gameObject.GetComponent<particulasC>().chaves.liquido == true){
                            gameObject.GetComponent<personagem>().chaves.liquido = true;}
                        else if(other.gameObject.GetComponent<particulasC>().chaves.solido == true){
                            gameObject.GetComponent<personagem>().chaves.solido = true;}

                        else if(other.gameObject.GetComponent<particulasC>().particulasSA.sonar == true){
                            gameObject.GetComponent<personagem>().eventarioPSA.sonar += 1;}
                        else if(other.gameObject.GetComponent<particulasC>().particulasSA.magnetico == true){
                            gameObject.GetComponent<personagem>().eventarioPSA.magnetico += 1;}
                        else if(other.gameObject.GetComponent<particulasC>().particulasSA.invisibildade == true){
                            gameObject.GetComponent<personagem>().eventarioPSA.invisibildade += 1;}
                        else if(other.gameObject.GetComponent<particulasC>().particulasSA.camaraLenta == true){
                            gameObject.GetComponent<personagem>().eventarioPSA.camaraLenta += 1;}

                        // descativa todas as particulas C
                        allObjectsC = GameObject.FindGameObjectsWithTag("ParticulasC");
                            foreach(GameObject go in allObjectsC){
                                go.gameObject.GetComponent<particulasC>().ativa = false;
                            }
                       Destroy(other.gameObject);
                    }
                    timeLastImpact = Time.time;

                
            }
        }
        //Destroy(gameObject);
    }
    public ArrayList arrayFilhos(){
        ArrayList filhos = new ArrayList();
        if(gameObject.GetComponent<personagem>().chaves.plasma == true){filhos.Add(("plasma"));}
        if(gameObject.GetComponent<personagem>().chaves.gasoso == true){filhos.Add(("gasoso"));}
        if(gameObject.GetComponent<personagem>().chaves.liquido == true){filhos.Add(("liquido"));}
        if(gameObject.GetComponent<personagem>().chaves.solido == true){filhos.Add(("solido"));}

        if(gameObject.GetComponent<personagem>().eventarioPSA.sonar >= 1){filhos.Add(("sonar"));}
        if(gameObject.GetComponent<personagem>().eventarioPSA.magnetico >= 1){filhos.Add(("magnetico"));}
        if(gameObject.GetComponent<personagem>().eventarioPSA.invisibildade >= 1){filhos.Add(("invisibildade"));}
        if(gameObject.GetComponent<personagem>().eventarioPSA.camaraLenta >= 1){filhos.Add(("camaraLenta"));}
        return filhos;
    }
    public void destroiFilho(GameObject go, string c){
        rb = go.GetComponent<Rigidbody2D>();
        // istantiate an object of the assigned public variable gameObect with coordinates ranging betwen min and max.
        //Vector3 position = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), Z);
        if(c == "plasma" || c == "gasoso" || c == "liquido" || c == "solido"){
            Debug.Log("1destroiFilho: " + c);
            GameObject tmpObj =  Instantiate(criaParticulas, rb.position, Quaternion.identity);
            tmpObj.gameObject.GetComponent<particulasC>().activaChaves(c,false);
            //criaParticulas.GetComponent<particulasC>().activaChaves(c,false);
            //Destroi filhos passados 5 segundos
            Destroy(tmpObj,5.0f);
        }
        else if(c == "sonar" || c == "magnetico" || c == "invisibildade" || c == "camaraLenta"){
            Debug.Log("2destroiFilho: " + c);
            GameObject tmpObj =  Instantiate(criaParticulas, rb.position, Quaternion.identity);
            tmpObj.gameObject.GetComponent<particulasC>().activaChaves(c,false);
            //criaParticulas.GetComponent<particulasC>().activaPSA(c,false);
            //Destroi filhos passados 5 segundos
            Destroy(tmpObj,5.0f);
        }
        
    }
    private void removeEventario(string s){
        Debug.Log("removeEventario: " + s);
        if(s == "plasma"){
            gameObject.GetComponent<personagem>().chaves.plasma = false;}
        else if(s == "gasoso"){
            gameObject.GetComponent<personagem>().chaves.gasoso = false;}
        else if(s == "liquido"){
            gameObject.GetComponent<personagem>().chaves.liquido = false;}
        else if(s == "solido"){
            gameObject.GetComponent<personagem>().chaves.solido = false;}

        else if(s == "sonar"){
            gameObject.GetComponent<personagem>().eventarioPSA.sonar -= 1;}
        else if(s == "magnetico"){
            gameObject.GetComponent<personagem>().eventarioPSA.magnetico -= 1;}
        else if(s == "invisibildade"){
            gameObject.GetComponent<personagem>().eventarioPSA.invisibildade -= 1;}
        else if(s == "camaraLenta"){
            gameObject.GetComponent<personagem>().eventarioPSA.camaraLenta -= 1;}

    }



}
