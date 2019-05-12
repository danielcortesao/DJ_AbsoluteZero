using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magnetico : MonoBehaviour
{

    private GameObject mag;
    public GameObject campo;
    public float forceFactor;
    private bool aproximar = false;
    // Start is called before the first frame updat
    void Start()
    {
        mag = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

        if (campo.active)
        {
           if (this.GetComponent<Renderer>().bounds.Intersects(campo.GetComponent<Renderer>().bounds) )
          {
               
                if (this.GetComponent<particulasB>().nivelTamanho <= mag.GetComponent<personagem>().nivelTamanho)
                {
                    GetComponent<Rigidbody2D>().AddForce((mag.transform.position - transform.position) * forceFactor * Time.deltaTime);
                }
                else{
                    GetComponent<Rigidbody2D>().AddForce((transform.position - mag.transform.position) * forceFactor * Time.deltaTime);

                }



           }


        }









    }
}
