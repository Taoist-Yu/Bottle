using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

	public GameObject MainUI;
	public GameObject StartUI;
	public GameObject OptionUI;

	public static AudioSource AS;
	public Slider sd;
	public Toggle tg;

	private void Awake()
	{
		AS = GetComponent<AudioSource>();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (tg.isOn == true)
		{
			AS.volume = sd.value;
		}
		else
		{
			AS.volume = 0;
		}
		
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
