using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CaracterController : MonoBehaviour
{




    public float speed = 10;
    public Text countText;
    public Text winText;




    private float mouseSensitivityX = 1;
    private float mouseSensitivityY = 1;

    private Rigidbody2D rb;
    private float mass; //para mudar a massa
    private bool isMoving = false; // para ver se esta a andar ou nao
    private int count; //contdor de pickups apanhados



    // Current Movement Direction
    // (by default it moves to the right)
    //Vector2 dir = Vector2.right;


    // Start is called before the first frame update
    void Start()
    {
        // Move the Snake every 300ms
        //InvokeRepeating("Move", 0.3f, 0.3f);
        rb = GetComponent<Rigidbody2D>(); //vai buscar o componente rigidbody2d
        mass = rb.mass;


    }

    void Update()
    {
        MouseMove();
    }

    void FixedUpdate()
    {

        ArrowMove();


    }



    void MouseMove()
    {
        Vector3 mousePosition = Input.mousePosition; // codigo desnecessario, só para inicializar
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition); //a posição do rato passa a coordenadas do mundo de jogo


        Vector2 direction = new Vector2(
            mousePosition.x - transform.position.x,
            mousePosition.y - transform.position.y
            );

        transform.up = direction;

        if (Input.GetMouseButton(0)) // se clikar no rato 
        {
            Vector3 mouseTarget = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseTarget.z = transform.position.z; // guarda a posição que clicou

            if (isMoving == false)
            {
                isMoving = true;
            }
            if (isMoving == true)
            {
                // e enquanto estiver  a andar ele vai para o target a x velocidade
                transform.position = Vector3.MoveTowards(transform.position, mouseTarget, speed * Time.deltaTime);
            }
        }
        //transform.right = direction;
    }

    void ArrowMove()
    {
        //float moveHorizontal = Input.GetAxis("Horizontal");  //vai buscar as coordenadas xx usando as arrow keys
        //float moveVertical = Input.GetAxis("Vertical");

        //float moveHorizontal = Input.GetAxis("Mouse X") * mouseSensitivityX * Time.deltaTime;
        //float moveVertical = Input.GetAxis("Mouse Y") * mouseSensitivityY * Time.deltaTime;
        //float moveVertical = Input.GetMouseButtonDown(1.0);

        //Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        //transform.Translate(dir);
        //rb.AddForce(movement * speed); //adiciona uma forca ao rigidbody 

        var move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        transform.position += move * speed * Time.deltaTime;
        
           
        
    }


    // private void OnTriggerEnter2D(Collider2D other)
    // {

    //     if (other.gameObject.CompareTag("ParticulasA") || other.gameObject.CompareTag("ParticulasB"))  //se player colide com um pickup (particula A)
    //     {
    //         if (gameObject)  //mudar isto para verificar o tamanho das outras particulas se forem mais pequenas, come
    //         {
    //             //Destroy(other.gameObject);
    //             rb.mass++; //gain mass , it gets slow
    //             speed--;
    //             other.gameObject.SetActive(false); //poe o pickup (particula a a desaparecer do vista
    //             count++;
    //             setCountText();

    //             transform.localScale += new Vector3(0.1F, 0.1F, 0);
    //         }
    //         else
    //         {

    //         }
    //     }

    // }

    /*
         private  float distance_to_screen;
         private   Vector3 pos_move;

         void OnMouseDrag()
        {
            distance_to_screen = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
            pos_move = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen));
            transform.position = new Vector3(pos_move.x, pos_move.y, pos_move.z);
        }
    */


}



