using System.Collections;
using System.Linq;
using UnityEngine;

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
    private readonly float directionChangeTime = 3f;
    private Vector2 movementDirection;
    private Vector2 movementPerSecond;
    private ArrayList filhosB = new ArrayList();


    //vars de area de movimento da particula
    public double centroX,centroY;
    public double d1Dentro, d2Dentro;
    public double d1Fora, d2Fora;

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
        calcuateNewMovementVector();
    }

    void Update()
    {
        //codigo para  personagem();
        forcaGravitica();
        ResetContacto();
        //fim de codigo para perseguir personagem

        //codigo para movimentr B
        MovB();
        float novoTam = (float)nivelTamanho*0.1f+0.3f;
        rb.transform.localScale = new Vector3(novoTam,novoTam, 0);;
        //fim de codigo para movimentar B
        
    }

    void MovB()
    {

        //if the changeTime was reached, calculate a new movement vector
        /*if (Time.time - latestDirectionChangeTime > directionChangeTime)
        {
            latestDirectionChangeTime = Time.time;
            calcuateNewMovementVector();
        }
        */

        // //if the changeTime was reached, calculate a new movement vector
        // if (Time.time - latestDirectionChangeTime > directionChangeTime)
        // {
        //     latestDirectionChangeTime = Time.time;
        //     calcuateNewMovementVector();
        // }



        //move enemy: 
        //implemementar condição para ficar dentro dos limites.
        Vector2 norte = new Vector2(0.0f, 30.0f);
        Vector2 sul = new Vector2(0.0f, -20.0f);
        Vector2 este = new Vector2(40.0f, 0.0f);
        Vector2 oeste = new Vector2(-40.0f, 0.0f);
            
        

        if (transform.position.y >= norte.y)
            movementDirection = new Vector2(Random.Range(-1.0f, 1.0f), -1.0f).normalized;
        if (transform.position.y <= sul.y)
            movementDirection = new Vector2(Random.Range(-1.0f, 1.0f), 1.0f).normalized;
        if (transform.position.x >= este.x)
            movementDirection = new Vector2(-1.0f, Random.Range(-1.0f, 1.0f)).normalized;
        if (transform.position.x <= oeste.x)
            movementDirection = new Vector2(1.0f, Random.Range(-1.0f, 1.0f)).normalized;

        movementPerSecond = movementDirection * velocidade;

        transform.position = new Vector2(transform.position.x + (movementPerSecond.x * Time.deltaTime),
        transform.position.y + (movementPerSecond.y * Time.deltaTime));
    }

    


    void calcuateNewMovementVector()
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
        if (gameObject.transform.localScale.x >= target.transform.localScale.x){ // se B for maior que personagem atrai personagem
            rb.AddForce(-direction * force); // Adding the force to the player 
        }
        else{// se B for menor que personagem é repelida 
            rb.AddForce(direction * force); // Adding the force to the player 
        }

        // if (contacto == false)
        // {
           

        //     if (gameObject.transform.localScale.x >= target.transform.localScale.x) // se B for maior que persongame vai perseguir
        //         if (Vector2.Distance(transform.position, target.position) < ChaseDistance) // B persegue desde que este no seu aclance
        //             if (Vector2.Distance(transform.position, target.position) > 0.5)  //para de perseguir  esta distancia
        //             {
        //                 if (Vector2.Distance(transform.position, target.position) <= 1)
        //                     contacto = true;

        //                 transform.position = Vector2.MoveTowards(transform.position, target.position, velocidade * Time.deltaTime);
        //             }
            
        // }

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

}
