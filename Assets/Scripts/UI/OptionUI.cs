using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionUI : MonoBehaviour {

	public float volume;

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
