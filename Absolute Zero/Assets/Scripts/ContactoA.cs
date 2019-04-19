using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactoA : MonoBehaviour
{
    private float timeLastImpact = -0.9f;
    void OnCollisionEnter2D(Collision2D other)
    {
        
        if(other.gameObject.CompareTag("Player")){
           //Correr apenas se passou 1 segundo
            if(Time.time >= timeLastImpact + 1f){
                    //Particle A <= Player 
                if (gameObject.transform.localScale.x <= other.transform.localScale.x)
                {
                    if(gameObject.transform.localScale.x <= 0.3f) { 
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
