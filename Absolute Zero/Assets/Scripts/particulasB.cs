using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class particulasB : MonoBehaviour
{
    public int nivelTamanho;
    public float velocidade;
    public Chaves chaves;
    public ParticulasSA particulasSA;
    public Rigidbody2D rb;
    public Rigidbody2D rb_target;
    const float G = 10f * (10 ^ 11); // Gravitational constant
    
    public float ChaseDistance; //alcance da particula b (distancia apartir do qual a B começa a perseguir o personagem
    private Transform target; // alvo que B vai perseguir (neste caso é sempre a personagem)
    private bool contacto = false;
    private float timeLeft = 10.0f;


    private float latestDirectionChangeTime;
    private readonly float directionChangeTime = 5f;
    private Vector2 movementDirection;
    private Vector2 movementPerSecond;
    private ArrayList filhosB = new ArrayList();


    //vars de area de movimento da particula
    public double centroX,centroY;
    public double d1Dentro, d2Dentro;
    public double d1Fora, d2Fora;
    public GameObject controladorCamada;
    public string nomeZona;



    //Vars para campo magnetico
    private bool isWithForce;
    private Vector2 vetorDaForca;
    private float valueForca;


    public GameObject personagem;

    //Constructores particulas B
    public particulasB(){
        nivelTamanho = 3;
        velocidade = 5;
    }
    public particulasB(int tam, float vel, Chaves cha, ParticulasSA psa){
        nivelTamanho = tam;
        velocidade = vel;
        chaves = cha;
        particulasSA = psa;
    }

    //Instanciar objecto de outro lado
    // public particulasB testB = new particulasB(3, 3.0f, new Chaves(), new ParticulasSA());

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>(); //poe o alvo a perseguir
        rb_target = target.GetComponent<Rigidbody2D>();

        latestDirectionChangeTime = 0f; // para movB
        CalcuateNewMovementVector();

        personagem = GameObject.FindGameObjectWithTag("Player");
        isWithForce = false;
    }

    void Update()
    {
        //codigo para  personagem();
        forcaGravitica();
        ResetContacto();
        //fim de codigo para perseguir personagem

        //codigo para movimentr B
        MovB();
        //fim de codigo para movimentar B
        // slow motion nas partículas
      

        //MovB();
        float novoTam = (float)nivelTamanho*0.1f+0.3f;
        rb.transform.localScale = new Vector3(novoTam,novoTam, 0);
        //fim de codigo para movimentar B

    }

    void MovB()
    {

        //if the changeTime was reached, calculate a new movement vector
        if (Time.time - latestDirectionChangeTime > directionChangeTime)
        {
            latestDirectionChangeTime = Time.time;
            CalcuateNewMovementVector();
        }

        Scene currentScene = SceneManager.GetActiveScene();

        // Retrieve the name of this scene.
        string sceneName = currentScene.name;

        if (sceneName == "Main")
        {

        //move enemy: 
        //implemementar condição para ficar dentro dos limites.
        
         Vector2 norte = new Vector2(0.0f, 30.0f);
         Vector2 sul = new Vector2(0.0f, -20.0f);
         Vector2 este = new Vector2(40.0f, 0.0f);
         Vector2 oeste = new Vector2(-40.0f, 0.0f);



         if (transform.localPosition.y >= norte.y)
             movementDirection = new Vector2(Random.Range(-1.0f, 1.0f), -1.0f).normalized;
         if (transform.localPosition.y <= sul.y)
             movementDirection = new Vector2(Random.Range(-1.0f, 1.0f), 1.0f).normalized;
         if (transform.localPosition.x >= este.x)
             movementDirection = new Vector2(-1.0f, Random.Range(-1.0f, 1.0f)).normalized;
         if (transform.localPosition.x <= oeste.x)
             movementDirection = new Vector2(1.0f, Random.Range(-1.0f, 1.0f)).normalized;

         movementPerSecond = movementDirection * velocidade;

         transform.localPosition = new Vector2(transform.localPosition.x + (movementPerSecond.x * Time.smoothDeltaTime),
         transform.localPosition.y + (movementPerSecond.y * Time.deltaTime));
        }

        if (sceneName == "Scene_Mundo")
        {
            double newPosicaoX = transform.localPosition.x + (movementPerSecond.x * Time.smoothDeltaTime);
            double newPosicaoY = transform.localPosition.y + movementPerSecond.y * Time.smoothDeltaTime;

            bool newPosicaoValida = false;
            //verificar se a nova posicao está dentro dos limites
            double verificarFora = Mathf.Pow((float)(newPosicaoX - centroX), 2) / Mathf.Pow((float)d2Fora, 2) + Mathf.Pow((float)(newPosicaoY - centroY), 2) / Mathf.Pow((float)d1Fora, 2);
            //Debug.Log(verificarFora);

            if (verificarFora <= 1.0)
            {
                //Debug.Log("Fora Check");
                if (d1Dentro == 0)
                {
                    newPosicaoValida = true;
                }
                else
                {
                    double verificarDentro = Mathf.Pow((float)(newPosicaoX - centroX), 2) / Mathf.Pow((float)d2Dentro, 2) + Mathf.Pow((float)(newPosicaoY - centroY), 2) / Mathf.Pow((float)d1Dentro, 2);
                    if (verificarFora <= 1.0)
                    {
                        newPosicaoValida = true;
                    }
                }

            }

            if (newPosicaoValida)
            {
                //Debug.Log("Moveu");
                transform.localPosition = new Vector2(transform.localPosition.x + (movementPerSecond.x * Time.smoothDeltaTime),
                                                transform.localPosition.y + (movementPerSecond.y * Time.smoothDeltaTime));
            }
            if (newPosicaoValida == false)
            {
                //Debug.Log("mudou direcao");
                // transform.position = new Vector2(transform.position.x  + (movementPerSecond.x * -1 * Time.smoothDeltaTime),
                //                                 transform.position.y  + (movementPerSecond.y * -1 * Time.smoothDeltaTime));
                CalcuateNewMovementVector();
            
            }
        }

    }

    


    void CalcuateNewMovementVector()
    {
        //create a random direction vector with the magnitude of 1, later multiply it with the velocity of the enemy
        movementDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;
        movementPerSecond = movementDirection * velocidade;
    }

    /*

        public class Example : MonoBehaviour {
        public float speed = 5.0f;
        public Vector3 direction;

        void Start() 
        {
            direction = (new Vector3(Random.Range(-1.0f,1.0f), Random.Range(-1.0f,1.0f),0.0f)).normalized;
            transform.Rotate(direction);
        }

        void Update()
        {
            Vector3 newPos = transform.position + direction * speed * Time.deltaTime;
            rigidbody.MovePosition (newPos);
        }

        void OnCollisionEnter (Collision col)
        {
            Debug.Log ("Collision");
            if (col.gameObject.tag == "Muri")   
            {
                direction = col.contacts[0].normal;
                direction = Quaternion.AngleAxis(Random.Range(-70.0f, 70.0f), Vector3.forward) * direction;
                transform.rotation = Quaternion.LookRotation(direction);
            }
        }
    }


       */

    void ResetContacto()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            contacto = false;
            timeLeft = 10.0f;
        }
    }

    void forcaGravitica()
    {
        //http://forum.brackeys.com/thread/2d-planetary-gravity/
        float mass1 = rb.mass; // Planets mass
        float mass2 = rb_target.mass; // this is 10, its Player mass
        Vector2 direction = transform.position - target.transform.position; // Direction to apply the force
        float dist = Vector2.Distance(transform.position, target.transform.position); // Distance between player and planet
        float force = G * ((mass1 * mass2) / (dist * dist)); // The force that should be applied
                                                             //        Debug.Log(force);

        //só fazer a certa distancia do utilizador
        if (!(personagem.GetComponent<personagem>().PSAActivas.magnetico))
        {
            if(dist<20){
                /*
                if(isWithForce){
                    rb.AddForce(-vetorDaForca * valueForca);
                    isWithForce = false;
                }*/
                if (gameObject.transform.localScale.x >= target.transform.localScale.x)
                { // se B for maior que personagem atrai personagem

                    rb.AddForce(-direction * force * (float)0.6); // Adding the force to the player 
                    if(isWithForce){
                        vetorDaForca = vetorDaForca + (-direction*force * (float)0.6);
                    }
                    else{
                        vetorDaForca = -direction*force * (float)0.6;
                    }
                    isWithForce = true;

                }
                else
                {// se B for menor que personagem é repelida 
                    rb.AddForce(direction * force * (float)0.6); // Adding the force to the player 
                    if(isWithForce){
                        vetorDaForca = vetorDaForca + (direction*force * (float)0.6);
                    }
                    else{
                        vetorDaForca = direction*force * (float)0.6;
                    }
                    isWithForce = true;
                }   
            }
            else if(dist > 30){
                 if(isWithForce){
                    rb.AddForce(-vetorDaForca);
                    isWithForce = false;
                }
            }
        }
        else {
            if(isWithForce){
                rb.AddForce(-vetorDaForca);
                isWithForce = false;
            }
        }


       

    }


    public int qtdChaves(){
        int filhos = 0;
        filhos = CountTrue(chaves.plasma, chaves.gasoso,chaves.liquido, chaves.solido);
        return filhos;
    }
        public int qtdPSA(){
        int filhos = 0;
        filhos = CountTrue(particulasSA.sonar, particulasSA.magnetico, particulasSA.invisibildade, particulasSA.camaraLenta);
        return filhos;
    }
    public int qtdFilhosB(){
        int filhos = 0;
        filhos = CountTrue(chaves.plasma, chaves.gasoso,chaves.liquido, chaves.solido,particulasSA.sonar, particulasSA.magnetico, particulasSA.invisibildade, particulasSA.camaraLenta);
        return filhos;
    }
    public ArrayList arrayFilhosB(){
        if(chaves.plasma == true){filhosB.Add(("plasma"));}
        if(chaves.gasoso == true){filhosB.Add(("gasoso"));}
        if(chaves.liquido == true){filhosB.Add(("liquido"));}
        if(chaves.solido == true){filhosB.Add(("solido"));}

        if(particulasSA.sonar == true){filhosB.Add(("sonar"));}
        if(particulasSA.magnetico == true){filhosB.Add(("magnetico"));}
        if(particulasSA.invisibildade == true){filhosB.Add(("invisibildade"));}
        if(particulasSA.camaraLenta == true){filhosB.Add(("camaraLenta"));}
        return filhosB;
    }
    // public ArrayList arrayFilhosB(){
    //     filhosB.Add(chaves.plasma ? 1 : 0);
    //     filhosB.Add(chaves.gasoso ? 1 : 0);
    //     filhosB.Add(chaves.liquido ? 1 : 0);
    //     filhosB.Add(chaves.solido ? 1 : 0);
    //     filhosB.Add(particulasSA.sonar);
    //     filhosB.Add(particulasSA.magnetico);
    //     filhosB.Add(particulasSA.invisibildade);
    //     filhosB.Add(particulasSA.camaraLenta);
    //     return filhosB;
    // }

    public static int CountTrue(params bool[] args)
    {
    return args.Count(t => t);
    }

    public void reposicao()
    {

        // Create a temporary reference to the current scene.
        Scene currentScene = SceneManager.GetActiveScene();

        // Retrieve the name of this scene.
        string sceneName = currentScene.name;

        if (sceneName == "Scene_Mundo")
        {
            controladorCamada.GetComponent<GeradorDeCamadas>().reposicaoParticula(nomeZona);
        }
    }

}
