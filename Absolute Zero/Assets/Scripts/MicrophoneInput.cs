using UnityEngine;
using UnityEngine.UI; //for accessing Sliders and Dropdown
using System.Collections.Generic; // So we can use List<>

[RequireComponent(typeof(AudioSource))]
public class MicrophoneInput : MonoBehaviour {
	public float minThreshold = 0;
	public int defaultFrequency = 120; //i:80 a:185
	public float sensibilityFrequency = 30.0f;
	public float marginFrequency = 40.0f;
	public float minVolume = 0.3f;
	public int audioSampleRate = 8192;//44100;
	public string microphone;
	private int samples = 8192; 
	private float updateInterval = 4.0f;
	private float lastInterval; // Last interval end time
	private int cycles = 0; // Cycles numver over current interval
	public float fps; // Frames per second
	public float ffps; // Current fundamental frequency per second
	private List<float> listFrequency = new List<float>();
	private List<float> listTimes = new List<float>();

	private AudioSource audioSource;
	public Slider handlerFreq;
	public FFTWindow fftWindow;
	

	void Start() {
		//get components you'll need
		audioSource = GetComponent<AudioSource> ();

		// get all available microphones
		foreach (string device in Microphone.devices) {
			if (microphone == null) {
				//set default mic to first mic found.
				microphone = device;
				Debug.Log("Microphone: "+device);
			}
		}

		//initialize input with default mic
		UpdateMicrophone ();
	}

	void Update(){
        //float volume = GetAveragedVolume();
		float averagedFrequency = GetAveragedFrequency();
	//->	Debug.Log("averagedFrequency: " + averagedFrequency);
        handlerFreq.value = averagedFrequency;
        
		ColorBlock cb = handlerFreq.colors;
        if(averagedFrequency > defaultFrequency + sensibilityFrequency){
            //Debug.Log("CIMA--GetAveragedFrequency" + averagedFrequency);
			cb.normalColor = Color.red;
			handlerFreq.colors = cb;
        }
        else if(averagedFrequency < defaultFrequency - sensibilityFrequency){
            //Debug.Log("BAIXO-GetAveragedFrequency" + averagedFrequency);
			cb.normalColor = Color.red;
			handlerFreq.colors = cb;
        }
        else{
            //Debug.Log ("-----GetAveragedFrequency" + averagedFrequency);
			cb.normalColor = Color.white;
			handlerFreq.colors = cb;
        }
		//GetFps();
	}
	void FixedUpdate(){
		
		float currentFreq = GetFundamentalFrequency();
		var timeNow = Time.realtimeSinceStartup;

		
		float upperLimit = defaultFrequency + sensibilityFrequency + marginFrequency;
		float lowerLimit = defaultFrequency - sensibilityFrequency - marginFrequency;
		// filter all values before do temporal analisys
		// if(currentFreq > lowerLimit || currentFreq < upperLimit){
		// 	currentFreq = defaultFrequency;
		// }
		Debug.Log("currentFreq" + currentFreq);
		listFrequency.Add(currentFreq);
		listTimes.Add(timeNow);

	}


	void GetFps(){
		var timeNow = Time.realtimeSinceStartup;
		++cycles;
		if( timeNow > lastInterval + updateInterval )
		{
			fps = cycles / (timeNow - lastInterval);
			//Debug.Log ("fps" + fps);
			cycles = 0;
			lastInterval = timeNow;
		}	
	}

	void UpdateMicrophone(){
		audioSource.Stop(); 
		//Start recording to audioclip from the mic
		audioSource.clip = Microphone.Start(microphone, true, 10, audioSampleRate);
		audioSource.loop = true; 
		// Mute the sound with an Audio Mixer group becuase we don't want the player to hear it
		Debug.Log(Microphone.IsRecording(microphone).ToString());

		if (Microphone.IsRecording (microphone)) { //check that the mic is recording, otherwise you'll get stuck in an infinite loop waiting for it to start
			while (!(Microphone.GetPosition (microphone) > 0)) {
			} // Wait until the recording has started. 
		
			Debug.Log ("recording started with " + microphone);

			// Start playing the audio source
			audioSource.Play (); 
		} else {
			//microphone doesn't work for some reason

			Debug.Log (microphone + " doesn't work!");
		}
	}
	
	public float GetAveragedVolume()
	{ 

		//verificar o valor do volume a partir do qual o user está a falar. Nao calcular a freq com o ruido de fundo
		float[] data = new float[256];
		float a = 0;
		audioSource.GetOutputData(data,0);
		foreach(float s in data)
		{
			a += Mathf.Abs(s);
		}
		return a;
		
	}
	public float GetAveragedFrequency()
	{
		//float averagedVolume = GetAveragedVolume();
		//float currentFreq = GetFundamentalFrequency();
		float averageFreq = 0.0f;
		var timeNow = Time.realtimeSinceStartup;
		var elements = 0;
		var sumFreq = 0.0f;

		// Debug.Log("currentFreq" + currentFreq);
		// float upperLimit = defaultFrequency + sensibilityFrequency + marginFrequency;
		// float lowerLimit = defaultFrequency - sensibilityFrequency - marginFrequency;
		// //filter all values before do temporal analisys
		// if(currentFreq > lowerLimit || currentFreq < upperLimit){
		// 	currentFreq = defaultFrequency;
		// }

		elements = 0;
		sumFreq = 0;
		//if there are elements in list
		if (listFrequency.Count > 10){
			//remove old elements
			for (int i = 0; i < listFrequency.Count - 1; i++)
			{
				//remove element if is older than time interval
				if (listTimes[i] + updateInterval < timeNow ){
					listTimes.RemoveAt(i);
					listFrequency.RemoveAt(i);
				}
				else{ // inside time interval, sum this elements
					++elements;
					sumFreq += listFrequency[i];
				}
			}
		}
		//add last element
		//++elements;
		//sumFreq += currentFreq;

		// listFrequency.Add(currentFreq);
		// listTimes.Add(timeNow);
		
		//do average calc of current last second lsit
		averageFreq = sumFreq / elements;

		Debug.Log("listFrequency.Count: " + listFrequency.Count + " elements" + elements + "sumFreq" + sumFreq + "average" + averageFreq);
		return averageFreq;

	}
	public float GetFundamentalFrequency()
	{
		int value = 0;
		//isto aqui ainda pode ser melhorado
		//talvez calcular a média consoante os 3 valores mais altos
		//e ainda calcular a média durante o segundo, nos ultimos X's update()

//		Debug.Log("GetAveragedVolume:" + GetAveragedVolume());

		 if(GetAveragedVolume() > minVolume){
			float fundamentalFrequency = 0.0f;
			float[] data = new float[samples];
			audioSource.GetSpectrumData(data,0,FFTWindow.BlackmanHarris);//fftWindow);
			float s = 0.0f;
			int i = 0;
			//float a = 0;
			
			for (int j = 1; j < samples; j++)
			{
				if ( s < data[j] )
				{
					s = data[j];
					i = j;
				}
			}
			value = i;
			fundamentalFrequency = data[i] * audioSampleRate / samples;
			//Debug.Log("fundamentalFrequency:" + fundamentalFrequency + " i:" + i + " data:" + data[i]);
		}
		else{
			//	Debug.Log("fundamentalFrequency" + defaultFrequency);
			value = defaultFrequency;
		}
	return value;
	}
}