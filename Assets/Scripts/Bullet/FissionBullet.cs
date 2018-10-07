using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FissionBullet : MonoBehaviour {

	public Sprite sprite;
	private GameObject smallPrefab;

	private SpriteRenderer sr;

	private const float moveSpeed = 15.0f;

	private void Awake()
	{
		sr = GetComponent<SpriteRenderer>();
	}

	// Use this for initialization
	void Start () {
		//设置图片精灵
		sr.sprite = sprite;
		//获取小子弹
		switch (tag)
		{
			case "BlueBullet":
				smallPrefab = (GameObject)Resources.Load("Prefabs/Bullet/Blue/SmallBullet");
				break;
			case "RedBullet":
				smallPrefab = (GameObject)Resources.Load("Prefabs/Bullet/Red/SmallBullet");
				break;
		}
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(Quaternion.AngleAxis(20,Vector3.forward) * Vector3.left * moveSpeed * Time.deltaTime);
		if (Input.GetKeyDown(KeyCode.Space)) Fission();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		switch (other.tag)
		{
			case "Wall":
				Fission();
				break;
			case "BluePlayer":
				if (tag == "BlueBullet") return;
				Fission();
				break;
			case "RedPlayer":
				if (tag == "RedBullet") return;
				Fission();
				break;
		}
		
	}

	private void Fission()
	{
		for (float r = 0; r < 360; r += 36) {
			Vector3 deltaPos = Quaternion.AngleAxis(r, Vector3.forward) * Vector3.right; 
			Instantiate(smallPrefab, transform.position + deltaPos, Quaternion.Euler(new Vector3(0,0,r+160)));
		}
		Destroy(gameObject);
	}

}
