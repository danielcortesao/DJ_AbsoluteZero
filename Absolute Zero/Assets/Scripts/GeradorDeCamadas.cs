using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorDeCamadas : MonoBehaviour {
	public string camada;
	public GameObject particulaA;
	public GameObject particulaB;
	
	// Use this for initialization
	void Start () {
		if(camada.Equals("LiquidTut")){
			//primeiras particulas
			UnityEngine.Random.InitState((int)System.DateTime.Now.Ticks);
			gerarStart_LiqTut();
			int i, numA15 = 30, numBS = 40, numBCG = 20;
			for(i = 0;i<numA15;i++){
				gerarA15_LiqTut();
			}
			for(i = 0;i<numBS;i++){
				gerarB_S_LiqTut();
			}
			for(i = 0;i<numBCG;i++){
				gerarB_CG_LiqTut();
			}
		}
		else if(camada.Equals("GasosoTut")){
			//primeiras particulas
			UnityEngine.Random.InitState((int)System.DateTime.Now.Ticks);
			int i, numBM = 8, numA15 = 20, numA511 = 30, numBS = 40, numBCPMP = 10;
			for(i = 0;i<numBM;i++){
				gerarB_M_GasTut();
			}
			for(i = 0;i<numA15;i++){
				gerarA15_GasTut();
			}
			for(i = 0;i<numA511;i++){
				gerarA511_GasTut();
			}
			for(i = 0;i<numBS;i++){
				gerarB_S_GasTut();
			}
			for(i = 0;i<numBCPMP;i++){
				gerarB_Cp_M_P_GasTut();
			}
		}
		else if(camada.Equals("PlasmaTut")){
			//primeiras particulas
			UnityEngine.Random.InitState((int)System.DateTime.Now.Ticks);
			int i, numBC= 10, numBI = 20, numBCG = 40;
			for(i = 0;i<numBC;i++){
				gerarB_C_PlasmaTut();
			}
			for(i = 0;i<numBI;i++){
				gerarB_I_PlasmaTut();
			}
			for(i = 0;i<numBCG;i++){
				gerarB_Cg_PlasmaTut();
			}
		}


	}
	
	// Update is called once per frame
	void Update () {
		if(camada.Equals("LiquidTut")){
			//gerar zona A 1-5 
			if (Input.GetKeyDown(KeyCode.A))
	        {
	        	//fazer random consoante o max e min
	        	gerarA15_LiqTut();
	        }
	        //gerar zona B - S 
			if (Input.GetKeyDown(KeyCode.S))
	        {
	        	gerarB_S_LiqTut();

	        }
	        //gerar zona B - Cg 
			if (Input.GetKeyDown(KeyCode.D))
	        {
	        	gerarB_CG_LiqTut();
	        }
    	}
    	else if(camada.Equals("GasosoTut")){
			//gerar zona A 1-5 
			if (Input.GetKeyDown(KeyCode.A))
	        {
	        	//fazer random consoante o max e min
	        	gerarB_M_GasTut();
	        }
	        //gerar zona B - S 
			if (Input.GetKeyDown(KeyCode.S))
	        {
	        	gerarA15_GasTut();

	        }
	        //gerar zona B - Cg 
			if (Input.GetKeyDown(KeyCode.D))
	        {
	        	gerarA511_GasTut();
	        }
	        //gerar zona B - Cg 
			if (Input.GetKeyDown(KeyCode.Q))
	        {
	        	gerarB_S_GasTut();
	        }
	         //gerar zona B - Cg 
			if (Input.GetKeyDown(KeyCode.W))
	        {
	        	gerarB_Cp_M_P_GasTut();
	        }
    	}
    	else if(camada.Equals("PlasmaTut")){
    		
			//gerar zona A 1-5 
			if (Input.GetKeyDown(KeyCode.A))
	        {
	        	//fazer random consoante o max e min
	        	 gerarB_C_PlasmaTut();
	        }
	        //gerar zona B - S 
			if (Input.GetKeyDown(KeyCode.S))
	        {
	        	gerarB_I_PlasmaTut();

	        }
	        //gerar zona B - Cg 
			if (Input.GetKeyDown(KeyCode.D))
	        {
	        	gerarB_Cg_PlasmaTut();
	        }
    	}
	}

	public void reposicaoParticula(string nomeCamada){
		if(nomeCamada.Equals("gerarA15_LiqTut")){
			gerarA15_LiqTut();
		}
		else if(nomeCamada.Equals("gerarB_S_LiqTut")){
			gerarB_S_LiqTut();
		}
		else if(nomeCamada.Equals("gerarB_CG_LiqTut")){
			gerarB_CG_LiqTut();
		}
		else if(nomeCamada.Equals("gerarB_M_GasTut")){
			gerarB_M_GasTut();
		}
		else if(nomeCamada.Equals("gerarA15_GasTut")){
			gerarA15_GasTut();
		}
		else if(nomeCamada.Equals("gerarA511_GasTut")){
			gerarA511_GasTut();
		}
		else if(nomeCamada.Equals("gerarB_S_GasTut")){
			gerarB_S_GasTut();
		}
		else if(nomeCamada.Equals("gerarB_Cp_M_P_GasTut")){
			gerarB_Cp_M_P_GasTut();
		}
		else if(nomeCamada.Equals("gerarB_C_PlasmaTut")){
			gerarB_C_PlasmaTut();
		}
		else if(nomeCamada.Equals("gerarB_I_PlasmaTut")){
			gerarB_I_PlasmaTut();
		}
		else if(nomeCamada.Equals("gerarB_Cg_PlasmaTut")){
			gerarB_Cg_PlasmaTut();
		}
	}

	void createA(double x, double y, int size, double centroX, double centroY, double d1Dentro, double d2Dentro, double d1Fora, double d2Fora, string nomeZona){
		Vector3 position = new Vector3((float)x,(float)y,0);
		Quaternion rotation = Quaternion.Euler(0, 0, 0);
		GameObject newA = Instantiate(particulaA, position, rotation, gameObject.transform);
		newA.transform.localScale = new Vector3((float)((size*0.1)+0.3), (float)((size*0.1)+0.3), 0.1f);
		newA.transform.localPosition = position;
		newA.SetActive(true);

		newA.GetComponent<particulasA>().nivelTamanho = size;	
		newA.GetComponent<particulasA>().velocidade = (float)(1.0/(float)size);
		newA.GetComponent<particulasA>().centroX = centroX;
		newA.GetComponent<particulasA>().centroY = centroY;
		newA.GetComponent<particulasA>().d1Dentro = d1Dentro;
		newA.GetComponent<particulasA>().d2Dentro = d2Dentro;
		newA.GetComponent<particulasA>().d1Fora = d1Fora;
		newA.GetComponent<particulasA>().d2Fora = d2Fora;
		newA.GetComponent<particulasA>().anguloDeMovimento = UnityEngine.Random.Range(.0f, 360.0f); 
		newA.GetComponent<particulasA>().controladorCamada = this.gameObject;
		newA.GetComponent<particulasA>().nomeZona = nomeZona;
	}

	void createB(double x, double y, int size, int type, double centroX, double centroY, double d1Dentro, double d2Dentro, double d1Fora, double d2Fora, string nomeZona){
		Vector3 position = new Vector3((float)x,(float)y,0);
		Quaternion rotation = Quaternion.Euler(0, 0, 0);
		GameObject newB = Instantiate(particulaB, position, rotation, gameObject.transform);
		newB.transform.localPosition = position;
		newB.transform.localScale = new Vector3((float)((size*0.1)+0.3), (float)((size*0.1)+0.3), 0.1f);
		newB.SetActive(true);

        // camada liquida tuturial
        if (nomeZona == "gerarB_S_LiqTut")
        {
            newB.GetComponent<particulasB>().particulasSA.sonar = true;
            newB.GetComponent<particulasB>().particulasSA.invisibildade = false;
            newB.GetComponent<particulasB>().particulasSA.magnetico = false;
            newB.GetComponent<particulasB>().particulasSA.camaraLenta = false;

            newB.GetComponent<particulasB>().arrayFilhosB();
        }

        else if (nomeZona == "gerarB_CG_LiqTut")
        {
            newB.GetComponent<particulasB>().particulasSA.sonar = false;
            newB.GetComponent<particulasB>().particulasSA.invisibildade = false;
            newB.GetComponent<particulasB>().particulasSA.magnetico = false;
            newB.GetComponent<particulasB>().particulasSA.camaraLenta = false;

            newB.GetComponent<particulasB>().chaves.gasoso = true;
        }

        //camada gasosa tuturial

        else if (nomeZona == "gerarB_M_GasTut")
        {
            newB.GetComponent<particulasB>().particulasSA.sonar = false;
            newB.GetComponent<particulasB>().particulasSA.invisibildade = false;
            newB.GetComponent<particulasB>().particulasSA.magnetico = true;
            newB.GetComponent<particulasB>().particulasSA.camaraLenta = false;
        }


        else if (nomeZona == "gerarB_S_GasTut")
        {
            newB.GetComponent<particulasB>().particulasSA.sonar = true;
            newB.GetComponent<particulasB>().particulasSA.invisibildade = false;
            newB.GetComponent<particulasB>().particulasSA.magnetico = false;
            newB.GetComponent<particulasB>().particulasSA.camaraLenta = false;
        }

        else if (nomeZona == "gerarB_Cp_M_P_GasTut")
        {
            newB.GetComponent<particulasB>().particulasSA.sonar = true;
            newB.GetComponent<particulasB>().particulasSA.invisibildade = false;
            newB.GetComponent<particulasB>().particulasSA.magnetico = true;
            newB.GetComponent<particulasB>().particulasSA.camaraLenta = false;

            newB.GetComponent<particulasB>().chaves.plasma = true;
        }

        //camada plasma tuturial

        else if (nomeZona == "gerarB_C_PlasmaTut")
        {
            newB.GetComponent<particulasB>().particulasSA.sonar = false;
            newB.GetComponent<particulasB>().particulasSA.invisibildade = false;
            newB.GetComponent<particulasB>().particulasSA.magnetico = false;
            newB.GetComponent<particulasB>().particulasSA.camaraLenta = true;
        }

        else if (nomeZona == "gerarB_I_PlasmaTut")
        {
            newB.GetComponent<particulasB>().particulasSA.sonar = false;
            newB.GetComponent<particulasB>().particulasSA.invisibildade = true;
            newB.GetComponent<particulasB>().particulasSA.magnetico = false;
            newB.GetComponent<particulasB>().particulasSA.camaraLenta = false;
        }

        else if (nomeZona == "gerarB_Cg_PlasmaTut")
        {
            newB.GetComponent<particulasB>().particulasSA.sonar = false;
            newB.GetComponent<particulasB>().particulasSA.invisibildade = false;
            newB.GetComponent<particulasB>().particulasSA.magnetico = false;
            newB.GetComponent<particulasB>().particulasSA.camaraLenta = false;

            newB.GetComponent<particulasB>().chaves.gasoso = true;
        }

        //fim de por chaves e ppowerups


        newB.GetComponent<particulasB>().nivelTamanho = size;

		newB.GetComponent<particulasB>().velocidade = (float)(1.0/(float)size);

		newB.GetComponent<particulasB>().centroX = centroX;
		newB.GetComponent<particulasB>().centroY = centroY;
		newB.GetComponent<particulasB>().d1Dentro = d1Dentro;
		newB.GetComponent<particulasB>().d2Dentro = d2Dentro;
		newB.GetComponent<particulasB>().d1Fora = d1Fora;
		newB.GetComponent<particulasB>().d2Fora = d2Fora;
		newB.GetComponent<particulasB>().nomeZona = nomeZona;
		newB.GetComponent<particulasB>().controladorCamada = this.gameObject;
	}

	//________________________________________________________________________________________________
	//________________________________________________________________________________________________
	//________________________________________________________________________________________________
	//________________________________________________________________________________________________
	//LIQUIDO TUTORIAL________________________________________________________________________________

	/*
	W- 538px --- 269units
	H- 410px --- 205units
	Centro 269,205px

	Start
		Centro - 386,276px --> 58.5,-35.5
		D1-28
		D2-37

	A-1-5
		Centro - 386,276px --> 58.5,-35.5
		D1Dentro-28
		D2Dentro-37
		D1Fora-48
		D2Fora-64.5

	B-S
		Centro - 386,276px --> 58.5,-35.5
		D1Dentro-48
		D2Dentro-64.5
		D1Fora-66
		D2Fora-81.5
	B-Cg
		Centro - 78,263px --> -95.5,-29
		D1- 32
		D2- 40

	*/
	void gerarStart_LiqTut(){
		int raioH = UnityEngine.Random.Range(1, 38);
		//raio verticar proporcinal com o obtido em raioH
		int raioV = (raioH*28)/37;
		//Y=(1-(X^2/raioH^2))*raioV^2
		double x = UnityEngine.Random.Range(0, raioH);
		double y = Math.Sqrt((1- (Math.Pow(x, 2)/Math.Pow(raioH, 2)))*Math.Pow(raioV, 2));
		int posiNeg = UnityEngine.Random.Range(0, 2);

		if(posiNeg==1){
			x = -x;
		}
		posiNeg = UnityEngine.Random.Range(0, 2);
    	if(posiNeg==1){
			y = -y;
		}

		//translate Centro
		x = x+58.5;
		y = y-35.5;

		//criar   
		createA(x, y, 1, 58.5,-35.5, 0,0,28,37,"gerarStart_LiqTut");
		createA(65.2, -38.6, 1, 58.5,-35.5, 0,0,28,37,"gerarStart_LiqTut");


		raioH = UnityEngine.Random.Range(1, 38);
		//raio verticar proporcinal com o obtido em raioH
		raioV = (raioH*28)/37;
		//Y=(1-(X^2/raioH^2))*raioV^2
		x = UnityEngine.Random.Range(0, raioH);
		y = Math.Sqrt((1- (Math.Pow(x, 2)/Math.Pow(raioH, 2)))*Math.Pow(raioV, 2));
		posiNeg = UnityEngine.Random.Range(0, 2);
		if(posiNeg==1){
			x = -x;
		}
		posiNeg = UnityEngine.Random.Range(0, 2);
    	if(posiNeg==1){
			y = -y;
		}

		//translate Centro
		x = x+58.5;
		y = y-35.5;

		createA(x, y, 5, 58.5,-35.5, 0,0,28,37,"gerarStart_LiqTut");
		createA(52.2, -42.3, 5, 58.5,-35.5, 0,0,28,37,"gerarStart_LiqTut");
	}


	void gerarA15_LiqTut(){
		int raioH = UnityEngine.Random.Range(37, 65);
		//raio verticar proporcinal com o obtido em raioH
		int raioV = (raioH*48)/64;
		//Y=(1-(X^2/raioH^2))*raioV^2
		double x = UnityEngine.Random.Range(0, raioH);
		double y = Math.Sqrt((1- (Math.Pow(x, 2)/Math.Pow(raioH, 2)))*Math.Pow(raioV, 2));
		int posiNeg = UnityEngine.Random.Range(0, 2);
		if(posiNeg==1){
			x = -x;
		}
		posiNeg = UnityEngine.Random.Range(0, 2);
    	if(posiNeg==1){
			y = -y;
		}
		//translate Centro
		x = x+58.5;
		y = y-35.5;

		createA(x, y, UnityEngine.Random.Range(1, 5), 58.5,-35.5, 28,37,48,64,"gerarA15_LiqTut");
	}

	void gerarB_S_LiqTut(){
		int raioH = UnityEngine.Random.Range(64, 82);
		//raio verticar proporcinal com o obtido em raioH
		int raioV = (raioH*66)/81;
		//Y=(1-(X^2/raioH^2))*raioV^2
		double x = UnityEngine.Random.Range(0, raioH);
		double y = Math.Sqrt((1- (Math.Pow(x, 2)/Math.Pow(raioH, 2)))*Math.Pow(raioV, 2));
		int posiNeg = UnityEngine.Random.Range(0, 2);
		if(posiNeg==1){
			x = -x;
		}
		posiNeg = UnityEngine.Random.Range(0, 2);
    	if(posiNeg==1){
			y = -y;
		}
		//translate Centro
		x = x+58.5;
		y = y-35.5;
		createB(x, y, UnityEngine.Random.Range(1, 5),1, 58.5,-35.5, 48,64,66,81,"gerarB_S_LiqTut");
	}


	void gerarB_CG_LiqTut(){
		int raioH = UnityEngine.Random.Range(1, 41);
		//raio verticar proporcinal com o obtido em raioH
		int raioV = (raioH*32)/40;
		//Y=(1-(X^2/raioH^2))*raioV^2
		double x = UnityEngine.Random.Range(0, raioH);
		double y = Math.Sqrt((1- (Math.Pow(x, 2)/Math.Pow(raioH, 2)))*Math.Pow(raioV, 2));
		int posiNeg = UnityEngine.Random.Range(0, 2);
		if(posiNeg==1){
			x = -x;
		}
		posiNeg = UnityEngine.Random.Range(0, 2);
    	if(posiNeg==1){
			y = -y;
		}
		//translate Centro
		x = x-95.5;
		y = y-29;
		createB(x, y, UnityEngine.Random.Range(1, 5),1, -95.5,-29, 0,0,32,40,"gerarB_CG_LiqTut");
	}
	//________________________________________________________________________________________________
	//________________________________________________________________________________________________
	//________________________________________________________________________________________________
	//________________________________________________________________________________________________
	//GasosoTut TUTORIAL________________________________________________________________________________

	/*
	W- 538px --- 269units
	H- 410px --- 205units
	Centro 269,205px

	Start
		Centro - 143,150px --> -63,27.5
		D1-12.5
		D2-14

	A-1-5
		Centro - 143,150px --> -63,27.5
		D1Dentro-12.5
		D2Dentro-14
		D1Fora-17
		D2Fora-22.5

	A-5-11
		Centro - 143,150px --> -63,27.5
		D1Dentro-17
		D2Dentro-22.5
		D1Fora-34.5
		D2Fora-46.5

	B-S
		Centro - 143,150px --> -63,27.5
		D1Dentro-34.5
		D2Dentro-46.5
		D1Fora-47.5
		D2Fora-58.5
	B-Cp-M-P
		Centro - 453,126px --> 92,39.5
		D1- 40.5
		D2- 63

	*/

	void gerarB_M_GasTut(){
		int raioH = UnityEngine.Random.Range(1, 15);
		int raioV = (raioH*12)/14;
		//Y=(1-(X^2/raioH^2))*raioV^2
		double x = UnityEngine.Random.Range(0, raioH);
		double y = Math.Sqrt((1- (Math.Pow(x, 2)/Math.Pow(raioH, 2)))*Math.Pow(raioV, 2));
		int posiNeg = UnityEngine.Random.Range(0, 2);

		if(posiNeg==1){
			x = -x;
		}
		posiNeg = UnityEngine.Random.Range(0, 2);
    	if(posiNeg==1){
			y = -y;
		}

		//translate Centro
		x = x-63;
		y = y+27.5;

		//criar
		createB(x, y, UnityEngine.Random.Range(1, 5), 1, -63, 27.5, 0,0, 12, 14,"gerarB_M_GasTut");
	}
	
	void gerarA15_GasTut(){
		int raioH = UnityEngine.Random.Range(15, 24);
		int raioV = (raioH*17)/23;
		//Y=(1-(X^2/raioH^2))*raioV^2
		double x = UnityEngine.Random.Range(0, raioH);
		double y = Math.Sqrt((1- (Math.Pow(x, 2)/Math.Pow(raioH, 2)))*Math.Pow(raioV, 2));
		int posiNeg = UnityEngine.Random.Range(0, 2);

		if(posiNeg==1){
			x = -x;
		}
		posiNeg = UnityEngine.Random.Range(0, 2);
    	if(posiNeg==1){
			y = -y;
		}

		//translate Centro
		x = x-63;
		y = y+27.5;

		//criar
		createA(x, y, UnityEngine.Random.Range(1, 5), -63, 27.5, 12,14, 17, 23, "gerarA15_GasTut");
	}
	void gerarA511_GasTut(){
		int raioH = UnityEngine.Random.Range(24, 47);
		int raioV = (raioH*34)/46;
		//Y=(1-(X^2/raioH^2))*raioV^2
		double x = UnityEngine.Random.Range(0, raioH);
		double y = Math.Sqrt((1- (Math.Pow(x, 2)/Math.Pow(raioH, 2)))*Math.Pow(raioV, 2));
		int posiNeg = UnityEngine.Random.Range(0, 2);

		if(posiNeg==1){
			x = -x;
		}
		posiNeg = UnityEngine.Random.Range(0, 2);
    	if(posiNeg==1){
			y = -y;
		}

		//translate Centro
		x = x-63;
		y = y+27.5;

		//criar
		createA(x, y, UnityEngine.Random.Range(5, 11), -63, 27.5, 17,23, 34, 46,"gerarA511_GasTut");
	}
	void gerarB_S_GasTut(){
		int raioH = UnityEngine.Random.Range(47, 60);
		int raioV = (raioH*47)/59;
		//Y=(1-(X^2/raioH^2))*raioV^2
		double x = UnityEngine.Random.Range(0, raioH);
		double y = Math.Sqrt((1- (Math.Pow(x, 2)/Math.Pow(raioH, 2)))*Math.Pow(raioV, 2));
		int posiNeg = UnityEngine.Random.Range(0, 2);

		if(posiNeg==1){
			x = -x;
		}
		posiNeg = UnityEngine.Random.Range(0, 2);
    	if(posiNeg==1){
			y = -y;
		}

		//translate Centro
		x = x-63;
		y = y+27.5;

		//criar
		createB(x, y, UnityEngine.Random.Range(1, 5), 1, -63, 27.5, 34,46, 47, 59, "gerarB_S_GasTut");
	}
	void gerarB_Cp_M_P_GasTut(){
		int raioH = UnityEngine.Random.Range(1, 62);
		int raioV = (raioH*38)/61;
		//Y=(1-(X^2/raioH^2))*raioV^2
		double x = UnityEngine.Random.Range(0, raioH);
		double y = Math.Sqrt((1- (Math.Pow(x, 2)/Math.Pow(raioH, 2)))*Math.Pow(raioV, 2));
		int posiNeg = UnityEngine.Random.Range(0, 2);

		if(posiNeg==1){
			x = -x;
		}
		posiNeg = UnityEngine.Random.Range(0, 2);
    	if(posiNeg==1){
			y = -y;
		}

		//translate Centro
		x = x+92;
		y = y+39.5;
		//criar
		createB(x, y, UnityEngine.Random.Range(1, 5), 1, 92, 39.5, 0,0, 41, 63,"gerarB_Cp_M_P_GasTut");
	}

	//________________________________________________________________________________________________
	//________________________________________________________________________________________________
	//________________________________________________________________________________________________
	//________________________________________________________________________________________________
	//PlasmaTut TUTORIAL________________________________________________________________________________

	/*
	W- 538px --- 269units
	H- 410px --- 205units
	Centro 269,205px

	Start-BC
		Centro - 405,305px --> 68,-50
		D1-14
		D2-18.5

	B-I
		Centro - 405,305px --> 68,-50
		D1Dentro-14
		D2Dentro-18.5
		D1Fora-32.5
		D2Fora-38.5

	B-Cg
		Centro - 405,305px --> 68,-50
		D1Dentro-32.5
		D2Dentro-38.5
		D1Fora-48.5
		D2Fora-58.5
	

	*/


	void gerarB_C_PlasmaTut(){
		int raioH = UnityEngine.Random.Range(1, 19);
		int raioV = (raioH*14)/18;
		//Y=(1-(X^2/raioH^2))*raioV^2
		double x = UnityEngine.Random.Range(0, raioH);
		double y = Math.Sqrt((1- (Math.Pow(x, 2)/Math.Pow(raioH, 2)))*Math.Pow(raioV, 2));
		int posiNeg = UnityEngine.Random.Range(0, 2);

		if(posiNeg==1){
			x = -x;
		}
		posiNeg = UnityEngine.Random.Range(0, 2);
    	if(posiNeg==1){
			y = -y;
		}

		//translate Centro
		x = x+68;
		y = y-50;
		//criar
		createB(x, y, UnityEngine.Random.Range(1, 5), 1, 68, -50, 0, 0, 14, 18, "gerarB_C_PlasmaTut");
	}

	void gerarB_I_PlasmaTut(){
		int raioH = UnityEngine.Random.Range(15, 39);
		int raioV = (raioH*32)/38;
		//Y=(1-(X^2/raioH^2))*raioV^2
		double x = UnityEngine.Random.Range(0, raioH);
		double y = Math.Sqrt((1- (Math.Pow(x, 2)/Math.Pow(raioH, 2)))*Math.Pow(raioV, 2));
		int posiNeg = UnityEngine.Random.Range(0, 2);

		if(posiNeg==1){
			x = -x;
		}
		posiNeg = UnityEngine.Random.Range(0, 2);
    	if(posiNeg==1){
			y = -y;
		}

		//translate Centro
		x = x+68;
		y = y-50;
		//criar
		createB(x, y, UnityEngine.Random.Range(1, 5), 1, 68, -50, 14, 18, 32, 38,"gerarB_I_PlasmaTut");
	}
	void gerarB_Cg_PlasmaTut(){
		int raioH = UnityEngine.Random.Range(33, 59);
		int raioV = (raioH*48)/58;
		//Y=(1-(X^2/raioH^2))*raioV^2
		double x = UnityEngine.Random.Range(0, raioH);
		double y = Math.Sqrt((1- (Math.Pow(x, 2)/Math.Pow(raioH, 2)))*Math.Pow(raioV, 2));
		int posiNeg = UnityEngine.Random.Range(0, 2);

		if(posiNeg==1){
			x = -x;
		}
		posiNeg = UnityEngine.Random.Range(0, 2);
    	if(posiNeg==1){
			y = -y;
		}

		//translate Centro
		x = x+68;
		y = y-50;
		//criar
		createB(x, y, UnityEngine.Random.Range(1, 5), 1, 68, -50, 32, 38, 48, 58, "gerarB_Cg_PlasmaTut");
	}
}