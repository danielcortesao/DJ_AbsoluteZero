using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generateA : MonoBehaviour   
{

    public GameObject A;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 currentPosition = new Vector3(0f, 0f, -1f);//A.transform.position;

        float minX = -8.0f;
        float maxX = 8.0f;
        float minY = -4.0f;
        float maxY = 4.0f;
        float Z = -1.0f;

        for (int i = 0; i < 10; i++)
        {
            
            //GameObject tmpObj = GameObject.Instantiate(A, currentPosition, Quaternion.identity) as GameObject;
            //currentPosition += new Vector3(1f, 0f, 0f);
            


            Vector3 position = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), Z);
            Instantiate(A, position, Quaternion.identity);

        }

        

}

    // Update is called once per frame
    void Update()
    {
        
    }
}
