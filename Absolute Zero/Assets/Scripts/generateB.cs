using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generateB : MonoBehaviour   
{

    public GameObject B;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 currentPosition = new Vector3(0f, 0f, -1f);//A.transform.position;

        float minX = -8.0f;
        float maxX = -2.0f;
        float minY = -4.0f;
        float maxY = 4.0f;
        float Z = -1.0f;

        for (int i = 0; i < 5; i++)
        {
            
            //GameObject tmpObj = GameObject.Instantiate(A, currentPosition, Quaternion.identity) as GameObject;
            //currentPosition += new Vector3(1f, 0f, 0f);
            

            // istantiate an object of the assigned public variable gameObect with coordinates ranging betwen min and max.
            Vector3 position = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), Z);
            GameObject tmpObj =  Instantiate(B, position, Quaternion.identity);

            // scale the object
            float size = Random.Range(0.0f, 1.0f) + 0.3f;
            tmpObj.transform.localScale = new Vector3(size,size, 0);

        }

        

}

    // Update is called once per frame
    void Update()
    {
        
    }
}
