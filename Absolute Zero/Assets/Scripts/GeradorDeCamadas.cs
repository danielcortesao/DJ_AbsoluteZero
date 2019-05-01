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

	void createA(double x, double y, int size, double centroX, double centroY, double d1Dentro, double d2Dentro, double d1Fora, double d2Fora){
		Vector3 position = new Vector3((float)x,(float)y,0);
		Quaternion rotation = Quaternion.Euler(0, 0, 0);
		GameObject newA = Instantiate(particulaA, position, rotation, gameObject.transform);
		newA.transform.localScale = new Vector3((float)size, (float)size, 0.1f);
		newA.SetActive(true);
		newA.GetComponent<particulasA>().nivelTamanho = size;	
		newA.GetComponent<particulasA>().velocidade = 1/size;
		newA.GetComponent<particulasA>().centroX = centroX;
		newA.GetComponent<particulasA>().centroY = centroY;
		newA.GetComponent<particulasA>().d1Dentro = d1Dentro;
		newA.GetComponent<particulasA>().d2Dentro = d2Dentro;
		newA.GetComponent<particulasA>().d1Fora = d1Fora;
		newA.GetComponent<particulasA>().d2Fora = d2Fora;
		newA.GetComponent<particulasA>().anguloDeMovimento = UnityEngine.Random.Range(.0f, 360.0f);

	}

	void createB(double x, double y, int size, int type, double centroX, double centroY, double d1Dentro, double d2Dentro, double d1Fora, double d2Fora){
		Vector3 position = new Vector3((float)x,(float)y,0);
		Quaternion rotation = Quaternion.Euler(0, 0, 0);
		GameObject newB = Instantiate(particulaB, position, rotation, gameObject.transform);
		newB.transform.localScale = new Vector3((float)size, (float)size, 0.1f);
		newB.SetActive(true);
		newB.GetComponent<particulasB>().nivelTamanho = size;
		newB.GetComponent<particulasB>().velocidade = 10/size;

		newB.GetComponent<particulasB>().centroX = centroX;
		newB.GetComponent<particulasB>().centroY = centroY;
		newB.GetComponent<particulasB>().d1Dentro = d1Dentro;
		newB.GetComponent<particulasB>().d2Dentro = d2Dentro;
		newB.GetComponent<particulasB>().d1Fora = d1Fora;
		newB.GetComponent<particulasB>().d2Fora = d2Fora;
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
		createA(x, y, 1, 58.5,-35.5, 0,0,28,37);

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

		createA(x, y, 5, 58.5,-35.5, 0,0,28,37);

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

		createA(x, y, UnityEngine.Random.Range(1, 5), 58.5,-35.5, 28,37,48,64);
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
		createB(x, y, UnityEngine.Random.Range(1, 5),1, 58.5,-35.5, 48,64,66,81);
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
		createB(x, y, UnityEngine.Random.Range(1, 5),1, -95.5,-29, 0,0,32,40);
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
		createB(x, y, UnityEngine.Random.Range(1, 5), 1, -63, 27.5, 0,0, 12, 14);
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
		createA(x, y, UnityEngine.Random.Range(1, 5), -63, 27.5, 12,14, 17, 23);
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
		createA(x, y, UnityEngine.Random.Range(5, 11), -63, 27.5, 17,23, 34, 46);
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
		createB(x, y, UnityEngine.Random.Range(1, 5), 1, -63, 27.5, 34,46, 47, 59);
	}
	void gerarB_Cp_M_P_GasTut(){
		int raioH = UnityEngine.Random.Range(1, 64);
		int raioV = (raioH*63)/41;
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
		createB(x, y, UnityEngine.Random.Range(1, 5), 1, 92, 39.5, 0,0, 41, 63);
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
		createB(x, y, UnityEngine.Random.Range(1, 5), 1, 68, -50, 0, 0, 14, 18);
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
		createB(x, y, UnityEngine.Random.Range(1, 5), 1, 68, -50, 14, 18, 32, 38);
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
		createB(x, y, UnityEngine.Random.Range(1, 5), 1, 68, -50, 32, 38, 48, 58);
	}
}