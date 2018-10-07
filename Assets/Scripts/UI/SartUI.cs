using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SartUI : MonoBehaviour {

	bool isDouble;

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

	public void OnDouble()
	{
		isDouble = true;
		SetGameMode();
		SceneManager.LoadScene(1);
	}

	public void OnSingle()
	{
		isDouble = false;
		SetGameMode();
		SceneManager.LoadScene(1);
	}

	void SetGameMode()
	{
		GameMode.isDouble = isDouble;
		GameMode.volume = UIController.AS.volume;
	}

}
