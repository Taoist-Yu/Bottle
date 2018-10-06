using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SartUI : MonoBehaviour {

	bool isDouble;

	// Use this for initialization
	void Start () {
		Debug.Log("The sumber of scenes is " + SceneManager.sceneCountInBuildSettings);
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
		SceneManager.LoadScene(0);
	}

	public void OnSingle()
	{
		isDouble = false;
		SetGameMode();
		SceneManager.LoadScene(0);
	}

	void SetGameMode()
	{
		GameMode.isDouble = isDouble;
	}

}
