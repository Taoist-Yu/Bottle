using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderNet : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag != "BluePlayer" && other.tag != "RedPlayer")
			return;
		other.SendMessage("OnNetEnter");
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag != "BluePlayer" && other.tag != "RedPlayer")
			return;
		other.SendMessage("OnNetExit");
	}

}
