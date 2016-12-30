using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required when Using UI elements.

public class PrimaryControl : MonoBehaviour {
	public GameObject plane;
	public GameObject light;
	public GameObject UI;
	public Slider hertzSlider;
	public Slider timeSlider;
	public Text hertzText;
	public Text timeText;
	public int hertz = 40;
	public int time = 60;

 	float frequency = 0f;
	float timeLeft = 0f;
	bool executing = false;

	// Use this for initialization
	void Start () {
		hertzSlider.onValueChanged.AddListener (delegate {SetHertz ();});
		timeSlider.onValueChanged.AddListener (delegate {SetTime ();});
		hertz = (int) hertzSlider.value;
		hertzText.text = ""+hertz;
		time = (int) timeSlider.value;
		timeText.text = ""+time;
	}

	void CycleLights(){
		plane.SetActive (!plane.activeSelf);
		light.SetActive (!light.activeSelf);
	}
		
	// Update is called once per frame
	void Update () {
		if (executing && (Input.touchCount > 0 || Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))) {
			Stop ();
		}

		if (timeLeft <= 0f && executing) {
			Stop ();
			return;
		} else if (timeLeft > 0 && executing) {
			timeLeft -= Time.deltaTime;
		}
	}

	public void SetHertz(){
		hertz = (int) hertzSlider.value;
		hertzText.text = ""+hertz;
	}

	public void SetTime(){
		time = (int) timeSlider.value;
		timeText.text = ""+time + "  \n\tRemain: " + ((int)timeLeft/60) ;
	}

	public void Begin(){
		timeLeft = time * 60;
		frequency = 1f / hertz;
		Resume ();
	}
		
	public void Resume(){
		executing = true;
		HideUI ();
		InvokeRepeating("CycleLights", 0, frequency);
	}

	public void Stop(){
		executing = false;
		DisplayUI();
		CancelInvoke("CycleLights");
		plane.SetActive (false);
		light.SetActive (false);
	}

	void HideUI(){
		UI.SetActive (false);
	}

	void DisplayUI(){
		SetTime ();
		UI.SetActive (true);
	}
}
