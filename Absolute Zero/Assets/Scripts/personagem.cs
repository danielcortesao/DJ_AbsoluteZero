using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [System.Serializable]
// public class Boundary2D
// {
//     public float xMin, xMax, yMin, yMax;
// }

[System.Serializable]
public class Chaves
{
    public bool plasma = false,gasoso = false, liquido = false, solido = false;
    //Constructores Chaves
    public Chaves(){
    }
    public Chaves(bool p, bool g, bool l, bool s){
        plasma = p;
        gasoso = g;
        liquido = l;
        solido = s;
    }
}
[System.Serializable]
public class ParticulasSA
{
    public bool sonar, magnetico, invisibildade, camaraLenta;
        //Constructores Chaves
    public ParticulasSA(){
    }
    public ParticulasSA(bool s, bool m, bool i, bool c){
        sonar = s;
        magnetico = m;
        invisibildade = i;
        camaraLenta = c;
    }
}
[System.Serializable]
public class EventarioPSA
{
    public int sonar, magnetico, invisibildade, camaraLenta;
}



public class personagem : MonoBehaviour
{
    public int nivelTamanho;
    // public int nivelTamanho
    // {
    //     get {return (int)(tamanho*10); }
    //     set {tamanho = Mathf.Clamp(value/10.0f, 1, 10); }
    // } 
    private float tamanho; 
    public Chaves chaves;
    public ParticulasSA particulasSA;
    public EventarioPSA eventarioPSA;
    public float velocidade;
    public float gravity;
    // public Boundary2D boundary2d;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        nivelTamanho = 3;
        velocidade = 3;
    }

    //// Update is called once per frame
    //void Update()
    //{
    //}

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector2(moveHorizontal, moveVertical);
        rb.velocity = movement * velocidade;

        // rb.position = new Vector2(
        //     Mathf.Clamp(rb.position.x, boundary2d.xMin, boundary2d.xMax),
        //     Mathf.Clamp(rb.position.y, boundary2d.yMin, boundary2d.yMax)
        // );
    }
}
