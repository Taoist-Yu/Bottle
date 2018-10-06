using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnReturn()
	{
		transform.parent.SendMessage("SetMainUI");
		gameObject.SetActive(false);
	}

}
