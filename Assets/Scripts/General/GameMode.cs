using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode : MonoBehaviour {

	public static bool isDouble = true;
	public static float volume = 1.0f;

	public GameObject gameOver;
	public GameObject gamePause;

	private float timeVal = 1.0f;
	private static bool isOver = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (isOver == true)
		{
			timeVal -= Time.deltaTime;
			if (timeVal <= 0)
			{
				GameObject.Find("GameController").SendMessage("Over");
			}
		}
		else
		{
			if (Input.GetKeyDown(KeyCode.Space)) {
				Pause();
			}
		}


	}

	private void Over()
	{
		gameOver.SetActive(true);
		Time.timeScale = 0;
	}

	private void Pause()
	{
		if (gamePause.activeSelf == true)
		{
			gamePause.SetActive(false);
			Time.timeScale = 1.0f;
		}
		else
		{
			gamePause.SetActive(true);
			Time.timeScale = 0;
		}
	}

	//玩家死亡时调用
	public static void GameOver()
	{
		isOver = true;
	}

}
