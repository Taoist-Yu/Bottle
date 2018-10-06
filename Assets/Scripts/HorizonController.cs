using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizonController : MonoBehaviour
{
	
	private GameObject lWall,rWall;

	private float shrinkWaitingTime = 30.0f;  //收缩的间隔时间
	private WallShrinker Shrinker;


	private void Awake()
	{
		lWall = GameObject.Find("LeftWall");
		rWall = GameObject.Find("RightWall");
		Shrinker = new WallShrinker(lWall, rWall);
	}

	// Use this for initialization
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		Shrinker.Update();

		shrinkWaitingTime -= Time.deltaTime;
		if (shrinkWaitingTime <= 0) {
			shrinkWaitingTime = 30.0f;
			Shrinker.BeginShrink(lWall.transform.localPosition,rWall.transform.localPosition);
			//重置计时变量，开始一次收缩
		}

	}

}

//控制墙体的一次收缩
class WallShrinker
{
	private const float shrinkTime = 3.0f;  //一次收缩的时间
	private const float shrinkDistance = 2.0f;   //一次收缩距离

	//记录墙壁的旋转情况
	private Vector3 LeftRotation = new Vector3(0, 0, 180);
	private Vector3 RightRotation = new Vector3(0, 0, 0);

	private bool isShrinking = false;
	private float timeVal;    //当前收缩时间
	private GameObject lWall, rWall;

	private Vector3 oldPosL,oldPosR;   //一次收缩中物体原本的位置

	
	//初始化构造函数并且接受左右墙的实例
	public WallShrinker(GameObject LeftWall,GameObject RightWall) {
		lWall = LeftWall;
		rWall = RightWall;
	}

	public void Update()
	{
		//如果当前不在收缩状态，则退出
		if (isShrinking == false)
			return;

		timeVal += Time.deltaTime;
		if (timeVal >= shrinkTime) {
			EndShrink();
			timeVal = shrinkTime;
		}
		MoveWall();
		
	}

	//开始一次收缩
	public void BeginShrink(Vector3 lPos,Vector3 rPos)
	{
		isShrinking = true;
		oldPosL = lPos;
		oldPosR = rPos;
		timeVal = 0;
	}

	//结束一次收缩
	public void EndShrink()
	{
		isShrinking = false;
	}


	private void MoveWall()
	{
		//计算已经收缩的距离
		float moveDistance = shrinkDistance / shrinkTime * timeVal;

		//计算墙壁的新坐标
		Vector3 newPosL = oldPosL + Vector3.right * moveDistance;
		Vector3 newPosR = oldPosR + Vector3.left * moveDistance;

		//重新设置墙壁的位置
		lWall.transform.SetPositionAndRotation(newPosL, Quaternion.Euler(LeftRotation));
		rWall.transform.SetPositionAndRotation(newPosR, Quaternion.Euler(RightRotation));
	}

}