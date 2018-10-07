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
	private AudioSource AS;

	//定时器相关
	private static float timeVal = 0.6f;
	private static bool timeLimit = false;

	//由于地图收缩引起的游戏结束事件
	public delegate void TimeLimieEventHandler(int delta);
	public static TimeLimieEventHandler TimeLimitEvent;

	private void Awake()
	{
		AS = GetComponent<AudioSource>();
	}

	private void OnEnable()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	// Use this for initialization
	void Start () {
		AS.volume = GameMode.volume;
	}
	
	// Update is called once per frame
	void Update () {
		if (timeLimit == true) {
			timeVal -= Time.deltaTime;
			if (timeVal <= 0) {
				timeVal = 0.5f;
				TimeLimitEvent(-1);
			}
		}
	}

	//播放音效方法
	private void AudioPlay(AudioClip clip)
	{
		AS.clip = clip;
		AS.Play();
	}

	//当场景加载时生成玩家 
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
		Instantiate(player1,transform);
		Instantiate(player2,transform);

		//随机旋转玩家
		player1.transform.rotation = Quaternion.Euler(new Vector3(0, 0, new Random().Next() % 360));
		player2.transform.rotation = Quaternion.Euler(new Vector3(0, 0, new Random().Next() % 360));
	}

	//地图收缩到不能再收缩时调用
	public static void TimeLimit()
	{
		timeLimit = true;
	}

}
