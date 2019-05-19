using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerUps : MonoBehaviour
{

    // Use this for initialization
    public Rigidbody2D rb;
    public GameObject magnetico;

    public GameObject chavesGrupo, powerUpsGrupo;
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

    bool invisivel, mag, lenta,Sonar;
    public string nomeCamadaOn;
    private float posicaoSonarX, posicaoSonarY;

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

        magnetico.SetActive(false);
    }



    void posicionarSonar()
    {
        float x = posicaoSonarX;
        float y = posicaoSonarY;
        // Camera camera = GetComponent<Camera>();
        float yIntersecao;
        float xIntersecao;
        Vector3 p1 = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, Camera.main.nearClipPlane));
        Vector3 p2 = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, Camera.main.nearClipPlane));
        
        if(y>p1.y && y<p2.y && x<p1.x && x>p2.x){
            yIntersecao = y;
            xIntersecao = x;
        }
        else{
            if(y == camera.transform.position.y){
                if(x > camera.transform.position.x){
                    xIntersecao = p1.x;
                    yIntersecao = y;
                }
                else{
                    xIntersecao = p2.x;
                    yIntersecao = y;
                }
            }
            else if(x == camera.transform.position.x){
                if(y > camera.transform.position.y){
                    xIntersecao = x;
                    yIntersecao = p2.y;
                }
                else{
                    xIntersecao = x;
                    yIntersecao = p1.y;
                }
            }
            else{

                m = (y - camera.transform.position.y) / (x - camera.transform.position.x);
                b = camera.transform.position.y - m * camera.transform.position.x;

                //Interseção das retas

                yIntersecao = m * p2.x + b;
                if (yIntersecao >= p1.y && yIntersecao <= p2.y)
                {
                    if (x > camera.transform.position.x)
                    {
                        xIntersecao = p1.x;
                        yIntersecao = m * p1.x + b;
                    }
                    else
                    {
                        xIntersecao = p2.x;
                        yIntersecao = m * p1.x + b;
                    }
                }
                else
                {
                    if(y >= p2.y){
                        xIntersecao = (p2.y - b) / m;
                        yIntersecao = p2.y;
                    }
                    else{
                        xIntersecao = (p1.y - b) / m;
                        yIntersecao = p1.y;
                    }

                        
                    
                    
                }

            }
        }
        sonar.transform.localPosition = new Vector3(xIntersecao, yIntersecao, (float)(camera.transform.position.z+9.5f));
        
    }





    // Update is called once per frame
    void Update()
    {
    
        //--------------------- SONAR -------------------------
        if (Input.GetKeyDown("a"))
        {
            if (this.GetComponent<personagem>().eventarioPSA.sonar>0 && !coolDown && !this.GetComponent<personagem>().pwAtivo){
                
                //verificar posicao
                /*
                    Camada Plasma Tut
                        Chave Gasoso     ---  
                        Transicao Gasoso --- -92.5   -63.7

                    Camada Gasoso Tut
                        Chave Plasma     ---  92     39.5
                        Transicao Plasma ---  94.4   -77.3

                    Camada Liquido Tut
                        Chave Gasoso     ---  -95.5  -29
                        Transicao Gasoso --- -77.7   37.7
            

                */
                if(nomeCamadaOn == ("LiquidoTut")){
                    if(this.GetComponent<personagem>().chaves.gasoso){
                        posicaoSonarX =-77.7f;
                        posicaoSonarY = 37.7f;
                    }
                    else{
                        posicaoSonarX =-95.5f;
                        posicaoSonarY =-29.0f;
                    }
                }
                else if(nomeCamadaOn == ("GasosoTut")){
                    if(this.GetComponent<personagem>().chaves.plasma){
                        posicaoSonarX =94.4f;
                        posicaoSonarY =-77.3f;
                    }
                    else{
                        posicaoSonarX =92.0f;
                        posicaoSonarY =39.5f;
                    }
                }
                else if(nomeCamadaOn == ("PlasmaTut")){
                    if(this.GetComponent<personagem>().chaves.gasoso){
                        posicaoSonarX =-92.5f;
                        posicaoSonarY =-63.7f;
                    }
                    else{
                        posicaoSonarX =94.4f;
                        posicaoSonarY =-77.3f;
                    }
                }

                this.GetComponent<personagem>().pwAtivo = true;
                this.GetComponent<personagem>().eventarioPSA.sonar--;
                this.GetComponent<personagem>().numeroPowerUps--;
                this.GetComponent<ContactoPersonagem>().removePowerUp("sonar");
                this.GetComponent<personagem>().PSAActivas.sonar = true;
                this.GetComponent<ContactoPersonagem>().powerUpsDesativados();
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
                posicionarSonar();
            }
            else{
                this.GetComponent<personagem>().pwAtivo = false;
                sonar.gameObject.SetActive(false);
                this.GetComponent<personagem>().PSAActivas.sonar = false;
                timeRemaining = 15;
                isSonar = false;
            }
        }

       

        //--------------------- CAMPO MAGNÉTICO -----------------------
        if (Input.GetKeyDown("s"))
        {
            if(this.GetComponent<personagem>().eventarioPSA.magnetico>0 && !coolDown && !(this.GetComponent<personagem>().pwAtivo))
            
            {

                this.GetComponent<personagem>().pwAtivo = true;
                this.GetComponent<personagem>().eventarioPSA.magnetico--;
                this.GetComponent<personagem>().PSAActivas.magnetico = true;
                this.GetComponent<personagem>().numeroPowerUps--;
                this.GetComponent<ContactoPersonagem>().removePowerUp("magnetico");
                this.GetComponent<ContactoPersonagem>().powerUpsDesativados();
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
            }
        }


        //------------------- INVISIBILIDADE -------------------------
        if (Input.GetKeyDown("d"))
        {
            Debug.Log("invisibildade");
            if (this.GetComponent<personagem>().eventarioPSA.invisibildade > 0 && !coolDown && !(this.GetComponent<personagem>().pwAtivo))
            {
                this.GetComponent<personagem>().pwAtivo = true;
                Debug.Log("Entrei na invisibilidade");
                this.GetComponent<personagem>().PSAActivas.invisibildade = true;
                this.GetComponent<personagem>().eventarioPSA.invisibildade--;
                this.GetComponent<personagem>().numeroPowerUps--;
                this.GetComponent<ContactoPersonagem>().removePowerUp("invisibilidade");
                this.GetComponent<ContactoPersonagem>().powerUpsDesativados();
                invisibilidade = true;
                chavesGrupo.SetActive(false);
                powerUpsGrupo.SetActive(false);
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
            }
            else
            {
                this.GetComponent<personagem>().pwAtivo = false;
                this.GetComponent<personagem>().PSAActivas.invisibildade = false;

                chavesGrupo.SetActive(true);
                powerUpsGrupo.SetActive(true);

                timeRemaining = 15;
                rb.isKinematic = false;
                this.GetComponent<Collider2D>().enabled = true;
                this.GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                invisibilidade = false;
            }

        }
        //           CAMARA LENTA


        if (Input.GetKeyDown("f"))
        {
            Debug.Log("camara lenta");
            if (this.GetComponent<personagem>().eventarioPSA.camaraLenta > 0 && !coolDown && !(this.GetComponent<personagem>().pwAtivo))
            {
                this.GetComponent<personagem>().pwAtivo = true;
                Debug.Log("Entrei na camara lenta");
                this.GetComponent<personagem>().PSAActivas.camaraLenta = true;
                this.GetComponent<personagem>().numeroPowerUps--;
                this.GetComponent<ContactoPersonagem>().removePowerUp("lento");
                this.GetComponent<ContactoPersonagem>().powerUpsDesativados();
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
                this.GetComponent<ContactoPersonagem>().powerUpsAtivados();
            }
        }

    }




}
