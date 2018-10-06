using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = System.Random;

public enum BulletType
{
	small = 0,
	bounce = 2,
	fission = 3,
	Thunder = 1,
	Number = 4
}

public class PlayerManager : MonoBehaviour {

	public GameObject player1;
	public GameObject player2;

	private void Awake()
	{

	}

	private void OnEnable()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetPlayer()
	{
		//生成骚瓶
		if (GameMode.isDouble == false)
		{
			player1 = (GameObject)Resources.Load("Prefabs/Player/BluePlayer");
			player2 = (GameObject)Resources.Load("Prefabs/Player/RedPlayerAI");
		}
		else
		{
			player1 = (GameObject)Resources.Load("Prefabs/Player/BluePlayer");
			player2 = (GameObject)Resources.Load("Prefabs/Player/RedPlayer");
		}

		//实例化玩家，使用默认的tramsform
		Instantiate(player1);
		Instantiate(player2);

		//随机旋转玩家
		player1.transform.rotation = Quaternion.Euler(new Vector3(0, 0, new Random().Next() % 360));
		player2.transform.rotation = Quaternion.Euler(new Vector3(0, 0, new Random().Next() % 360));
	}

	void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		//生成骚瓶
		if (GameMode.isDouble == false)
		{
			player1 = (GameObject)Resources.Load("Prefabs/Player/BluePlayer");
			player2 = (GameObject)Resources.Load("Prefabs/Player/RedPlayerAI");
		}
		else
		{
			player1 = (GameObject)Resources.Load("Prefabs/Player/BluePlayer");
			player2 = (GameObject)Resources.Load("Prefabs/Player/RedPlayer");
		}

		//实例化玩家，使用默认的tramsform
		Instantiate(player1);
		Instantiate(player2);

		//随机旋转玩家
		player1.transform.rotation = Quaternion.Euler(new Vector3(0, 0, new Random().Next() % 360));
		player2.transform.rotation = Quaternion.Euler(new Vector3(0, 0, new Random().Next() % 360));
	}

}
