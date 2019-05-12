using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransicoesScript : MonoBehaviour {
	public GameObject player;
	public bool plasma;
	public bool gasoso;
	public bool liquido;
	public bool solido;
	public bool keyCima;
	private bool personagemOnTransicao;

	// Use this for initialization
	void Start () {
		personagemOnTransicao = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.UpArrow) && keyCima && personagemOnTransicao){
			if(player.GetComponent<personagem>().chaves.plasma && plasma){
				mudaDeCamada();
			}
			else if(player.GetComponent<personagem>().chaves.solido && solido){
				mudaDeCamada();
			}
			else if(player.GetComponent<personagem>().chaves.gasoso && gasoso){
				mudaDeCamada();
			}
			else if(player.GetComponent<personagem>().chaves.liquido && liquido){
				mudaDeCamada();
			}
		}
		else if (Input.GetKeyDown(KeyCode.DownArrow) && !keyCima && personagemOnTransicao){
			if(player.GetComponent<personagem>().chaves.plasma && plasma){
				mudaDeCamada();
			}
			else if(player.GetComponent<personagem>().chaves.solido && solido){
				mudaDeCamada();
			}
			else if(player.GetComponent<personagem>().chaves.gasoso && gasoso){
				mudaDeCamada();
			}
			else if(player.GetComponent<personagem>().chaves.liquido && liquido){
				mudaDeCamada();
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player")){
        	personagemOnTransicao = true;
        }
    }

    private void mudaDeCamada(){
    	Debug.Log("Muda de Camada");
    }



    private void OnTriggerExit2D (Collider2D other)
    {
       	if(other.gameObject.CompareTag("Player")){
        	personagemOnTransicao = false;
        }         
    }
}
