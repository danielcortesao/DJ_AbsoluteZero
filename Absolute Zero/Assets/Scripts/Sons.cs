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

    bool sonarPressed = false;
    bool invisivelPressed = false;
    bool magneticoPressed = false;


    private float timeRemaining;

    public int timeToDecrease = 2;
    public GameObject personagem;
    // Use this for initialization

    IEnumerator Espera(int s)
    {
        yield return new WaitForSeconds(s);
    }

        void Start () {

        timeRemaining = 15;
        audioSource = this.GetComponentsInChildren<AudioSource>();
        audioSource[0].Play();
        audioSource[0].volume = 0.5f;
        int j = 0;
        int i = 0;
       // for (int i =0; i<=6; i++)
       //{
       //     j = i + 2;
       //     audioSource[i].PlayDelayed(1+j);
       // }

       
        // audioSource[0].PlayDelayed(1); //musica de fundo
        // audioSource[0].Pause();
        // audioSource[1].PlayDelayed(3);  //erro
        // audioSource[2].PlayDelayed(5); //musica apanhar 
        // audioSource[3].PlayDelayed(7); //sonar
        // audioSource[4].PlayDelayed(9); //invisivel
        // audioSource[5].PlayDelayed(11); //magnetico
        // audioSource[6].PlayDelayed(13); //perde psa ou chave


        foreach (AudioSource sound in audioSource){
            Debug.Log(sound);
        }



    }


    void FixedUpdate()
    {
        if (Input.GetKeyDown("z")) //musica de fundo
        {
            if (audioSource[0].isPlaying)
                audioSource[0].Pause();
            else
                audioSource[0].PlayDelayed(0);
            
        }
        if (Input.GetKeyDown("x")) //erro
        {
            audioSource[1].volume = 1.0f;
            if (audioSource[1].isPlaying)
                audioSource[1].Pause();
            else
                audioSource[1].PlayDelayed(0);

        }
        if (Input.GetKeyDown("c")) //musica apanhar
        {
            if (audioSource[2].isPlaying)
                audioSource[2].Pause();
            else
                audioSource[2].PlayDelayed(0);

        }
        if (Input.GetKeyDown("v")) //sonar
        {
            if (audioSource[3].isPlaying)
                audioSource[3].Pause();
            else
                audioSource[3].PlayDelayed(0);

        }
        if (Input.GetKeyDown("b")) //invisivel
        {
            audioSource[4].volume = 1.0f;
            if (audioSource[4].isPlaying)
                audioSource[4].Pause();
            else
                audioSource[4].PlayDelayed(0);

        }
        if (Input.GetKeyDown("n")) //musica magnetico
        {
            audioSource[5].volume = 1.0f;
            //audioSource[5]
            if (audioSource[5].isPlaying)
                audioSource[5].Pause();
            else
                audioSource[5].PlayDelayed(0);

        }
        if (Input.GetKeyDown("m")) //musica perde chave ou psa
        {
            audioSource[6].volume = 1.0f;
            if (audioSource[6].isPlaying)
                audioSource[6].Pause();
            else
                audioSource[6].PlayDelayed(0);

        }

        // ---------- SONAR --------

        if (Input.GetKeyDown("1"))
        {
            if (personagem.GetComponent<personagem>().eventarioPSA.sonar == 0 || sonarPressed == true)
            {
                audioSource[0].volume = 0.5f;
                audioSource[1].Play();
            }
            else if (sonarPressed == false){
               
                audioSource[3].Play();
                teclaSonar = true;
                sonarPressed = true;

            }

        }

        if(teclaSonar){
            timeRemaining -= Time.deltaTime;
            if (timeRemaining <=0)
            {
                audioSource[3].Stop();
                timeRemaining = 15;
                teclaSonar = false;
                sonarPressed = false;
            }
        }


        // ------------- MAGNETICO ------------------

        if (Input.GetKeyDown("2"))
        {
            if (personagem.GetComponent<personagem>().eventarioPSA.magnetico == 0 || magneticoPressed == true)
            {
                audioSource[1].Play();
            }
            else if (magneticoPressed == false)
            {
                audioSource[5].Play();
                audioSource[0].volume = 0.3f;
                teclaMagnetico = true;
                magneticoPressed = true;
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
                magneticoPressed = false;
            }
        }

        // INVISIBILIDADE 
        if (Input.GetKeyDown("3"))
        {
            if (personagem.GetComponent<personagem>().eventarioPSA.invisibildade == 0 || invisivelPressed == true)
            {
                //audioSource[0].volume = 0.5f;
                audioSource[1].Play();
                
            }
            else if (invisivelPressed == false){
                audioSource[0].Stop();
                audioSource[4].Play();
                teclaInvisivel = true;
                invisivelPressed = true;
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
                invisivelPressed = false;
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
