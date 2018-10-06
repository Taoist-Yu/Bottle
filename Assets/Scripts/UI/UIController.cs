using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {

	public GameObject MainUI;
	public GameObject StartUI;
	public GameObject OptionUI;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void SetStartUI()
	{
		StartUI.SetActive(true);
	}

	private void SetOptionUI()
	{
		OptionUI.SetActive(true);
	}

	private void SetMainUI()
	{
		MainUI.SetActive(true);
	}

}
