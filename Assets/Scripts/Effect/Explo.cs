using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explo : MonoBehaviour {

	private float timeVal = 0.3f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		timeVal -= Time.deltaTime;
		if (timeVal < 0)
			Destroy(gameObject);
	}
}
