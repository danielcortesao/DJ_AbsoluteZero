using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class ContactoPersonagem : MonoBehaviour
{
    private float timeLastImpact = -0.4f;
    private float deltaTime = 0.5f;
    private GameObject[] allObjectsC;
    
    public AudioSource[]audioSource;
    private Rigidbody2D rb;
    public GameObject criaParticulas;

    public GameObject caixaTexto;
    public Text texto;
    public GameObject imgTutMov;
    public GameObject imgTutSonar;
    public GameObject imgTutMagnetico;
    public GameObject imgTutInvisivel;
    public GameObject imgTutLento;
    public GameObject imgTutChave;
    public GameObject imgTutTransicao;

    public GameObject[] powerUpsOnPersonagem;
    public GameObject chaveGasosoOnPersonagem;
    public GameObject chaveLiquidoOnPersonagem;
    public GameObject chavePlasmaOnPersonagem;

    public GameObject ChavePrefab;
    public GameObject PowerUpPrefab;
    public GameObject groupPowerUp;
    public Sprite spriteSonar;
    public Sprite spriteMagnetico;
    public Sprite spriteInvisivel;
    public Sprite spriteLento;
    public Sprite spriteGasoso;
    public Sprite spriteLiquido;
    public Sprite spritePlasma;

    public bool ajudaMov;
    public bool ajudaGasoso;
    public bool ajudaMagnetico;
    public bool ajudaSonar;
    public bool ajudaLento;
    public bool ajudaInvisibilidade;
    public bool ajudaMicro;


    public Button botaoAjuda;

    private bool parado;
    private string frase;


    int tamanhoNextLevel;

    private void Start()
    {
        audioSource = GameObject.FindGameObjectWithTag("sons").GetComponentsInChildren<AudioSource>();

        ajudaMov = false;
        ajudaGasoso = false;
        ajudaMagnetico = false;
        ajudaSonar = false;
        ajudaLento = false;
        ajudaInvisibilidade = false;
        ajudaMicro = false;
        botaoAjuda.onClick.AddListener(carregaAjuda);
        chaveGasosoOnPersonagem.SetActive(false);
        chaveLiquidoOnPersonagem.SetActive(false);
        chavePlasmaOnPersonagem.SetActive(false);

        powerUpsOnPersonagem = GameObject.FindGameObjectsWithTag("PowerUpsOnChar");
        foreach(GameObject powerUp in powerUpsOnPersonagem){
            powerUp.SetActive(false);
        }
        tamanhoNextLevel = 0;

        if (!ajudaMov)
        {
            caixaTexto.SetActive(true);
            imgTutMov.SetActive(true);
            parado = true;
            frase = "Para deslocar a personagem use as" + '\n' +"teclas das setas, WASD ou o rato.";
            Time.timeScale = 0.0f;
            if (Input.GetKeyDown("space"))
            {
                Time.timeScale = 1.0f;
            }
            Time.timeScale = 0.0f;
            ajudaMov = true;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {   //Correr apenas se passou 1 segundo
        if(Time.time >= timeLastImpact + deltaTime){

            if(other.gameObject.CompareTag("ParticulasA")){
                //Se Player >= outro objecto (localScale tem que ser alterado para o parametro do tamanho)
                if (gameObject.transform.localScale.x >= other.transform.localScale.x)
                {
                    // int diferenca = gameObject.GetComponent<personagem>().nivelTamanho - other.gameObject.GetComponent<particulasA>().nivelTamanho;
                    // if(diferenca == 2){
                    //     tamanhoNextLevel += 50;
                    // }
                    // else if(diferenca == 3){
                    //     tamanhoNextLevel += 30;
                    // }
                    // else if(diferenca == 4){
                    //     tamanhoNextLevel += 25;
                    // }
                    // else if(diferenca == 5){
                    //     tamanhoNextLevel += 20;
                    // }
                    // else if(diferenca == 6){
                    //     tamanhoNextLevel += 15;
                    // }
                    // else if(diferenca == 7){
                    //     tamanhoNextLevel += 5;
                    // }
                    // else if(diferenca == 8){
                    //     tamanhoNextLevel += 2;
                    // }
                    // Debug.Log(tamanhoNextLevel);
                    // Debug.Log(diferenca);


                    // if(gameObject.GetComponent<personagem>().nivelTamanho < 9 && tamanhoNextLevel>=100){
                    //     gameObject.GetComponent<personagem>().nivelTamanho +=1;
                    //     tamanhoNextLevel = 0;
                    // }
                        
                        if(gameObject.GetComponent<personagem>().nivelTamanho < 9){
                            gameObject.GetComponent<personagem>().nivelTamanho +=1;
                        }
                        
                    // if(gameObject.transform.localScale.x < 1.3f) { 
                    //     gameObject.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
                    // }
                }
                else
                {//Se Player < outro objecto
                    if(gameObject.GetComponent<personagem>().nivelTamanho > 1){
                        gameObject.GetComponent<personagem>().nivelTamanho -=1;
                    }

                    //                 if (gameObject.transform.localScale.x >= 0.4)
                    // {
                    //     gameObject.transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
                    // }
                    // if (gameObject.transform.localScale.x >= 0.4)
                    // {
                    //     gameObject.transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
                    // }
                }
                timeLastImpact = Time.time;
            } 
            else if(other.gameObject.CompareTag("ParticulasB")){
                //Se Player >= outro objecto (localScale tem que ser alterado para o parametro do tamanho)
                if (gameObject.transform.localScale.x >= other.transform.localScale.x)
                {
                    //Não acontece nada ao player
                }
                else
                {   
                    //persoangem perde particula C de forma aleatoria
                    if(arrayFilhos().Count > 0){
                        audioSource[6].Play();
                        //gameObject.GetComponent<personagem>().numeroPowerUps--;
                        int index = Random.Range(0, arrayFilhos().Count);
                        string str = (string)arrayFilhos()[index];
                        print(str);
                        removeEventario((string)arrayFilhos()[index]);
                        destroiFilho(gameObject, str);
                    }
                }
                timeLastImpact = Time.time;
            }
            else if(other.gameObject.CompareTag("ParticulasC")){
                //se está activa, as personagem ganha essa chave / PSA
                if(other.gameObject.GetComponent<particulasC>().ativa == true)
                    {
                    //Som para amanha power up
                    audioSource[2].Play();
                    if (other.gameObject.GetComponent<particulasC>().chaves.plasma == true && !gameObject.GetComponent<personagem>().chaves.plasma)
                    {
                        gameObject.GetComponent<personagem>().chaves.plasma = true;
                        chavePlasmaOnPersonagem.SetActive(true);

                        // descativa todas as particulas C
                            allObjectsC = other.gameObject.GetComponent<particulasC>().irmaos;

                            for(int j = 0; j< other.gameObject.GetComponent<particulasC>().numIrmaos; j++){
                                    other.gameObject.GetComponent<particulasC>().irmaos[j].GetComponent<particulasC>().ativa = false;
                            }
                        Destroy(other.gameObject);
                    }
                    else if (other.gameObject.GetComponent<particulasC>().chaves.gasoso == true && !gameObject.GetComponent<personagem>().chaves.gasoso)
                    {

                        gameObject.GetComponent<personagem>().chaves.gasoso = true;

                        // descativa todas as particulas C
                            allObjectsC = other.gameObject.GetComponent<particulasC>().irmaos;

                            for(int j = 0; j< other.gameObject.GetComponent<particulasC>().numIrmaos; j++){
                                    other.gameObject.GetComponent<particulasC>().irmaos[j].GetComponent<particulasC>().ativa = false;
                            }
                        if (!ajudaGasoso)
                        {
                            caixaTexto.SetActive(true);
                            imgTutChave.SetActive(true);
                            parado = true;
                            frase = "Ganhaste uma chave!"+'\n'+"Esta dá-te acesso a uma nova dimensão.";
                            Time.timeScale = 0.0f;
                            if (Input.GetKeyDown("space"))
                            {
                                Time.timeScale = 1.0f;
                            }
                            Time.timeScale = 0.0f;
                            ajudaGasoso = true;
                        }

                        chaveGasosoOnPersonagem.SetActive(true);
                        Destroy(other.gameObject);
                    }
                    else if(other.gameObject.GetComponent<particulasC>().chaves.liquido == true && !gameObject.GetComponent<personagem>().chaves.liquido){
                        gameObject.GetComponent<personagem>().chaves.liquido = true;

                        chaveLiquidoOnPersonagem.SetActive(true);

                        // descativa todas as particulas C
                            allObjectsC = other.gameObject.GetComponent<particulasC>().irmaos;

                            for(int j = 0; j< other.gameObject.GetComponent<particulasC>().numIrmaos; j++){
                                    other.gameObject.GetComponent<particulasC>().irmaos[j].GetComponent<particulasC>().ativa = false;
                            }
                        Destroy(other.gameObject);
                    }
                    else if(other.gameObject.GetComponent<particulasC>().chaves.solido == true && !gameObject.GetComponent<personagem>().chaves.solido){
                        gameObject.GetComponent<personagem>().chaves.solido = true;
                        chavePlasmaOnPersonagem.SetActive(true);

                        // descativa todas as particulas C
                            allObjectsC = other.gameObject.GetComponent<particulasC>().irmaos;

                            for(int j = 0; j< other.gameObject.GetComponent<particulasC>().numIrmaos; j++){
                                    other.gameObject.GetComponent<particulasC>().irmaos[j].GetComponent<particulasC>().ativa = false;
                            }
                        Destroy(other.gameObject);
                    }
                    else if(other.gameObject.GetComponent<particulasC>().particulasSA.sonar == true && gameObject.GetComponent<personagem>().numeroPowerUps<8){
                        gameObject.GetComponent<personagem>().eventarioPSA.sonar += 1;
                        gameObject.GetComponent<personagem>().numeroPowerUps+=1;

                        // descativa todas as particulas C
                            allObjectsC = other.gameObject.GetComponent<particulasC>().irmaos;

                            for(int j = 0; j< other.gameObject.GetComponent<particulasC>().numIrmaos; j++){
                                    other.gameObject.GetComponent<particulasC>().irmaos[j].GetComponent<particulasC>().ativa = false;
                            }
                        if (!ajudaSonar)
                        {
                            caixaTexto.SetActive(true);
                            frase = "Ganhaste uma partícula sub-atómica!" + '\n'+" Para usares pressiona a tecla 1.";
                            texto.text = frase;
                            imgTutSonar.SetActive(true);

                            parado = true;
                            Time.timeScale = 0.0f;
                            if (Input.GetKeyDown("space"))
                            {
                                Time.timeScale = 1.0f;
                            }
                            Time.timeScale = 0.0f;
                            ajudaSonar = true;
                        }
                        addPowerUp("sonar");
                        Destroy(other.gameObject);
                    }
                    else if(other.gameObject.GetComponent<particulasC>().particulasSA.magnetico == true  && gameObject.GetComponent<personagem>().numeroPowerUps<8){

                        gameObject.GetComponent<personagem>().eventarioPSA.magnetico += 1;
                        gameObject.GetComponent<personagem>().numeroPowerUps+=1;

                        // descativa todas as particulas C
                            allObjectsC = other.gameObject.GetComponent<particulasC>().irmaos;

                            for(int j = 0; j< other.gameObject.GetComponent<particulasC>().numIrmaos; j++){
                                    other.gameObject.GetComponent<particulasC>().irmaos[j].GetComponent<particulasC>().ativa = false;
                            }
                        if (!ajudaMagnetico)
                        {
                                caixaTexto.SetActive(true);
                                imgTutMagnetico.SetActive(true);
                                frase = "Ganhaste uma partícula sub-atómica!" + '\n' + " Para usares pressiona a tecla 2.";
                                parado = true;
                                Time.timeScale = 0.0f;
                                if (Input.GetKeyDown("space"))
                                {
                                    Time.timeScale = 1.0f;
                                }
                                Time.timeScale = 0.0f;
                                ajudaMagnetico = true;
                        }
                        addPowerUp("magnetico");
                        Destroy(other.gameObject);
                    }
                    else if(other.gameObject.GetComponent<particulasC>().particulasSA.invisibildade == true  && gameObject.GetComponent<personagem>().numeroPowerUps<8){
                        gameObject.GetComponent<personagem>().eventarioPSA.invisibildade += 1;
                        gameObject.GetComponent<personagem>().numeroPowerUps+=1;
                        // descativa todas as particulas C
                            allObjectsC = other.gameObject.GetComponent<particulasC>().irmaos;

                            for(int j = 0; j< other.gameObject.GetComponent<particulasC>().numIrmaos; j++){
                                    other.gameObject.GetComponent<particulasC>().irmaos[j].GetComponent<particulasC>().ativa = false;
                            }

                        if (!ajudaInvisibilidade)
                        {
                                caixaTexto.SetActive(true);
                                imgTutInvisivel.SetActive(true);
                                frase = "Ganhaste uma partícula sub-atómica!" + '\n' + "  Para usares pressiona a tecla 3.";
                                parado = true;
                                Time.timeScale = 0.0f;
                                if (Input.GetKeyDown("space"))
                                {
                                    Time.timeScale = 1.0f;
                                }
                                Time.timeScale = 0.0f;
                                ajudaInvisibilidade = true;
                        }
                        addPowerUp("invisibildade");
                        Destroy(other.gameObject);
                    }
                    else if(other.gameObject.GetComponent<particulasC>().particulasSA.camaraLenta == true  && gameObject.GetComponent<personagem>().numeroPowerUps<8){
                            gameObject.GetComponent<personagem>().eventarioPSA.camaraLenta += 1;
                            gameObject.GetComponent<personagem>().numeroPowerUps+=1;
                            // descativa todas as particulas C
                            allObjectsC = other.gameObject.GetComponent<particulasC>().irmaos;

                            for(int j = 0; j< other.gameObject.GetComponent<particulasC>().numIrmaos; j++){
                                    other.gameObject.GetComponent<particulasC>().irmaos[j].GetComponent<particulasC>().ativa = false;
                            }
                            if (!ajudaLento)
                            {
                                caixaTexto.SetActive(true);
                                imgTutInvisivel.SetActive(true);
                                frase = "Ganhaste uma partícula sub-atómica! " + '\n' + " Para usares pressiona a tecla 4.";
                                parado = true;
                                Time.timeScale = 0.0f;
                                if (Input.GetKeyDown("space"))
                                {
                                    Time.timeScale = 1.0f;
                                }
                                Time.timeScale = 0.0f;
                                ajudaLento = true;
                            }
                            addPowerUp("lento");
                            Destroy(other.gameObject);
                    }                    
                }
                timeLastImpact = Time.time;

                
            }
        }
        //Destroy(gameObject);
    }
    public ArrayList arrayFilhos(){
        ArrayList filhos = new ArrayList();
        if(gameObject.GetComponent<personagem>().chaves.plasma == true){filhos.Add(("plasma"));}
        if(gameObject.GetComponent<personagem>().chaves.gasoso == true){filhos.Add(("gasoso"));}
        if(gameObject.GetComponent<personagem>().chaves.liquido == true){filhos.Add(("liquido"));}
        if(gameObject.GetComponent<personagem>().chaves.solido == true){filhos.Add(("solido"));}

        if(gameObject.GetComponent<personagem>().eventarioPSA.sonar >= 1){filhos.Add(("sonar"));}
        if(gameObject.GetComponent<personagem>().eventarioPSA.magnetico >= 1){filhos.Add(("magnetico"));}
        if(gameObject.GetComponent<personagem>().eventarioPSA.invisibildade >= 1){filhos.Add(("invisibildade"));}
        if(gameObject.GetComponent<personagem>().eventarioPSA.camaraLenta >= 1){filhos.Add(("camaraLenta"));}
        return filhos;
    }
    public void destroiFilho(GameObject go, string c){
        rb = go.GetComponent<Rigidbody2D>();
        // istantiate an object of the assigned public variable gameObect with coordinates ranging betwen min and max.
        //Vector3 position = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), Z);
        if(c == "plasma" || c == "gasoso" || c == "liquido" || c == "solido"){
            GameObject tmpObj =  Instantiate(ChavePrefab, rb.position, Quaternion.identity);
            tmpObj.gameObject.GetComponent<particulasC>().activaChaves(c,false);
            if(c=="plasma"){ //vermelho 
                tmpObj.GetComponent<SpriteRenderer>().sprite = spritePlasma;
            }
            else if(c=="gasoso"){ // verde
                tmpObj.GetComponent<SpriteRenderer>().sprite = spriteGasoso;
            }
            else if(c=="liquido"){ // AZUL
                tmpObj.GetComponent<SpriteRenderer>().sprite = spriteLiquido;
            }
            else{ // cinza
                //tmpObj.GetComponent<SpriteRenderer>().color = new Color(0,0,0,1);
            }
            //criaParticulas.GetComponent<particulasC>().activaChaves(c,false);
            //Destroi filhos passados 5 segundos
            Destroy(tmpObj,5.0f);
        }
        else if(c == "sonar" || c == "magnetico" || c == "invisibildade" || c == "camaraLenta"){
            GameObject tmpObj =  Instantiate(PowerUpPrefab, rb.position, Quaternion.identity);
            tmpObj.gameObject.GetComponent<particulasC>().activaChaves(c,false);
            if(c=="sonar"){ //branco 
                //tmpObj.GetComponent<SpriteRenderer>().color = new Color(1,1,1,1);
                //Sprite spriteAdd = Resources.Load<Sprite>("Assets/imgs/ParticulasImgs/powerUp1.png");
                tmpObj.GetComponent<SpriteRenderer>().sprite = spriteSonar;
            }
            else if(c=="magnetico"){ // roxo
                tmpObj.GetComponent<SpriteRenderer>().sprite = spriteMagnetico;
            }
            else if(c=="invisibildade"){ // preto
                tmpObj.GetComponent<SpriteRenderer>().sprite = spriteInvisivel;
            }
            else if (c == "camaraLenta"){ // laranja
                tmpObj.GetComponent<SpriteRenderer>().sprite = spriteLento;
            }   
            removePowerUp(c);
            //criaParticulas.GetComponent<particulasC>().activaPSA(c,false);
            //Destroi filhos passados 5 segundos
            Destroy(tmpObj,5.0f);
        }
        
    }
    private void removeEventario(string s){
        if(s == "plasma"){
            gameObject.GetComponent<personagem>().chaves.plasma = false;
            chavePlasmaOnPersonagem.SetActive(false);
        }
        else if(s == "gasoso"){
            gameObject.GetComponent<personagem>().chaves.gasoso = false;
            chaveGasosoOnPersonagem.SetActive(false);
        }
        else if(s == "liquido"){
            gameObject.GetComponent<personagem>().chaves.liquido = false;
            chaveLiquidoOnPersonagem.SetActive(false);
        }
        else if(s == "solido"){
            gameObject.GetComponent<personagem>().chaves.solido = false;}

        else if(s == "sonar"){
            gameObject.GetComponent<personagem>().eventarioPSA.sonar -= 1;
            gameObject.GetComponent<personagem>().numeroPowerUps--;}
        else if(s == "magnetico"){
            gameObject.GetComponent<personagem>().eventarioPSA.magnetico -= 1;
            gameObject.GetComponent<personagem>().numeroPowerUps--;}
        else if(s == "invisibildade"){
            gameObject.GetComponent<personagem>().eventarioPSA.invisibildade -= 1;
            gameObject.GetComponent<personagem>().numeroPowerUps--;
        }
        else if(s == "camaraLenta"){
            gameObject.GetComponent<personagem>().eventarioPSA.camaraLenta -= 1;
            gameObject.GetComponent<personagem>().numeroPowerUps--;
        }

    }

   
    private void Update()
    {
        if(parado){
            texto.text = frase;
            //img = GetComponent<Image>();

            /*if (Input.GetKeyDown("space"))
            {
                caixaTexto.SetActive(false);
                Time.timeScale = 1.0f;
                parado = false;
            }*/
        }
    }


     void carregaAjuda(){
        Debug.Log("You have clicked the button!");

        caixaTexto.SetActive(false);
        imgTutSonar.SetActive(false);
        imgTutMagnetico.SetActive(false);
        imgTutInvisivel.SetActive(false);
        imgTutLento.SetActive(false);
        imgTutMov.SetActive(false);
        imgTutChave.SetActive(false);
        imgTutTransicao.SetActive(false);
        

        Time.timeScale = 1.0f;
        parado = false;
    }

     public void addPowerUp(string nomePowerUp){
        if(nomePowerUp == "sonar"){
            powerUpsOnPersonagem[gameObject.GetComponent<personagem>().numeroPowerUps-1].SetActive(true);
            powerUpsOnPersonagem[gameObject.GetComponent<personagem>().numeroPowerUps-1].GetComponent<SpriteRenderer>().sprite = spriteSonar;
        }
        else if(nomePowerUp == "magnetico"){
            powerUpsOnPersonagem[gameObject.GetComponent<personagem>().numeroPowerUps-1].SetActive(true);
            powerUpsOnPersonagem[gameObject.GetComponent<personagem>().numeroPowerUps-1].GetComponent<SpriteRenderer>().sprite = spriteMagnetico;
        }
        else if(nomePowerUp == "invisibildade"){
            powerUpsOnPersonagem[gameObject.GetComponent<personagem>().numeroPowerUps-1].SetActive(true);
            powerUpsOnPersonagem[gameObject.GetComponent<personagem>().numeroPowerUps-1].GetComponent<SpriteRenderer>().sprite = spriteInvisivel;
        }
        else{
            powerUpsOnPersonagem[gameObject.GetComponent<personagem>().numeroPowerUps-1].SetActive(true);
            powerUpsOnPersonagem[gameObject.GetComponent<personagem>().numeroPowerUps-1].GetComponent<SpriteRenderer>().sprite = spriteLento;
        }
    }

    public void removePowerUp(string nomePowerUp){
        //Debug.Log(lala == this.GetComponent<SpriteRenderer>().sprite);  

        int i, powerUpRemove;
        i=gameObject.GetComponent<personagem>().numeroPowerUps;
        if(nomePowerUp == "sonar"){  
            while(powerUpsOnPersonagem[i].GetComponent<SpriteRenderer>().sprite != spriteSonar){
                i--;
            }
        }
        else if(nomePowerUp == "magnetico"){
            while(powerUpsOnPersonagem[i].GetComponent<SpriteRenderer>().sprite != spriteMagnetico){
                i--;
            }
        }
        else if(nomePowerUp == "invisibildade"){
            while(powerUpsOnPersonagem[i].GetComponent<SpriteRenderer>().sprite != spriteInvisivel){
                i--;
            }
        }
        else if(nomePowerUp == "camaraLenta"){
            while(powerUpsOnPersonagem[i].GetComponent<SpriteRenderer>().sprite != spriteLento){
                i--;
            }
        }

        powerUpRemove = i;

        for(i=powerUpRemove;i<gameObject.GetComponent<personagem>().numeroPowerUps;i++){
            powerUpsOnPersonagem[i].GetComponent<SpriteRenderer>().sprite = powerUpsOnPersonagem[i+1].GetComponent<SpriteRenderer>().sprite;
        }


        powerUpsOnPersonagem[gameObject.GetComponent<personagem>().numeroPowerUps].SetActive(false);
    }

    public void powerUpsDesativados(){
        int i;
        for(i=0;i<8;i++){
            powerUpsOnPersonagem[i].GetComponent<SpriteRenderer>().color = new Color((float)0.5,(float)0.5,(float)0.5,(float)0.5);
            groupPowerUp.GetComponent<Rotate>().enabled =false;
        }
    }

    public void powerUpsAtivados(){
        int i;
        for(i=0;i<8;i++){
            powerUpsOnPersonagem[i].GetComponent<SpriteRenderer>().color = new Color(1,1,1,1);
            groupPowerUp.GetComponent<Rotate>().enabled = true;
        }
    }
  


}
