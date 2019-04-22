using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactoPersonagem : MonoBehaviour
{
    private float timeLastImpact = -0.4f;
    private float deltaTime = 0.5f;
    public GameObject[] allObjectsC;

    void OnCollisionEnter2D(Collision2D other)
    {   //Correr apenas se passou 1 segundo
        if(Time.time >= timeLastImpact + deltaTime){

            if(other.gameObject.CompareTag("ParticulasA")){
                Debug.Log(other.gameObject.name);
                //Se Player >= outro objecto (localScale tem que ser alterado para o parametro do tamanho)
                if (gameObject.transform.localScale.x >= other.transform.localScale.x)
                {
                    if(gameObject.transform.localScale.x < 1.3f) { 
                        gameObject.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
                    }
                }
                else
                {//Se Player < outro objecto
                    if (gameObject.transform.localScale.x >= 0.4)
                    {
                        gameObject.transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
                    }
                }
                timeLastImpact = Time.time;
            } 
            else if(other.gameObject.CompareTag("ParticulasB")){
                 Debug.Log(other.gameObject.name);
                //Se Player >= outro objecto (localScale tem que ser alterado para o parametro do tamanho)
                if (gameObject.transform.localScale.x >= other.transform.localScale.x)
                {
                    //Não acontece nada ao player
                }
                else
                {
                    //-> Player perde PSA
                }
                timeLastImpact = Time.time;
            }
            else if(other.gameObject.CompareTag("ParticulasC")){
                Debug.Log("Particula C" + other.gameObject.name);
                //se está activa, as personagem ganha essa chave / PSA
                if(other.gameObject.GetComponent<particulasC>().ativa == true)
                    {
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

}
