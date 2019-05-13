using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEdges : MonoBehaviour
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

            Vector2 norte = new Vector2(currentX, 101.0f);
            Vector2 sul = new Vector2(currentX, -101.0f);
            Vector2 este = new Vector2(133.0f, currentY);
            Vector2 oeste = new Vector2(-133.0f, currentY);

            //Destroy(other.gameObject);

            

            if (currentX >= 133.0f)
                other.transform.position = oeste;
            if (currentX <= -133.0f)
                other.transform.position = este;
            if (currentY >= 101.0f)
                other.transform.position = sul;
            if (currentY <= -101.0f)
                other.transform.position = norte;




        }
    }
}
