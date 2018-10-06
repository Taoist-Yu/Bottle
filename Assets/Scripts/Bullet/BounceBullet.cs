using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBullet : MonoBehaviour
{

	public Sprite sprite;

	private SpriteRenderer sr;
	private Rigidbody2D rb2d;

	private float moveSpeed = 10.0f;

	private void Awake()
	{
		sr = GetComponent<SpriteRenderer>();
		rb2d = GetComponent<Rigidbody2D>();
	}

	// Use this for initialization
	void Start()
	{
		//设置图片精灵
		sr.sprite = sprite;
		//设置初速度
		rb2d.velocity =  Quaternion.AngleAxis(20, Vector3.forward) * -transform.right * moveSpeed;
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		switch (other.tag)
		{
			case "Wall":
				Bounce(other.gameObject.layer);
				break;
			case "BluePlayer":
				if (tag == "BlueBullet") return;
				other.SendMessage("ChangeHeart", -1);
				Destroy(gameObject);
				break;
			case "RedPlayer":
				if (tag == "RedBullet") return;
				other.SendMessage("ChangeHeart", -1);
				Destroy(gameObject);
				break;
		}
		Debug.Log("Trigger");
	}

	private void Bounce(int layer)
	{
		if (layer == LayerMask.NameToLayer("hWall"))
		{
			rb2d.velocity = new Vector2
			(
				rb2d.velocity.x,
				-rb2d.velocity.y
			);
		}
		else if (layer == LayerMask.NameToLayer("vWall"))
		{
			rb2d.velocity = new Vector2
			(
				-rb2d.velocity.x,
				rb2d.velocity.y
			);
		}
	}
}
