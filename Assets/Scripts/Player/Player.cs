using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class Player : MonoBehaviour {

	public enum BulletType {
		small = 0,
		big = 1,
		bounce = 2,
		fission = 3,
		Number = 4
	};
	BulletType bulletType;   //定义当前子弹类型
	KeyCode attackKeyCode;       //代表攻击按键

	//定义子弹
	public GameObject[] bulletArray;    //子弹数组
	private GameObject Bullet;           //当前子弹

	//定义玩家属性变量，如生命等
	private int heart = 3;

	//定义玩家图片
	public Sprite red_player_sprite;
	public Sprite blue_player_sprite;

	//定义子弹发射器
	BulletEmitter bulletEmitter;

	//定义后坐力大小与运动速度上限常量
	private const float lineRecoil = 200.0f;
	private const float AngleRecoil = 30.0f;
	private const float maxVelocity = 15.0f;
	private const float maxAngleVelocity = 1000.0f;

	//定义组件相关变量
	private Rigidbody2D rb2d;
	private SpriteRenderer sr;
	private GameObject test;

	private void Awake()
	{

		bulletEmitter = new BulletEmitter(this);
		rb2d = GetComponent<Rigidbody2D>();
		sr = GetComponent<SpriteRenderer>();

		bulletArray = new GameObject[(int)BulletType.Number];

	}

	// Use this for initialization
	void Start ()
	{
		//根据玩家设置属性
		switch (tag)
		{
			case "BluePlayer":
				attackKeyCode = KeyCode.Q;
				sr.sprite = blue_player_sprite;
				break;
			case "RedPlayer":
				attackKeyCode = KeyCode.UpArrow;
				sr.sprite = red_player_sprite;
				break;
		}

		//初始化子弹属性
		InitBullet();

		//设置默认的子弹类型
		bulletType = BulletType.small;
		Bullet = bulletArray[(int)BulletType.small];

	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown(attackKeyCode)) {
			Attack();
		}
	}

	//攻击方法
	private void Attack()
	{
		//产生后坐力
		rb2d.AddForce(Quaternion.AngleAxis(20,Vector3.forward) * transform.right * -lineRecoil);
		rb2d.AddTorque(-AngleRecoil);
		//速度上限
		if (rb2d.velocity.magnitude > maxVelocity) {
			rb2d.velocity = rb2d.velocity / rb2d.velocity.magnitude * maxVelocity;
		}
		if (rb2d.angularVelocity > maxAngleVelocity) {
			rb2d.angularVelocity = maxAngleVelocity;
		}
		else if(rb2d.angularVelocity < -maxAngleVelocity)
		{
			rb2d.angularVelocity = -maxAngleVelocity;
		}
		//发射子弹
		bulletEmitter.EmitBullet(bulletType,Bullet);
	}

	//受击方法
	private void BeAttack()
	{
		heart -= 1;
		if (heart == 0) {
			//游戏结束
			Debug.Log("功能尚未完善");
		}
		switch (heart)
		{
			case 3:
				sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1.0f);
				break;
			case 2:
				sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0.6f);
				break;
			case 1:
				sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0.2f);
				break;

		}
	}

	//子弹属性的初始化方法
	private void InitBullet()
	{
		//初始化子弹标签，为子弹数组赋值
		switch (tag)
		{
			case "BluePlayer":
				bulletArray[(int)BulletType.small] = (GameObject)Resources.Load("Prefabs/Bullet/Blue/SmallBullet");
				bulletArray[(int)BulletType.big] = (GameObject)Resources.Load("Prefabs/Bullet/Blue/SmallBullet");
				bulletArray[(int)BulletType.bounce] = (GameObject)Resources.Load("Prefabs/Bullet/Blue/SmallBullet");
				bulletArray[(int)BulletType.fission] = (GameObject)Resources.Load("Prefabs/Bullet/Blue/SmallBullet");
				break;
			case "RedPlayer":
				bulletArray[(int)BulletType.small] = (GameObject)Resources.Load("Prefabs/Bullet/Red/SmallBullet");
				bulletArray[(int)BulletType.big] = (GameObject)Resources.Load("Prefabs/Bullet/Red/SmallBullet");
				bulletArray[(int)BulletType.bounce] = (GameObject)Resources.Load("Prefabs/Bullet/Red/SmallBullet");
				bulletArray[(int)BulletType.fission] = (GameObject)Resources.Load("Prefabs/Bullet/Red/SmallBullet");
				break;
		}

	}

}

class BulletEmitter : Object
{
	Player player;
	private GameObject bulletInstant;

	public BulletEmitter(Player _player)
	{
		this.player = _player;
	}

	public void EmitBullet(Player.BulletType bullet_type,GameObject bullet)
	{
		switch (bullet_type)
		{
			case Player.BulletType.small:
				EmitSmallBullet(bullet);
				break;
			case Player.BulletType.big:
				EmitBigBullet(bullet);
				break;
			case Player.BulletType.bounce:
				EmitBounceBullet(bullet);
				break;
			case Player.BulletType.fission:
				EmitFissionBullet(bullet);
				break;
		}
	}

	private void EmitSmallBullet(GameObject bullet)
	{
		if (bulletInstant != null) {
			DestroyImmediate(bulletInstant);
		}

		bulletInstant = Instantiate(bullet,
						player.transform.position+0.2f*player.transform.right,
						Quaternion.Euler(player.transform.eulerAngles + new Vector3(0,0,180)));
	}

	private void EmitBigBullet(GameObject bullet)
	{
		Debug.Log("该功能尚未完善");
	}

	private void EmitBounceBullet(GameObject bullet)
	{
		Debug.Log("该功能尚未完善");
	}

	private void EmitFissionBullet(GameObject bullet)
	{
		Debug.Log("该功能尚未完善");
	}

}
