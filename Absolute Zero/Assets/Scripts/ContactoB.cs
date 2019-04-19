using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactoB : MonoBehaviour
{
    public GameObject C;
    private float timeLastImpact = 0.9f;
    private Rigidbody2D rb;
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player")){
           //Correr apenas se passou 1 segundo
            if(Time.time >= timeLastImpact + 1f){
                    //Particle B <= Player 
                if (gameObject.transform.localScale.x <= other.transform.localScale.x)
                {
                    Debug.Log(gameObject.GetComponent<Rigidbody2D>());
                    //->Particula B desintegra-se:
                    for (int i = 0; i < 4; i++)
                    {
                        rb = GetComponent<Rigidbody2D>();
                        // istantiate an object of the assigned public variable gameObect with coordinates ranging betwen min and max.
                        //Vector3 position = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), Z);
                        GameObject tmpObj =  Instantiate(C, rb.position, Quaternion.identity);
                    }
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

}
