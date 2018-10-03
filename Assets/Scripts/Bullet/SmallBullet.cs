using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallBullet : MonoBehaviour {

	public Sprite blue_sprite;
	public Sprite red_sprite;

	private SpriteRenderer sr;

	private const float moveSpeed = 10.0f;

	private void Awake()
	{
		sr = GetComponent<SpriteRenderer>();
	}

	// Use this for initialization
	void Start () {
		//设置图片精灵
		switch (tag)
		{
			case "BlueBullet":
				sr.sprite = blue_sprite;
				break;
			case "RedBullet":
				sr.sprite = red_sprite;
				break;
		}

	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(Quaternion.AngleAxis(20,Vector3.forward) * Vector3.left * moveSpeed * Time.deltaTime);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		switch (other.tag)
		{
			case "Wall":
				Destroy(gameObject);
				break;
			case "BluePlayer":
				if (tag == "BlueBullet") return;
				other.SendMessage("BeAttack");
				Destroy(gameObject);
				break;
			case "RedPlayer":
				if (tag == "RedBullet") return;
				other.SendMessage("BeAttack");
				Destroy(gameObject);
				break;
		}
		
	}

}
