using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnStart()
	{
		transform.parent.SendMessage("SetStartUI");
		gameObject.SetActive(false);
	}

	public void OnSet()
	{
		transform.parent.SendMessage("SetOptionUI");
		gameObject.SetActive(false);
	}

	public void OnExit()
	{
		Application.Quit();
	}

}
