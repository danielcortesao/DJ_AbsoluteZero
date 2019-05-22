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
    bool teclaSonar = false;
    bool teclaInvisivel = false;
    bool teclaMagnetico = false;

    private float timeRemaining;

    public int timeToDecrease = 2;
    public GameObject personagem;
    // Use this for initialization
    void Start () {

        timeRemaining = 15;
        audioSource = this.GetComponentsInChildren<AudioSource>();
        audioSource[0].Play();

        foreach(AudioSource sound in audioSource){
            Debug.Log(sound);
        }



    }


    void FixedUpdate()
    {

        // ---------- SONAR --------

        if (Input.GetKeyDown("1"))
        {
            if (personagem.GetComponent<personagem>().eventarioPSA.sonar == 0)
            {
                audioSource[0].volume = 0.5f;
                audioSource[1].Play();
            }
            else{
                audioSource[3].Play();
                teclaSonar = true;
            }

        }

        if(teclaSonar){
            timeRemaining -= Time.deltaTime;
            if (timeRemaining <=0)
            {
                audioSource[3].Stop();
                timeRemaining = 15;
                teclaSonar = false;
            }
        }


        // ------------- MAGNETICO ------------------

        if (Input.GetKeyDown("2"))
        {
            if (personagem.GetComponent<personagem>().eventarioPSA.magnetico == 0)
            {
                audioSource[1].Play();
            }
            else
            {
                audioSource[5].Play();
                audioSource[0].volume = 0.3f;
                teclaMagnetico = true;
            }

        }

        if (teclaMagnetico)
        {
            timeRemaining -= Time.deltaTime;
            if (timeRemaining <= 0)
            {
                audioSource[5].Stop();
                audioSource[0].volume = 1;
                timeRemaining = 15;
                teclaMagnetico = false;
            }
        }

        // INVISIBILIDADE 
        if (Input.GetKeyDown("3"))
        {
            if (personagem.GetComponent<personagem>().eventarioPSA.invisibildade == 0)
            {
                //audioSource[0].volume = 0.5f;
                audioSource[1].Play();
                
            }
            else{
                audioSource[0].Stop();
                audioSource[4].Play();
                teclaInvisivel = true;
            }

        }
        if (teclaInvisivel)
        {
            timeRemaining -= Time.deltaTime;
            if (timeRemaining <= 0)
            {
                audioSource[4].Stop();
                audioSource[0].Play();
                timeRemaining = 15;
                teclaInvisivel = false;
            }
        }


        //  -----------    CAMARA LENTA ---------------

        if (Input.GetKeyDown("4"))
        {
            if (personagem.GetComponent<personagem>().eventarioPSA.camaraLenta > 0)
            {
                tempoInicio = Time.realtimeSinceStartup;
                lento = true;
            }
            else{
                audioSource[1].Play();
            }

        }

        if (lento)
        {

            if ((Time.realtimeSinceStartup - tempoInicio)< 2)
            {

                audioSource[0].pitch = 1 - (((Time.realtimeSinceStartup - tempoInicio) / timeToDecrease) * 0.7f);

            }
            else if((Time.realtimeSinceStartup - tempoInicio) < 15){
                audioSource[0].pitch = 0.4f;
            }
            else
            {
                timeRemaining = Time.realtimeSinceStartup;

                    if (audioSource[0].pitch < 1)
                    {

                    audioSource[0].pitch = 0.3f + ((Time.realtimeSinceStartup - tempoInicio - 15) / timeToDecrease) * 0.7f;
                    Debug.Log(audioSource[0].pitch);

                    }
                    else
                    {
                        Debug.Log("SOM -- ");
                        lento = false;
                        audioSource[0].pitch = 1;
                        timeRemaining = 15;
                }
             
            }
        }


    }


}
