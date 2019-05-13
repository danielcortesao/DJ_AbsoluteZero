using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEdgesPlasma : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" || other.tag == "ParticulasA" || other.tag == "ParticulasB")
        { 
            

            float currentX = other.transform.position.x;
            float currentY = other.transform.position.y;

            Vector2 norte = new Vector2(currentX, 131.2f);
            Vector2 sul = new Vector2(currentX, -131.2f);
            Vector2 este = new Vector2(177.9f, currentY);
            Vector2 oeste = new Vector2(-177.9f, currentY);

            //Destroy(other.gameObject);



            if (currentX >= 178.0f)
                other.transform.position = oeste;
            if (currentX <= -178.0f)
                other.transform.position = este;
            if (currentY >= 131.0f)
                other.transform.position = sul;
            if (currentY <= -131.0f)
                other.transform.position = norte;




        }
    }
}
