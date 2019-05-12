using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sons : MonoBehaviour {
    private AudioSource[] audioSource;
    //Efeito camara lenta
    //public AudioClip lentoClip;


    bool lento = false;
    bool pause1;
    bool pause2;
    float tempoInicio;

    private float timeRemaining;

    public int timeToDecrease = 2;
    GameObject personagem;
    // Use this for initialization
    void Start () {

        timeRemaining = 15;
        audioSource = GameObject.FindGameObjectWithTag("sons").GetComponentsInChildren<AudioSource>();
        personagem = GameObject.FindGameObjectWithTag("Player");
        audioSource[0].Play();



    }


    void FixedUpdate()
    {

        if (Input.GetKeyDown("l"))
        {
            if(personagem.GetComponent<personagem>().eventarioPSA.camaraLenta>0){
                tempoInicio = Time.realtimeSinceStartup;
                lento = true;
            }
           /* else{
                audioSource[2].Play();
            }*/
           
           


        }

       
        if(lento){

        


            if (audioSource[0].pitch > 0.4f)
            {

                audioSource[0].pitch = 1 - (((Time.realtimeSinceStartup - tempoInicio) / timeToDecrease) * 0.7f);

            }
            else
            {
                timeRemaining -= Time.deltaTime;
                if (timeRemaining > 0)
                {
                    audioSource[0].pitch = 0.4f;
                }
                else{

                    if (audioSource[0].pitch < 1)
                    {
                        audioSource[0].pitch = 0.3f + ((Time.realtimeSinceStartup - tempoInicio-15) / timeToDecrease)*0.7f;
                        Debug.Log(Time.realtimeSinceStartup - tempoInicio - timeToDecrease);
                    }
                    else{
                        lento = false;
                        audioSource[0].pitch = 1;
                    }
                    timeRemaining = 15;
                }
             }
        }

        if (Input.GetKeyDown("g"))
        {
            if (personagem.GetComponent<personagem>().eventarioPSA.sonar == 0)
            {
                audioSource[1].Play();
            }
           


        }
        if (Input.GetKeyDown("h"))
        {
            if (personagem.GetComponent<personagem>().eventarioPSA.magnetico == 0)
            {
                audioSource[1].Play();
            }
           /* else
            {
                audioSource[2].Play();
            }*/


        }
        if (Input.GetKeyDown("j"))
        {
            if (personagem.GetComponent<personagem>().eventarioPSA.invisibildade == 0)
            {
                audioSource[1].Play();
            }

        }

        if (Input.GetKeyDown("l"))
        {
            if (personagem.GetComponent<personagem>().eventarioPSA.camaraLenta == 0)
            {
                audioSource[1].Play();
            }

        }


    }


}
