using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Property : MonoBehaviour {

	private enum pType {
		fission,
		bounce,
		thunder,
		save,
		nullPro,
		number
	};

	//定义精灵数组，editor中赋值
	public Sprite[] sprites;

	//组件相关
	private SpriteRenderer sr;

	//定时器相关
	private float timeVal = 0.0f;

	//随机数相关
	private System.Random rand;

	//当前道具
	private pType typeNow;

	private void Awake()
	{
		sr = GetComponent<SpriteRenderer>();
		rand = new System.Random();
	}

	// Use this for initialization
	void Start () {
		typeNow = pType.nullPro;
		sr.sprite = sprites[(int)typeNow];
	}
	
	// Update is called once per frame
	void Update () {
		timeVal += Time.deltaTime;
		if (timeVal > 10.0f) {
			ChangeProperty();
			//重置计时器
			timeVal = 0.0f;
		}
	}

	//变更道具方法
	private void ChangeProperty()
	{
		//更改类型
		typeNow = (pType)rand.Next(0, (int)pType.number - 1);
		//更改图片
		sr.sprite = sprites[(int)typeNow];
	}

	//令道具生效的总方法
	private void ActiveProperty(Collider2D other)
	{
		switch (typeNow)
		{
			case pType.bounce:
				ActiveBounce(other);
				break;
			case pType.fission:
				ActiveFission(other);
				break;
			case pType.save:
				ActiveSave(other);
				break;
			case pType.thunder:
				ActiveThunder(other);
				break;
		}
		typeNow = pType.nullPro;
		sr.sprite = sprites[(int)typeNow];
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag != "BluePlayer" && other.tag != "RedPlayer")
			return;

		ActiveProperty(other);

	}

	private void OnTriggerStay2D(Collider2D other)
	{
		if (other.tag != "BluePlayer" && other.tag != "RedPlayer")
			return;

		ActiveProperty(other);

	}

	private void ActiveBounce(Collider2D other)
	{
		other.SendMessage("SetBounce");
	}

	private void ActiveFission(Collider2D other)
	{
		other.SendMessage("SetFission");
	}

	private void ActiveThunder(Collider2D other)
	{
		other.SendMessage("SetThunder");
	}

	private void ActiveSave(Collider2D other)
	{
		other.SendMessage("ChangeHeart", 1);
	}

}
