using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class Player : MonoBehaviour {

	KeyCode attackKeyCode;       //代表攻击按键

	//定义子弹
	BulletType bulletType;               //定义当前子弹类型
	public GameObject[] bulletArray;     //子弹数组
	private GameObject Bullet;           //当前子弹
	public int residueCount;            //剩余次数（只对特殊子弹有效）

	//定义玩家属性变量，如生命等
	private int heart = 3;

	//定义图片，音效，特效
	public Sprite red_player_sprite;
	public Sprite blue_player_sprite;
	public GameObject ExploPrefab;
	public GameObject ExploPrefabE;
	private AudioClip exploClip;
	private AudioClip shootClip;
	private AudioClip impactClip;
	private AudioClip underAttackClip;

	//定义子弹发射器
	BulletEmitter bulletEmitter;

	//物理相关
	private const float lineRecoil = 220.0f;
	private const float angleRecoil = 30.0f;
	private float maxVelocity;
	private float maxAngleVelocity;
	private const float normalMaxVelocity = 10.0f;
	private const float normalMaxAngleVelocity = 1000.0f;
	private const float netMaxVelocity = 2.5f;
	private const float netMaxAngleVelocity = 100.0f;
	private const float normalDrag = 0.3f;
	private const float netDrag = 0.1f;

	//计时器相关
	private float shieldTimeVal = 0.11f;

	//定义组件相关变量
	private Rigidbody2D rb2d;
	private SpriteRenderer sr;
	private AudioSource AS;

	private void Awake()
	{
		bulletEmitter = new BulletEmitter(this);
		rb2d = GetComponent<Rigidbody2D>();
		sr = GetComponent<SpriteRenderer>();

		bulletArray = new GameObject[(int)BulletType.Number];

		//加载资源
		ExploPrefab = (GameObject)Resources.Load("Prefabs/Effect/Explo");
		ExploPrefabE = (GameObject)Resources.Load("Prefabs/Effect/ExploE");
		red_player_sprite = Resources.Load<Sprite>("Sprites/RedPlayer");
		blue_player_sprite = Resources.Load<Sprite>("Sprites/BluePlayer");

		exploClip = (AudioClip)Resources.Load("Audio/explode");
		impactClip = (AudioClip)Resources.Load("Audio/Impact");
		shootClip = (AudioClip)Resources.Load("Audio/Shoot");
		underAttackClip = (AudioClip)Resources.Load("Audio/UnderAttack");

	}

	private void OnEnable()
	{
		PlayerManager.TimeLimitEvent += ChangeHeart;
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

		//设置物理属性
		maxVelocity = normalMaxVelocity;
		maxAngleVelocity = normalMaxAngleVelocity;

	}

	// Update is called once per frame
	void Update()
	{
		//获取输入
		if (Input.GetKeyDown(attackKeyCode))
		{
			Attack();
		}
		//被攻击后短时间无敌
		if (shieldTimeVal < 0.1f)
		{
			shieldTimeVal += Time.deltaTime;
		}

		//速度上限
		if (rb2d.velocity.magnitude > maxVelocity)
		{
			rb2d.velocity = rb2d.velocity / rb2d.velocity.magnitude * maxVelocity;
		}
		if (rb2d.angularVelocity > maxAngleVelocity)
		{
			rb2d.angularVelocity = maxAngleVelocity;
		}
		else if (rb2d.angularVelocity < -maxAngleVelocity)
		{
			rb2d.angularVelocity = -maxAngleVelocity;
		}

	}

	//受力方法
	private void ActiveForce(Force force)
	{
		rb2d.AddForce(force.DirVec * force.lineForce);
		rb2d.AddTorque(-force.angleForce);
	}

	//攻击方法
	private void Attack()
	{
		//产生后坐力
		ActiveForce(new Force(lineRecoil, angleRecoil, Quaternion.AngleAxis(20, Vector3.forward) * transform.right * -1));
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
		//产生音效
		transform.parent.SendMessage("AudioPlay",shootClip);
		//发射子弹
		bulletEmitter.EmitBullet(bulletType,Bullet);
	}

	//改变血量方法
	public void ChangeHeart(int delta)
	{
		//短时间无敌
		if (shieldTimeVal < 0.1f && delta < 0)
			return;
		if (delta < 0) {
			shieldTimeVal = 0;
			if (heart != 1) 
				transform.parent.SendMessage("AudioPlay", underAttackClip);
		}
			
		heart += delta;
		if (heart == 0) {
			//游戏结束
			Die();
		}
		if (heart > 3)
			heart = 3;

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
				bulletArray[(int)BulletType.Thunder] = (GameObject)Resources.Load("Prefabs/Bullet/Blue/Thunder");
				bulletArray[(int)BulletType.bounce] = (GameObject)Resources.Load("Prefabs/Bullet/Blue/BounceBullet");
				bulletArray[(int)BulletType.fission] = (GameObject)Resources.Load("Prefabs/Bullet/Blue/FissionBullet");
				break;
			case "RedPlayer":
				bulletArray[(int)BulletType.small] = (GameObject)Resources.Load("Prefabs/Bullet/Red/SmallBullet");
				bulletArray[(int)BulletType.Thunder] = (GameObject)Resources.Load("Prefabs/Bullet/Red/Thunder");
				bulletArray[(int)BulletType.bounce] = (GameObject)Resources.Load("Prefabs/Bullet/Red/BounceBullet");
				bulletArray[(int)BulletType.fission] = (GameObject)Resources.Load("Prefabs/Bullet/Red/FissionBullet");
				break;
		}
	}

	//重置当前子弹为小子弹
	public void ResetBullet()
	{
		bulletType = BulletType.small;
		Bullet = bulletArray[(int)bulletType];
	}
	//设置子弹类型为弹性子弹
	private void SetBounce()
	{
		bulletType = BulletType.bounce;
		Bullet = bulletArray[(int)bulletType];
		residueCount = 1;
	}
	//设置子弹类型为分裂弹
	private void SetFission()
	{
		bulletType = BulletType.fission;
		Bullet = bulletArray[(int)bulletType];
		residueCount = 2;
	}
	//设置子弹类型为闪电
	private void SetThunder()
	{
		bulletType = BulletType.Thunder;
		Bullet = bulletArray[(int)bulletType];
		residueCount = 5;
	}

	//蜘蛛网相关
	private void OnNetEnter()
	{
		rb2d.angularDrag = netDrag;
		rb2d.drag = netDrag;

		maxVelocity = netMaxVelocity;
		maxAngleVelocity = netMaxAngleVelocity;

	}
	private void OnNetExit()
	{
		rb2d.angularDrag = normalDrag;
		rb2d.drag = normalDrag;

		maxVelocity = normalMaxVelocity;
		maxAngleVelocity = normalMaxAngleVelocity;
	}

	//死亡方法
	private void Die()
	{
		//爆炸效果
		Instantiate(ExploPrefab, transform.position, transform.rotation);
		Instantiate(ExploPrefabE, transform.position, transform.rotation);
		//爆炸音效
		transform.parent.SendMessage("AudioPlay", exploClip);
		//游戏结束
		GameMode.GameOver();
		//销毁玩家
		Destroy(gameObject);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		transform.parent.SendMessage("AudioPlay", impactClip);
	}

}

