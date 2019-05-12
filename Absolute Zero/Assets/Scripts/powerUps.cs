using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerUps : MonoBehaviour
{

    // Use this for initialization
    public Rigidbody2D rb;
    public GameObject magnetico;
    //public GameObject particulaA;

    //public GameObject principal;
    public ParticleSystem sonar;
    public GameObject camera;

    public float speed = 1.0f;


    float m, b;
    bool isMagnetico = false;
    bool invisibilidade = false;
    public bool isLento = false;
    bool isSonar = false;

    int seconds;

    Transform positionPersonagem;
    Transform positionA;

    private float timeRemaining;
    private float timeCoolDown;

    private bool coolDown;
    private bool esperar;

    bool invisivel, mag, lenta,Sonar;


    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        timeRemaining = 15;
        timeCoolDown = 30;

        isLento = false;
  
        invisivel = this.GetComponent<personagem>().PSAActivas.invisibildade;
        mag = this.GetComponent<personagem>().PSAActivas.magnetico;
        lenta = this.GetComponent<personagem>().PSAActivas.camaraLenta;
        Sonar= this.GetComponent<personagem>().PSAActivas.sonar;



    }



    void posicionarSonar(float x, float y, float z)
    {
        // Camera camera = GetComponent<Camera>();
        float yIntersecao;
        float xIntersecao;
        Vector3 p1 = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, Camera.main.nearClipPlane));
        Vector3 p2 = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, Camera.main.nearClipPlane));


        m = (y - camera.transform.position.y) / (x - camera.transform.position.x);
        Debug.Log(m);
        b = camera.transform.position.y - m * camera.transform.position.x;

        //Interseção das retas

        yIntersecao = m * p2.x + b;
        if (yIntersecao >= p1.y && yIntersecao <= p2.y)
        {
            if (x > camera.transform.position.x)
            {
                xIntersecao = p1.x;
            }
            else
            {
                xIntersecao = p2.x;

            }
        }
        else
        {
            if (x == camera.transform.position.x)
            {
                xIntersecao = camera.transform.position.x;
            }
            else
            {
                xIntersecao = (y - b) / m;
            }
            if (y > camera.transform.position.y)
            {
                yIntersecao = p1.y;
            }
            else
            {
                yIntersecao = p2.y;

            }
        }


        sonar.transform.localPosition = new Vector3(xIntersecao, yIntersecao, z);
      
    }





    // Update is called once per frame
    void Update()
    {
    
        //--------------------- SONAR -------------------------
        if (Input.GetKeyDown("g"))
        {

            if (this.GetComponent<personagem>().eventarioPSA.sonar>0 && !coolDown && !this.GetComponent<personagem>().pwAtivo)
            {
                this.GetComponent<personagem>().pwAtivo = true;
                this.GetComponent<personagem>().eventarioPSA.sonar--;
                this.GetComponent<personagem>().PSAActivas.sonar = true;
                posicionarSonar(-8.25f, -3.13f, -9.5f);
            
                sonar.gameObject.SetActive(true);
                sonar.Play();
                isSonar = true;


            }
            else{
                Debug.Log("Nao ha sonar");
             }

        }
        if (isSonar)
        {
            timeRemaining -= Time.deltaTime;
            coolDown = true;
            if (timeRemaining > 0)
            {
                Debug.Log("sonar");
                posicionarSonar(-8.25f, -3.13f, -9.5f);
            }
            else{
                this.GetComponent<personagem>().pwAtivo = false;
                sonar.gameObject.SetActive(false);
                this.GetComponent<personagem>().PSAActivas.sonar = false;
                timeRemaining = 15;
                isSonar = false;
                isSonar = false;
                esperar = true;
            }
        }

       

        //--------------------- CAMPO MAGNÉTICO -----------------------
        if (Input.GetKeyDown("h"))
        {
            if(this.GetComponent<personagem>().eventarioPSA.magnetico>0 && !coolDown && !(this.GetComponent<personagem>().pwAtivo))
            
            {

                this.GetComponent<personagem>().pwAtivo = true;
                this.GetComponent<personagem>().eventarioPSA.magnetico--;
                this.GetComponent<personagem>().PSAActivas.magnetico = true;
                magnetico.SetActive(true);
                isMagnetico = true;
                Debug.Log("Campo magnetico");
            }
            else{
                Debug.Log("nao ha campo magnetico");
            }



        }
        if (isMagnetico)
        {

            //Mover a partícula A para a nossa personagem
            // float step = speed * Time.deltaTime; // calculate distance to move
            coolDown = true;
            timeRemaining -= Time.deltaTime;
            if (timeRemaining <= 0)
            {
                this.GetComponent<personagem>().pwAtivo = false;
                Debug.Log("Campo magnetico");
                timeRemaining = 15;
                magnetico.SetActive(false);
                this.GetComponent<personagem>().PSAActivas.magnetico = false;
                isMagnetico = false;
                esperar = true;
            }
        }


        //------------------- INVISIBILIDADE -------------------------
        if (Input.GetKeyDown("j"))
        {
            Debug.Log("invisibildade");
            if (this.GetComponent<personagem>().eventarioPSA.invisibildade > 0 && !coolDown && !(this.GetComponent<personagem>().pwAtivo))
            {
                this.GetComponent<personagem>().pwAtivo = true;
                Debug.Log("Entrei na invisibilidade");
                this.GetComponent<personagem>().PSAActivas.invisibildade = true;
                this.GetComponent<personagem>().eventarioPSA.invisibildade--;
                invisibilidade = true;
            }
            else{
                Debug.Log("nao existe invisibilidade");
            }
        }
       
        if(invisibilidade){

            timeRemaining -= Time.deltaTime;
            coolDown = true;
            if (timeRemaining > 0)
            {

                rb.isKinematic = true;
                this.GetComponent<Collider2D>().enabled = false;
                this.GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
                Debug.Log("Invisibilidade");
                Debug.Log(timeRemaining);
            }
            else
            {
                this.GetComponent<personagem>().pwAtivo = false;
                Debug.Log("ja nao estou invisivel");
                this.GetComponent<personagem>().PSAActivas.invisibildade = false;
                timeRemaining = 15;
                rb.isKinematic = false;
                this.GetComponent<Collider2D>().enabled = true;
                this.GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                invisibilidade = false;
                esperar = true;
            }

        }

        if(coolDown){
            timeCoolDown -= Time.deltaTime;
            if(timeCoolDown<=0){
                timeCoolDown = 30;
                coolDown = false;
            }
        }



        //           CAMARA LENTA


        if (Input.GetKeyDown("l"))
        {
            Debug.Log("camara lenta");
            if (this.GetComponent<personagem>().eventarioPSA.camaraLenta > 0 && !coolDown && !(this.GetComponent<personagem>().pwAtivo))
            {
                this.GetComponent<personagem>().pwAtivo = true;
                Debug.Log("Entrei na camara lenta");
                this.GetComponent<personagem>().PSAActivas.camaraLenta = true;
                this.GetComponent<personagem>().eventarioPSA.camaraLenta--;
                isLento = true;
            }
            else
            {
                Debug.Log("nao existe camara lenta");
            }
        }

        if (isLento)
        {

            timeRemaining -= Time.deltaTime;
            coolDown = true;
            if (timeRemaining > 0)
            {

                Time.timeScale = 0.5f;
                Debug.Log("CAMARA LENTA");
                //rb.velocity= new Vector3(0, 100, 0);
                Debug.Log(timeRemaining);
            }
            else
            {
                this.GetComponent<personagem>().pwAtivo = false;
                this.GetComponent<personagem>().PSAActivas.camaraLenta = false;
                timeRemaining = 15;
                isLento = false;
            }

        }

        if (coolDown)
        {
            timeCoolDown -= Time.deltaTime;
            if (timeCoolDown <= 0)
            {
                timeCoolDown = 30;
                coolDown = false;
            }
        }

    }




}
