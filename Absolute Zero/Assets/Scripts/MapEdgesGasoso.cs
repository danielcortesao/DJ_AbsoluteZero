using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEdgesGasoso : MonoBehaviour
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

            Vector2 norte = new Vector2(currentX, 134.9f);
            Vector2 sul = new Vector2(currentX, -134.9f);
            Vector2 este = new Vector2(182.9f, currentY);
            Vector2 oeste = new Vector2(-182.9f, currentY);

            //Destroy(other.gameObject);



            if (currentX >= 183.0f)
                other.transform.position = oeste;
            if (currentX <= -183.0f)
                other.transform.position = este;
            if (currentY >= 135.0f)
                other.transform.position = sul;
            if (currentY <= -135.0f)
                other.transform.position = norte;




        }
    }
}
