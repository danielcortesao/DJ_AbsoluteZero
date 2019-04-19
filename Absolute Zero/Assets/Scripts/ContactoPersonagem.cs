using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactoPersonagem : MonoBehaviour
{
    private float timeLastImpact = -0.9f;

    void OnCollisionEnter2D(Collision2D other)
    {   //Correr apenas se passou 1 segundo
        if(Time.time >= timeLastImpact + 1f){

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
            else if(other.gameObject.name == "PSA"){

            }
             else if(other.gameObject.name == "Chaves"){

            }
        }
        //Destroy(gameObject);
    }

}
