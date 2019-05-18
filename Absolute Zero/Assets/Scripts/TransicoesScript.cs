using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransicoesScript : MonoBehaviour {
	public GameObject player;
	public bool plasma;
	public bool gasoso;
	public bool liquido;
	public bool solido;
	public bool keyCima;
	private bool personagemOnTransicao;
	public GameObject canvasMic;
	public GameObject micInput;


	private Camera m_MainCamera;
	private Vector3 posicaoFinalCamera;
	private Vector3 posicaoFinalJogador;
	private bool inTransition;
	public GameObject camadaInicio;
	public GameObject camadaFim;



    public Button botao;
    public Text texto;

    private bool micro;
    private bool parado;

    public GameObject caixaTexto;


	// Use this for initialization
	void Start () {
		personagemOnTransicao = false;
		m_MainCamera = Camera.main;
		inTransition = false;
        micro = false;
        parado = false;


    }

    // Update is called once per frame
    void Update () {

		if (keyCima && (Input.GetKeyDown(KeyCode.UpArrow) || micInput.GetComponent<MicrophoneInput>().vozCima) && personagemOnTransicao){
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
		else if (!keyCima && (Input.GetKeyDown(KeyCode.DownArrow) || micInput.GetComponent<MicrophoneInput>().vozBaixo) && personagemOnTransicao){
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
		if(inTransition){
			transicaoCamada();
		}

       
	}

	private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player")){
        	personagemOnTransicao = true;
        	//canvasMic.SetActive(true);
        	micInput.SetActive(true);
            if(!player.GetComponent<ContactoPersonagem>().ajudaMicro)
            {
                texto.text = "Faça um som grave ou agudo" + '\n' + " para transitar de camada.";

                Time.timeScale = 0.0f;
                player.GetComponent<ContactoPersonagem>().ajudaMicro = true;
                caixaTexto.SetActive(true);
                parado = true;

            }

        }
    }

    private void mudaDeCamada(){
    	Debug.Log("Muda de Camada");
    	m_MainCamera.orthographic = false;
    	int posicaoCamera = 70;
    	if(gasoso){
    		posicaoCamera = 30;
    	}
    	else if(liquido){
    		posicaoCamera = -10;

    	}
    	else if(solido){
    		posicaoCamera = -50;

    	}
    	posicaoFinalCamera = new Vector3(player.transform.position.x,player.transform.position.y,posicaoCamera);
    	posicaoFinalJogador = new Vector3(player.transform.position.x,player.transform.position.y,posicaoCamera+10);
    	m_MainCamera.gameObject.transform.position = Vector3.MoveTowards(player.transform.position, posicaoFinalCamera, 10 * Time.deltaTime);
		player.transform.position = Vector3.MoveTowards(player.transform.position, posicaoFinalJogador, 10 * Time.deltaTime);
		
		if(plasma){
	    	player.GetComponent<personagem>().chaves.plasma = false;
	    }
        else if(gasoso){
    		player.GetComponent<personagem>().chaves.gasoso = false;
	    }
	    else if(liquido){
	    	player.GetComponent<personagem>().chaves.liquido = false;
    	}
    	else if(solido){
    		player.GetComponent<personagem>().chaves.solido = false;
    	}

		inTransition = true;
		camadaFim.SetActive(true);
		
    }


    private void transicaoCamada(){
    	Debug.Log(player.transform.position.z);
		Debug.Log(posicaoFinalJogador.z);
    	m_MainCamera.gameObject.transform.position = Vector3.MoveTowards(player.transform.position, posicaoFinalCamera, 10 *Time.deltaTime);
		player.transform.position = Vector3.MoveTowards(player.transform.position, posicaoFinalJogador, 10 *Time.deltaTime);

		if(player.transform.position.z == posicaoFinalJogador.z){
			inTransition = false;
			m_MainCamera.orthographic = true;
			//canvasMic.SetActive(false);
        	micInput.SetActive(false);
        	camadaInicio.SetActive(false);
        	if(plasma){
	    		player.GetComponent<powerUps>().nomeCamadaOn = "PlasmaTut";
	    	}
        	else if(gasoso){
    			player.GetComponent<powerUps>().nomeCamadaOn = "GasosoTut";
	    	}
	    	else if(liquido){
	    		player.GetComponent<powerUps>().nomeCamadaOn = "LiquidoTut";

	    	}
	    	else if(solido){
	    		player.GetComponent<powerUps>().nomeCamadaOn = "Solido";
	    	}
	    	
        	

		}
	}



    private void OnTriggerExit2D (Collider2D other)
    {
       	if(other.gameObject.CompareTag("Player")){
        	personagemOnTransicao = false;
        	//canvasMic.SetActive(false);
        	micInput.SetActive(false);
        }         
    }
}
