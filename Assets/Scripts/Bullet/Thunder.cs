using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

public class Thunder : MonoBehaviour {

	RaycastHit2D[] hitArray;
	LineRenderer lr;

	//目标玩家的标签，用于判断命中目标是不是敌人
	string targetTag;

	//代表射线命中的目标玩家，若未命中则为空
	GameObject target;
	//命中目标所在位置，用于生成闪电特效
	Vector3 pos;

	//绘制相关
	private float detail = 0.02f;           //单节闪电最小中点偏移量，该值越小闪电链节数越多
	private List<Vector3> posList;         //顶点链表

	private void Awake()
	{
		lr = GetComponent<LineRenderer>();
		posList = new List<Vector3>();
	}

	// Use this for initialization
	void Start () {
		switch (tag)
		{
			case "BlueBullet":
				targetTag = "RedPlayer";
				break;
			case "RedBullet":
				targetTag = "BluePlayer";
				break;
		}
		Attack();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//展开攻击
	void Attack()
	{
		//发射射线
		EmitRay();
		//绘制闪电特效
		DrawThunder();
		//一段时间后销毁自身(开启自毁定时)
		Destroy(gameObject, 0.3f);
	}

	void EmitRay()
	{
		//发射射线
		hitArray =  Physics2D.RaycastAll(transform.position, transform.right);
		//处理射线信息
		foreach (RaycastHit2D hit in hitArray)
		{
			//命中玩家
			if (hit.transform.tag == targetTag) {
				target = hit.transform.gameObject;
				pos = hit.point;
				break;
			}
			//命中墙
			if (hit.transform.tag == "Wall") {
				pos = hit.point;
			}
		}
		//若命中玩家，造成伤害
		if (target != null) {
			target.SendMessage("ChangeHeart", -1);
		}

	}

	//绘制闪电特效
	void DrawThunder()
	{
		//设置闪电的线段属性
		lr.startWidth = 0.05f;
		lr.endWidth = 0.05f;

		//生成顶点
		CollectVertex(transform.position, pos, 0.5f);
		posList.Add(pos);

		//将顶点添加到LineRenderer
		lr.positionCount = posList.Count;
		lr.SetPositions(posList.ToArray());

	}

	//递归随机生成顶点，dis代表随机偏移量
	void CollectVertex(Vector3 start,Vector3 end,float dis)
	{
		if (dis < detail)
		{
			posList.Add(start);
			return;
		}

		//生成中点
		float midx = (start.x + end.x) / 2 + (Random.value - 0.5f) * dis;
		float midy = (start.y + end.y) / 2 + (Random.value - 0.5f) * dis;
		float midz = (start.z + end.z) / 2 + (Random.value - 0.5f) * dis;
		Vector3 midPos = new Vector3(midx, midy, midz);

		//递归左右线段
		CollectVertex(start, midPos, dis / 2);
		CollectVertex(midPos, end, dis / 2);
	}

}
