using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallBullet : MonoBehaviour {

	public Sprite sprite;

	private SpriteRenderer sr;

	private const float moveSpeed = 10.0f;

	private void Awake()
	{
		sr = GetComponent<SpriteRenderer>();
	}

	// Use this for initialization
	void Start()
	{
		//设置图片精灵
		sr.sprite = sprite;
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
				other.SendMessage("ChangeHeart",-1);
				other.SendMessage("ActiveForce", new Force(60.0f, 15.0f, -transform.right));
				Destroy(gameObject);
				break;
			case "RedPlayer":
				if (tag == "RedBullet") return;
				other.SendMessage("ChangeHeart",-1);
				other.SendMessage("ActiveForce", new Force(60.0f, 15.0f, -transform.right));
				Destroy(gameObject);
				break;
		}
		
	}

}
