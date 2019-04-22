using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactoA : MonoBehaviour
{
    public GameObject A; // para o controlo de particulas
    private float timeLastImpact = -0.4f;
    private float deltaTime = 0.5f;
    void OnCollisionEnter2D(Collision2D other)
    {
        
        if(other.gameObject.CompareTag("Player")){
           //Correr apenas se passou 1 segundo
            if(Time.time >= timeLastImpact + deltaTime){
                    //Particle A <= Player 
                if (gameObject.transform.localScale.x <= other.transform.localScale.x)
                {
                    if(gameObject.transform.localScale.x <= 0.3f) {
                        // controlo de particulas
                        float minX = -8.0f;
                        float maxX = -2.0f;
                        float minY = -4.0f;
                        float maxY = 4.0f;
                        float Z = -1.0f;

                        Vector3 position = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), Z);
                        GameObject tmpObj = Instantiate(A, position, Quaternion.identity);
                        // fim de controlo de particulas
                        Destroy(gameObject);
                    }
                    else{
                        gameObject.transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
                    }
                }
                else
                {   //Particle A > Player
                    if (gameObject.transform.localScale.x < 1.4f)
                    {
                        gameObject.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
                    }
                }
                timeLastImpact = Time.time;
            }
            //Destroy(other.gameObject);
        }

    }

}
