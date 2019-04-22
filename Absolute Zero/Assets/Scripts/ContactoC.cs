using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactoC : MonoBehaviour
{

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player")){
            //Destroy(gameObject);
        }

    }

}
