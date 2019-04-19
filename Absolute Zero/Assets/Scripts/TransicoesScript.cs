using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransicoesScript : MonoBehaviour {
	public GameObject player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Entrei na zona");            
    }

    private void OnTriggerExit2D (Collider2D other)
    {
        Debug.Log("Sai na zona");            
    }
}
